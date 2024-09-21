using Microsoft.AspNetCore.Hosting;


[assembly: HostingStartup(typeof(Web.Areas.Identity.IdentityHostingStartup))]
namespace Web.Areas.Identity
{
	public class IdentityHostingStartup : IHostingStartup
	{
		public void Configure(IWebHostBuilder builder)
		{
			builder.ConfigureServices
			(
				(context, services) => { }
			);
		}
	}
}