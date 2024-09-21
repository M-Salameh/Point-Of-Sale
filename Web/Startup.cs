using Web.WebServices;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ApplicationLayer.ServicesProvider;
using Microsoft.Extensions.Configuration;
using PersistenceLayer.UnitOfWork.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using PersistenceLayer.UnitOfWork.ConcreteClasses;
using ApplicationLayer.Services.ApplicationServices.NotificationServices;

namespace Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<MarketContext>
			(
				options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
			);


			services.AddIdentity<ApplicationUser, IdentityRole>
			(
				configs =>
				{
					configs.User.RequireUniqueEmail = true;
					configs.SignIn.RequireConfirmedPhoneNumber = false;
					configs.SignIn.RequireConfirmedEmail = false;
				}
			).AddEntityFrameworkStores<MarketContext>();

			services.ConfigureApplicationCookie
			(
				configs =>
				{
					configs.LoginPath = "/Account/Login";
					configs.LogoutPath = "/Account/Logout";
				}
			);

			services.AddSignalR();
			services.AddControllersWithViews();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IServicePool, ServicePool>();
			services.AddScoped<INotificationService, NotificationService>();
			services.AddScoped<IAutomatedNotificationGenerator, AutomatedNotificationGenerator>();
			services.AddScoped<IServerAssetsDisposal, ServerAssetsDisposal>();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Errors/500.cshtml");
				// The default HSTS value is 30 days.
				// You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints
			(
				endpoints =>
				{
					endpoints.MapControllerRoute
					(
						name: "default",
						pattern: "{controller=Home}/{action=Index}/{id?}"
					);
				}
			);
			app.UseEndpoints
			(
				endpoints =>
				{
					endpoints.MapHub<NotificationHub>("/NotificationHub");

				}
			);
		}
	}
}
