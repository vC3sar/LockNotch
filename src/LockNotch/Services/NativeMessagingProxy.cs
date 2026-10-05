using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LockNotch.Services;

public static class NativeMessagingProxy
{
    public static void Run()
    {
        // Chrome Native Messaging communicates via standard input/output
        Stream stdin = Console.OpenStandardInput();
        Stream stdout = Console.OpenStandardOutput();

        // Connect to the main LockNotch instance
        using var pipeClient = new NamedPipeClientStream(".", "LockNotchDownloadsPipe", PipeDirection.Out, PipeOptions.Asynchronous);
        
        try
        {
            // Timeout after 5 seconds if LockNotch isn't running
            pipeClient.Connect(5000);
        }
        catch (Exception ex)
        {
            File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "LockNotch_Proxy_Error.txt"), ex.ToString());
            return;
        }

        byte[] lengthBytes = new byte[4];
        byte[] buffer = new byte[1024 * 1024]; // 1MB buffer max

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
                if (length <= 0 || length > buffer.Length)
                {
                    return; // invalid length
                }

                bytesRead = 0;
                while (bytesRead < length)
                {
                    int r = stdin.Read(buffer, bytesRead, length - bytesRead);
                    if (r == 0) return; // stdin closed
                    bytesRead += r;
                }

                string json = Encoding.UTF8.GetString(buffer, 0, length);

                // Forward to Named Pipe (with a simple framing: 4 bytes length + UTF8 bytes)
                byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
                byte[] outLengthBytes = BitConverter.GetBytes(jsonBytes.Length);
                
                pipeClient.Write(outLengthBytes, 0, 4);
                pipeClient.Write(jsonBytes, 0, jsonBytes.Length);
                pipeClient.Flush();
            }
            catch
            {
                // Any error (pipe closed, stdin closed) -> exit
                break;
            }
        }
    }
}
