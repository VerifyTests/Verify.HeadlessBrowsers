static class InnerSocketWaiter
{
    public static async Task Wait(int port)
    {
        using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.ReceiveTimeout = 100;
        for (var i = 0; i < 100; i++)
        {
            try
            {
                await socket.ConnectAsync(new DnsEndPoint("localhost", port));
                return;
            }
            catch (SocketException)
            {
                //no op
            }
        }

        throw new TimeoutException("Failed repeated attempts to open connection to webserver");
    }
}