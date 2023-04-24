
using System.Configuration;
using FA.JustBlog.Core.DbInitializer;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.UnitOfWork;
using FA.JustBlog.Properties;
using log4net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LogConfiguration.SetupLog4net();

            var logger = LogManager.GetLogger(typeof(Program));
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<JustBlogContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddIdentity<AppUser, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<JustBlogContext>();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IDbInitializer, DbInitializer>();
            builder.Services.AddScoped<IList<AuthenticationScheme>, List<AuthenticationScheme>>();
            builder.Services.ConfigureApplicationCookie(ops =>
            {
                ops.LoginPath = $"/Account/Login";
                ops.LogoutPath = $"/Account/Logout";
                ops.AccessDeniedPath = $"/Home/Denied";

            });
            builder.Services.AddAuthentication()
                .AddGoogle(options =>
                {
                    var gconfig = builder.Configuration.GetSection("Authentication:Google");
                    options.ClientId = gconfig["ClientId"];
                    options.ClientSecret = gconfig["ClientSecret"];
                    options.CallbackPath = $"/Account/GoogleLogin";
                });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())    
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            SeedData();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "PostDetails",
                    pattern: "Post/{year}/{month}/{title}",
                    defaults: new { controller = "Post", action = "Details" },
                    constraints: new { year = @"\d{4}", month = @"\d{2}" }
                );



                endpoints.MapDefaultControllerRoute();
            });


            app.UseEndpoints(endpoints => { app.MapAreaControllerRoute(name: "Admin", areaName: "Admin", pattern: "Admin/{controller=Home}/{action=Index}"); app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}"); });
            app.MapRazorPages();


            app.Run();
            void SeedData()
            {
                using (var scope = app.Services.CreateScope())
                {
                    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
                    dbInitializer.Initialize();
                }

            }
        }


    }
}