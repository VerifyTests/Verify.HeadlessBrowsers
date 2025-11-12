#pragma warning disable ASPDEPR008
#pragma warning disable ASPDEPR004
public static class WebStartup
{
    [ModuleInitializer]
    public static void Init()
    {
        var builder = new WebHostBuilder();
        builder.UseStartup<Startup>();
        builder.UseKestrel();
        server = builder.Build();
        server.Start();
    }

    static IWebHost server = null!;
}