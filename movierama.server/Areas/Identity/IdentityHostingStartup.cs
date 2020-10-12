using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Movierama.Server.Areas.Identity.IdentityHostingStartup))]
namespace Movierama.Server.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}