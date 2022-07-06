namespace VerifyTests.Selenium;

public static class SocketWaiter
{
    public static Task Wait(int port) =>
        InnerSocketWaiter.Wait(port);
}