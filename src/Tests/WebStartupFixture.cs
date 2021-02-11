using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Xunit;

public class WebStartupFixture :
    IAsyncLifetime
{
    IWebHost? server;

    public Task InitializeAsync()
    {
        WebHostBuilder webBuilder = new();

        webBuilder.UseStartup<Startup>();
        webBuilder.UseKestrel();
        server = webBuilder.Build();
        return server.StartAsync();
    }

    public async Task DisposeAsync()
    {
        if (server != null)
        {
            await server.StopAsync();
            server.Dispose();
        }
    }
}