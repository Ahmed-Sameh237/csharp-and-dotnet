// ============================
//           PART D
// ============================
// D.1 Change the range of GPA to :: 2.9 - 4.0
// D.2 Change the range of year to :: 1 - 4 still the same
// D.3 and D.4 Run the application and edit one of students its gpa to 2.8 and a message error came out "GPA must be between 2.9 and 4.0."
// D.5 It is Validation-only attribute, as we didnt need to make any migration only save and run app, it works MVC immediately enforces the new limits.


using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentPortalWeb.Models
{

    public class Student
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = "";

        [Range(1,4,ErrorMessage = "Year of study must be between 1 and 4.")]    // MAX_YEAR_EDIT = 4 that means no change it is the same.
        public int YearOfStudy { get; set; }
        
        [Range(2.9,4.0,ErrorMessage = "GPA must be between 2.9 and 4.0.")]      // MIN_GPA_EDIT = 2.9 
        public double Gpa { get; set; }
    }

    public class Course
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string CourseName { get; set; } = "";

        public int Credits { get; set; }

        public int InstructorId { get; set; }

        public Instructor Instructor { get; set; } = null!;
    }

    public class Instructor
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = "";

        public int YearsOfExperience { get; set; }

        public List<Course> Courses { get; set; } = new();
    }

    // =================================================================
    // THE CONTEXT — Session 15's version, unchanged.
    // =================================================================
    public class StudentPortalContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }

        // Session 15, TODO 1: the constructor that makes this class
        // constructible by somebody else. This is what lets Program.cs
        // decide the connection string instead of this file deciding it.
        public StudentPortalContext(DbContextOptions<StudentPortalContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Session 14 Block 2 — Fluent API wins over annotations.
            modelBuilder.Entity<Student>()
                .Property(s => s.FullName)
                .IsRequired()
                .HasMaxLength(100);

            // Session 14 Block 3 — the real relationship. Restrict means
            // the database refuses to delete an instructor who still has
            // courses, rather than silently deleting the courses too.
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Instructor)
                .WithMany(i => i.Courses)
                .HasForeignKey(c => c.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
