using System;
using System.Collections.Concurrent;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LockNotch.Services;

public static class NativeMessagingProxy
{
    private static readonly BlockingCollection<byte[]> _messageQueue = new(100);
    private static readonly object _stdoutLock = new();

    public static void Run()
    {
        Stream stdin = Console.OpenStandardInput();
        
        Task.Run(SenderLoop);

        byte[] lengthBytes = new byte[4];
        byte[] buffer = new byte[1024 * 1024];

        while (true)
        {
            try
            {
                int bytesRead = 0;
                while (bytesRead < 4)
                {
                    int r = stdin.Read(lengthBytes, bytesRead, 4 - bytesRead);
                    if (r == 0) return; // stdin closed
                    bytesRead += r;
                }

                int length = BitConverter.ToInt32(lengthBytes, 0);
                if (length <= 0 || length > buffer.Length) return;

                bytesRead = 0;
                while (bytesRead < length)
                {
                    int r = stdin.Read(buffer, bytesRead, length - bytesRead);
                    if (r == 0) return; // stdin closed
                    bytesRead += r;
                }

                string json = Encoding.UTF8.GetString(buffer, 0, length);
                byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
                
                if (_messageQueue.Count >= 90) _messageQueue.TryTake(out _);
                _messageQueue.Add(jsonBytes);
            }
            catch
            {
                break;
            }
        }
    }

    private static void SenderLoop()
    {
        while (true)
        {
            try
            {
                using var pipeClient = new NamedPipeClientStream(".", "LockNotchDownloadsPipe", PipeDirection.InOut, PipeOptions.Asynchronous);
                pipeClient.Connect(2000);
                
                var cts = new CancellationTokenSource();
                var readerTask = Task.Run(() => ReaderLoop(pipeClient, cts.Token));

                while (pipeClient.IsConnected)
                {
                    if (_messageQueue.TryTake(out byte[]? jsonBytes, 1000))
                    {
                        byte[] outLengthBytes = BitConverter.GetBytes(jsonBytes.Length);
                        pipeClient.Write(outLengthBytes, 0, 4);
                        pipeClient.Write(jsonBytes, 0, jsonBytes.Length);
                        pipeClient.Flush();
                    }
                }
                
                cts.Cancel();
            }
            catch
            {
                Thread.Sleep(2000);
            }
        }
    }

    private static async Task ReaderLoop(NamedPipeClientStream pipe, CancellationToken token)
    {
        byte[] lengthBuffer = new byte[4];
        byte[] dataBuffer = new byte[1024 * 1024];
        Stream stdout = Console.OpenStandardOutput();
        
        try
        {
            while (pipe.IsConnected && !token.IsCancellationRequested)
            {
                int bytesRead = 0;
                while (bytesRead < 4)
                {
                    int r = await pipe.ReadAsync(lengthBuffer, bytesRead, 4 - bytesRead, token);
                    if (r == 0) break;
                    bytesRead += r;
                }
                if (bytesRead < 4) break;

                int length = BitConverter.ToInt32(lengthBuffer, 0);
                if (length <= 0 || length > dataBuffer.Length) break;

                bytesRead = 0;
                while (bytesRead < length)
                {
                    int r = await pipe.ReadAsync(dataBuffer, bytesRead, length - bytesRead, token);
                    if (r == 0) break;
                    bytesRead += r;
                }
                if (bytesRead < length) break;

                lock (_stdoutLock)
                {
                    stdout.Write(lengthBuffer, 0, 4);
                    stdout.Write(dataBuffer, 0, length);
                    stdout.Flush();
                }
            }
        }
        catch { }
    }
}
