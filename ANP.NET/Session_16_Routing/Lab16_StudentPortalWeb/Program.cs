// =======================
//         PART A
// =======================
// ### A.1 
// The App run and all works.

// ### A.2
// LAB 16 — Lab ID: 8 | MAX_YEAR = 1 | MIN_GPA = 3.5 | INTAKE_CODE = itiC

// ### A.3
// Default route sits at bottom of the route table to not make a conflicts with other routes, as if it be at the top ,always the default route runs.

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentPortalWeb.Constraints;
using StudentPortalWeb.Models;
using System;

namespace StudentPortalWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // =========================================================
            // PHASE ONE — WHAT CAN THIS APP DO?
            // Everything above builder.Build() registers capabilities
            // into the DI container. Nothing here handles a request yet.
            // =========================================================
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<StudentPortalContext>(options =>
            {
                options.UseSqlServer("Data Source=.;Initial Catalog=ITI_StudentPortalDB_EF;Integrated Security=True;Encrypt=True;TrustServerCertificate=True")
                .LogTo(Console.WriteLine , LogLevel.Information)
                .EnableSensitiveDataLogging();
            });


            builder.Services.AddRouting(options =>
            {
                options.ConstraintMap.Add("honourBand", typeof(HonourBandConstraint));
            });

            // ===================
            //       PART D
            // ===================

            builder.Services.AddRouting(options =>

                options.ConstraintMap.Add("intakeCode", typeof(IntakeCodeConstraint))

            );

            var app = builder.Build();
            // ↑↑↑ THE DIVIDING LINE. Above: what exists. Below: what runs.

            // =========================================================
            // PHASE TWO — HOW IS A REQUEST HANDLED?
            // Every app.Use... call below adds one checkpoint to the
            // hallway a request walks down, in the order written.
            // =========================================================
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.Use(async (context, next) =>
            {
                Console.WriteLine($"[START] Request path : {context.Request.Path}");
                await next.Invoke();
                Console.WriteLine($"[END] Request path : {context.Request.Path}");
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            //app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "studentsList",
                pattern: "students",
                defaults: new { controller = "Students", action = "Index" }
                );

            app.MapControllerRoute(
                name: "studentsDetails",
                pattern: "students/{id:int}",
                defaults: new { controller = "Students", action = "Details" }
                );

            app.MapControllerRoute(
                name: "studentsByYear",
                pattern: "students/year/{year:int:range(1,4)}",
                defaults: new { controller = "Students", action = "ByYear" }
                );

            app.MapControllerRoute(
                name: "studentsHonours",
                pattern: "students/honours/{band:honourBand}",
                defaults: new { controller = "Students", action = "Honours" }
                );
            // =======================
            //         PART D
            // =======================
            //URL Result
            // students / intake / itiC    Works
            // students / intake / ITIC    Works
            // students / intake / itiA    404
            // students / intake / banana  404
            app.MapControllerRoute(
                name: "studentsintake",
                pattern: "students/intake/{code:intakeCode}",
                defaults: new {controller = "Students", action = "Intake"}
                );

            // =======================
            //         PART C
            // =======================
            //  MAX_YEAR (1) is accepted because the range constraint is inclusive.
            app.MapControllerRoute(
                name: "highestGpaStudents",
                pattern: "students/top/{count:int:range(1,1)}",
                defaults: new { controller = "Students", action = "Top" }
                );

            // =======================
            //         PART B
            // =======================
            // Yes, it can be because they provide alternative ways to access the same resource without changing its behavior.
            app.MapControllerRoute(
                name: "studentsRoster",
                pattern: "roster",
                defaults: new { controller = "Students", action = "Index" }
                );
            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");

            //app.MapControllers();

            app.Run();
        }
    }
}
