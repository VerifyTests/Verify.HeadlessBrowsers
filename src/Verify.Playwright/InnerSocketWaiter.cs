using ProtocolType = System.Net.Sockets.ProtocolType;

static class InnerSocketWaiter
{
    public static async Task Wait(int port)
    {
        for (var i = 0; i < 100; i++)
        {
            using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                using var cancellation = new CancelSource(TimeSpan.FromMilliseconds(100));
                await socket.ConnectAsync(new DnsEndPoint("localhost", port), cancellation.Token);
                return;
            }
            catch (Exception exception) when (exception is SocketException or OperationCanceledException)
            {
                await Task.Delay(100);
            }
        }

        throw new TimeoutException("Failed repeated attempts to open connection to webserver");
    }
}