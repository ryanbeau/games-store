using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Sprint.Areas.Identity.IdentityHostingStartup))]
namespace Sprint.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}