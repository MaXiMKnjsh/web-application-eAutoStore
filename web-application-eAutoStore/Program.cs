using Microsoft.EntityFrameworkCore;
using web_application_eAutoStore.APPLICATION.Interfaces.Auth;
using web_application_eAutoStore.APPLICATION.Services;
using web_application_eAutoStore.Data;
using web_application_eAutoStore.Interfaces.Repositories;
using web_application_eAutoStore.Interfaces.Services;
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
			builder.Services.AddTransient<IJwtProvider, JwtProvider>();

			builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
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

			app.UseStaticFiles(); // connectiong wwwroot

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