using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Lab15_StudentPortalWeb.Models;
using Lab15_StudentPortalWeb.Services;
using System;
namespace Lab15_StudentPortalWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();



            // ############## PART C.4 ##############

            builder.Services.AddDbContext<StudentPortalContext>(options =>
            {
                options.UseSqlServer("Data Source=.;Initial Catalog=ITI_StudentPortalDB_EF;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
            });

            // ############## PART D.4 ###############
            //builder.Services.AddDbContext<StudentPortalContext>(options =>
            //{
            //    options.UseSqlServer("Data Source=.;Initial Catalog=ITI_StudentPortalDB_EF;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
            //},ServiceLifetime.Singleton);


            // Lab ID = 8, 8 mod 3 = 2,  lifetime is Singleton.
            builder.Services.AddSingleton<IAhmedStampService, AhmedStampService>();

            var app = builder.Build();

           
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.Use(async (context, next) =>
            {
                Console.WriteLine($"[START] {context.Request.Path}");

                if (context.Request.Path.Value!.Contains("/audit-08"))
                {
                    Console.WriteLine($"[AUDIT] Ahmed Sameh saw a request for {context.Request.Path}");
                }

                await next();

                Console.WriteLine($"[END] {context.Request.Path}");
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();

           

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();


            app.Run();

        }
    }
}

