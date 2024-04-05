using AutoMapper;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using web_application_eAutoStore.APPLICATION.Interfaces.Auth;
using web_application_eAutoStore.APPLICATION.Interfaces.Repositories;
using web_application_eAutoStore.APPLICATION.Services;
using web_application_eAutoStore.Data;
using web_application_eAutoStore.INFRASTRUCTURE.Repositories;
using web_application_eAutoStore.Interfaces.Repositories;
using web_application_eAutoStore.Interfaces.Services;
using web_application_eAutoStore.MyExtensions;
using web_application_eAutoStore.Repositories;
using web_application_eAutoStore.Services;

namespace web_application_eAutoStore
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

            builder.Services.AddTransient<IUsersService, UsersService>();
			builder.Services.AddTransient<IUsersRepository, UsersRepository>();
			builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();
			builder.Services.AddTransient<ITokensService, TokensService>();
			builder.Services.AddTransient<IVehiclesService, VehiclesService>();
			builder.Services.AddTransient<IVehiclesRepository, VehiclesRepository>();
			builder.Services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
			builder.Services.AddTransient<IImageService,ImageService>();

			builder.Services.AddHttpContextAccessor();

			builder.Services.AddAutoMapper(typeof(Program).Assembly);

			builder.Services.Configure<TokensOptions>(builder.Configuration.GetSection(nameof(TokensOptions)));
			builder.Services.AddUserAuthentication(builder.Configuration);

            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

			builder.Services.Configure<RazorViewEngineOptions>(options =>
			{
				options.ViewLocationFormats.Add("/Views/Shared/Partials/{0}.cshtml");
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();

			app.UseStaticFiles(); // connecting wwwroot

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
		}
	}
}