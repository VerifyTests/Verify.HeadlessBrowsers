using System.Net;
using System.Net.Sockets;
using VerifyTestsPlaywright;

public class SocketWaiterTests
{
    [Test]
    public async Task Returns_when_server_already_listening()
    {
        var port = FreePort();
        using var listener = new TcpListener(IPAddress.Loopback, port);
        listener.Start();

        await SocketWaiter.Wait(port);
    }

    [Test]
    public async Task Retries_until_server_starts()
    {
        var port = FreePort();
        using var listener = new TcpListener(IPAddress.Loopback, port);

        // start waiting before anything is listening
        var waitTask = SocketWaiter.Wait(port);

        // connection is refused, so Wait should still be retrying rather than
        // giving up (the pre-fix code burned through every attempt in a few ms)
        await Task.Delay(300);
        Assert.That(waitTask.IsCompleted, Is.False);

        // bring the server up; Wait should connect on a subsequent retry
        listener.Start();
        await waitTask;
    }

    // Slow (~20s) by design: it exercises the full give-up path. Each attempt is
    // bounded to ~100ms by the connect timeout, so 100 attempts exhaust in ~20s.
    // Without that timeout a refused connect can block ~2s, blowing out to ~200s.
    [Test]
    public async Task Gives_up_in_bounded_time()
    {
        var port = FreePort(); // nothing ever listens here

        var waitTask = SocketWaiter.Wait(port);

        var finished = await Task.WhenAny(waitTask, Task.Delay(TimeSpan.FromSeconds(45)));
        Assert.That(finished, Is.SameAs(waitTask), "Wait blocked too long before giving up");

        Assert.ThrowsAsync<TimeoutException>(() => waitTask);
    }

    static int FreePort()
    {
        var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        var port = ((IPEndPoint)listener.LocalEndpoint).Port;
        listener.Stop();
        return port;
    }
}
