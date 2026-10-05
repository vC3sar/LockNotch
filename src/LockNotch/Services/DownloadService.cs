using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LockNotch.Services;

public class DownloadEvent
{
    public string type { get; set; } = "";
    public int downloadId { get; set; }
    public string filename { get; set; } = "";
    public string state { get; set; } = "";
    public long totalBytes { get; set; }
    public long bytesReceived { get; set; }
    public int progress { get; set; }
    public long speed { get; set; }
    public string url { get; set; } = "";
    public long timestamp { get; set; }

    public string SpeedText => speed >= 1048576
        ? $"{speed / 1048576.0:F1} MB/s"
        : $"{speed / 1024.0:F0} KB/s";

    public string DetailText => state == "in_progress" || type == "download_progress" || type == "download_started"
        ? (totalBytes > 0
            ? $"{bytesReceived / 1048576.0:F1} / {totalBytes / 1048576.0:F1} MB  ·  {SpeedText}"
            : $"{bytesReceived / 1048576.0:F1} MB  ·  {SpeedText}")
        : "";
}

public interface IDownloadService
{
    event EventHandler? DownloadsChanged;
    IEnumerable<DownloadEvent> GetActiveDownloads();
    bool HasActiveDownloads { get; }
    DownloadEvent? GetLatestDownload();
    void Start();
    event EventHandler<(double lat, double lon)>? LocationUpdated;
}

public class DownloadService : IDownloadService, IDisposable
{
    private readonly ConcurrentDictionary<int, DownloadEvent> _activeDownloads = new();
    private CancellationTokenSource? _cts;
    private Task? _serverTask;

    public event EventHandler? DownloadsChanged;
    public event EventHandler<(double lat, double lon)>? LocationUpdated;

    public IEnumerable<DownloadEvent> GetActiveDownloads() => 
        _activeDownloads.Values.OrderByDescending(d => d.timestamp);

    public bool HasActiveDownloads => !_activeDownloads.IsEmpty;

    public DownloadEvent? GetLatestDownload() =>
        GetActiveDownloads().FirstOrDefault();

    public void Start()
    {
        _cts = new CancellationTokenSource();
        _serverTask = Task.Run(() => ServerLoop(_cts.Token));
    }

    private async Task ServerLoop(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            try
            {
                var pipeSecurity = new System.IO.Pipes.PipeSecurity();
                var sid = new System.Security.Principal.SecurityIdentifier(System.Security.Principal.WellKnownSidType.BuiltinUsersSid, null);
                pipeSecurity.AddAccessRule(new System.IO.Pipes.PipeAccessRule(sid, System.IO.Pipes.PipeAccessRights.ReadWrite, System.Security.AccessControl.AccessControlType.Allow));

                using var pipeServer = System.IO.Pipes.NamedPipeServerStreamAcl.Create("LockNotchDownloadsPipe", PipeDirection.In, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous, 0, 0, pipeSecurity);
                
                await pipeServer.WaitForConnectionAsync(token);

                byte[] lengthBuffer = new byte[4];
                byte[] dataBuffer = new byte[1024 * 1024];

                while (pipeServer.IsConnected && !token.IsCancellationRequested)
                {
                    int bytesRead = 0;
                    while (bytesRead < 4)
                    {
                        int r = await pipeServer.ReadAsync(lengthBuffer, bytesRead, 4 - bytesRead, token);
                        if (r == 0) break;
                        bytesRead += r;
                    }
                    if (bytesRead < 4) break;

                    int length = BitConverter.ToInt32(lengthBuffer, 0);
                    if (length <= 0 || length > dataBuffer.Length) break;

                    bytesRead = 0;
                    while (bytesRead < length)
                    {
                        int r = await pipeServer.ReadAsync(dataBuffer, bytesRead, length - bytesRead, token);
                        if (r == 0) break;
                        bytesRead += r;
                    }
                    if (bytesRead < length) break;

                    string json = Encoding.UTF8.GetString(dataBuffer, 0, length);
                    
                    try
                    {
                        using var doc = JsonDocument.Parse(json);
                        var root = doc.RootElement;
                        if (root.TryGetProperty("type", out var typeProp) && typeProp.GetString() == "update_location")
                        {
                            if (root.TryGetProperty("latitude", out var latProp) && root.TryGetProperty("longitude", out var lonProp))
                            {
                                LocationUpdated?.Invoke(this, (latProp.GetDouble(), lonProp.GetDouble()));
                            }
                            continue;
                        }

                        var ev = JsonSerializer.Deserialize<DownloadEvent>(json);
                        if (ev != null)
                        {
                            HandleEvent(ev);
                        }
                    }
                    catch { }
                }
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception)
            {
                // Delay before restarting server to avoid tight failure loops
                await Task.Delay(1000, token);
            }
        }
    }

    private void HandleEvent(DownloadEvent ev)
    {
        // Check for completions, interruptions, or cancellations to remove them
        if (ev.type == "download_completed" || ev.type == "download_interrupted" || ev.type == "download_cancelled")
        {
            _activeDownloads[ev.downloadId] = ev;
            
            // Clean up after 3.5 seconds
            Task.Run(async () =>
            {
                await Task.Delay(3500);
                if (_activeDownloads.TryRemove(ev.downloadId, out _))
                {
                    DownloadsChanged?.Invoke(this, EventArgs.Empty);
                }
            });
        }
        else if (ev.type == "download_started" || ev.type == "download_progress")
        {
            // Do not overwrite a terminal state with an out-of-order progress update
            if (_activeDownloads.TryGetValue(ev.downloadId, out var existing))
            {
                if (existing.type == "download_completed" || existing.type == "download_interrupted" || existing.type == "download_cancelled")
                {
                    return; // Ignore progress update
                }
            }
            _activeDownloads[ev.downloadId] = ev;
        }

        DownloadsChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Dispose()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }
}
