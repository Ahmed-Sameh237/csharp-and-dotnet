using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using static Azure.Core.HttpHeader;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;
// ========================= PART A ========================
// how many students the PreInit script reported, and how many migration were already applied?
//=====================================================
//Session 14 PreInit — VERIFY ONLY (nothing is changed)
//=====================================================
//Database ITI_StudentPortalDB_EF        : FOUND
//Table Students                          : FOUND (4 row(s))
//Table Courses                           : FOUND
//Table Instructors                       : FOUND
//Migration history                       : 2 migration(s) applied
//Instructors.AssignedCourseName          : PRESENT (expected — Block 3 removes it)
//Courses.InstructorId                    : ABSENT (expected — Block 3 adds it)
//=====================================================
// PreInit complete. Ready for Session 14.
// Nothing was created, dropped, or altered by this script.

//3 migrations and 1 snapshot




namespace StudentPortalConsole
{

    public class Student
    {
        public int Id { get; set; }
        // ==========
        // D.1 Add [Required] and [MaxLength(100)] to Student.FullName
        // ==========
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = "";
        public int YearOfStudy { get; set; }
        public double Gpa { get; set; }
        public DateTime EnrollmentDate { get; set; }  // LabId :8 , so own property number 2 (8 % 3 = 2)
    }

    public class Course
    {
        public int Id { get; set; }
        // ==========
        // D.1 Add [Required] and [MaxLength(150)] to Course.CourseName
        // ==========
        [Required, MaxLength(150)]
        public string CourseName { get; set; } = "";
        public int Credits { get; set; }

        // ==========
        // E.1 Add InstructorId (matching your required/nullable choice above) and an Instructor navigation property to Course.
        // ==========
        public int InstructorId { get; set; }

        public Instructor Instructor { get; set; } = null!;

    }

    public class Instructor
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public int YearsOfExperience { get; set; }

        // ==========
        // E.2 Add a Courses list to Instructor, initialized to a new empty list. Delete AssignedCourseName entirely.
        // ==========
        public List<Course> Courses { get; set; } = new();
   
    }


    public static class StudentQueryExtensions
    {
        public static IEnumerable<Student> MyTopStudents(this IEnumerable<Student> source)
        {
            return source.Where(s => s.Gpa >= 2.5);
        }
    }

    
    public class StudentPortalContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=.;Database=ITI_StudentPortalDB_EF;Trusted_Connection=True;TrustServerCertificate=True;")
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        // ==========
        // D.2 Add the same FullName rules again using the Fluent API in OnModelCreating.
        // ==========
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .Property(s => s.FullName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Course>()
                .Property(c => c.CourseName)
                .IsRequired()
                .HasMaxLength(150);
            // ==========
            // E.3 Configure the relationship in OnModelCreating with HasOne/WithMany/HasForeignKey, and
            // OnDelete set to your derived behaviour.
            // ==========
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Instructor)
                .WithMany(i => i.Courses)
                .HasForeignKey(c => c.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }



    internal class Program
    {

        static async Task Main(string[] args)
        {

            using (var context = new StudentPortalContext())
            {
                // =============================================
                //                  PART B
                // =============================================

                // B1.What is in the database after this runs?
                //var s = await context.Students.FirstAsync(x => x.Id == 1);
                //s.Gpa = 3.99;               
                //Console.WriteLine(s.Gpa);
                // My Prediction: studnet that has id = 1 and change his gpa to 3.99

                // B2.Instructor.Courses is initialized to new().Lazy loading is not enabled.What does this print ?
                //var instructors = await context.Instructors.ToListAsync();
                //foreach (var i in instructors)
                //    Console.WriteLine($"{i.FullName}: {i.Courses.Count}");
                // My Prediction
                // Hamdy : 0
                // Mona : 0

                // B3. What happens here, and why is it dangerous?
                //var s = await context.Students.AsNoTracking().FirstAsync();
                //s.Gpa = 2.0;
                //await context.SaveChangesAsync();
                // My Prediction
                // This wont change the gpa of the student because using "AsNoTracking()" method making it read only no changes happen



                // =============================================
                //                  PART C
                // =============================================

                // GPA Value:  3.0 + ((your Lab ID mod 7) * 0.1) ==> 3.0 + ( { 8 % 7 } * 0.1 = 3.1

                // ==========
                // C.1 Read the student Nada Samir using an async single-entity method. Print her current GPA?
                // ==========
                //var nada = await context.Students.FirstOrDefaultAsync(s => s.FullName == "Nada Samir");
                //if (nada != null)
                //{
                //    Console.WriteLine($"Nada's Gpa: {nada.Gpa}");
                //}
                // Evidence from SSMS: 
                //  id  FullName    YearOfStudy Gpa EnrollmentDate
                //  1	Yara Adel	    2	    3.4	2024-09-01 00:00:00.0000000
                //  2   Omar Hesham     3       2.8 2023 - 09 - 01 00:00:00.0000000
                //  3   Nada Samir      1       3.3 2025 - 09 - 01 00:00:00.0000000     3.3 thats she
                //  4   Kareem Fouad    4       3.9 2022 - 09 - 01 00:00:00.0000000

                // ==========
                // C.2 Change her GPA to your derived value — but do not save. Print the new value from
                // C#, then check SSMS. Record both, and explain in a comment why they differ?
                // ==========

                //nada.Gpa = 3.1;
                //Console.WriteLine($"Nada's Gpa in C#: {nada.Gpa}");

                // Evidence from SSMS: 
                //  id  FullName    YearOfStudy Gpa EnrollmentDate
                //  1	Yara Adel	    2	    3.4	2024-09-01 00:00:00.0000000
                //  2   Omar Hesham     3       2.8 2023 - 09 - 01 00:00:00.0000000
                //  3   Nada Samir      1       3.3 2025 - 09 - 01 00:00:00.0000000     Still not change
                //  4   Kareem Fouad    4       3.9 2022 - 09 - 01 00:00:00.0000000

                // they differ because didnt use SaveChanges() so DbContext didnt change the value in database as he dosent sense anychanges between them;


                // ==========
                // C.3 Save, re-check SSMS, and record the value now. In a comment, explain how EF knew to update
                // only Gpa when you never told it which property changed.
                // ==========

                //nada.Gpa = 3.1;
                //Console.WriteLine($"Nada's Gpa after changes: {nada.Gpa}");
                //await context.SaveChangesAsync();

                // Evidence from SSMS: 
                //  id  FullName    YearOfStudy Gpa EnrollmentDate
                //  1	Yara Adel	    2	    3.4	2024-09-01 00:00:00.0000000
                //  2   Omar Hesham     3       2.8 2023 - 09 - 01 00:00:00.0000000
                //  3   Nada Samir      1       3.1 2025 - 09 - 01 00:00:00.0000000     changed
                //  4   Kareem Fouad    4       3.9 2022 - 09 - 01 00:00:00.0000000

                // EF has a feature called "Change Tracker" which means that he has a snapshot of the retrieved object in memory
                // and when call "SaveChangesAsync()" check what difference between snapshot and obj in memory
                // and find the modified property then he modifies it in database.


                // ==========
                // C.4 Create a new student using your own real name, year 2, GPA = your derived value.
                // Save.Print the Id the database assigned. Record in a comment what Id was before the save.
                // ==========

                //var myStudent =new Student{ FullName = "Ahmed Sameh" ,Gpa = 3.1, YearOfStudy = 2 };
                //await context.Students.AddAsync(myStudent);
                //Console.WriteLine($"Id before database: {myStudent.Id}");   // Id before save : 0
                //await context.SaveChangesAsync();
                //Console.WriteLine($"Id after assigned to database: {myStudent.Id}");    //Id after save: 5

                // Evidence from SSMS: 
                //  id  FullName    YearOfStudy Gpa EnrollmentDate
                //  1	Yara Adel	    2	    3.4	2024-09-01 00:00:00.0000000
                //  2   Omar Hesham     3       2.8 2023 - 09 - 01 00:00:00.0000000
                //  3   Nada Samir      1       3.1 2025 - 09 - 01 00:00:00.0000000  
                //  4   Kareem Fouad    4       3.9 2022 - 09 - 01 00:00:00.0000000
                //  5   Ahmed Sameh     2       3.1 0001-01-01 00:00:00.0000000


                // ==========
                // C.5 Update your own student's YearOfStudy to 3, save, verify in SSMS?
                // ==========

                //var Ahmed = await context.Students.FirstOrDefaultAsync(s => s.Id == 5);
                //if (Ahmed != null)
                //{
                //    Ahmed.YearOfStudy = 3;
                //    await context.SaveChangesAsync();
                //}
                // Evidence from SSMS: 
                //  id  FullName    YearOfStudy Gpa EnrollmentDate
                //  1	Yara Adel	    2	    3.4	2024-09-01 00:00:00.0000000
                //  2   Omar Hesham     3       2.8 2023 - 09 - 01 00:00:00.0000000
                //  3   Nada Samir      1       3.1 2025 - 09 - 01 00:00:00.0000000  
                //  4   Kareem Fouad    4       3.9 2022 - 09 - 01 00:00:00.0000000
                //  5   Ahmed Sameh     3       3.1 0001-01-01 00:00:00.0000000         changer to 3


                // ==========
                // C.6 Delete your own student, save, verify in SSMS. In a comment, note why the remove method has
                // no Async version?
                // ==========

                //var Ahmed = await context.Students.FirstOrDefaultAsync(s => s.FullName =="Ahmed Sameh");
                //if(Ahmed != null)
                //    context.Students.Remove(Ahmed);
                //await context.SaveChangesAsync();

                // Evidence from SSMS: 
                //  id  FullName    YearOfStudy Gpa EnrollmentDate
                //  1	Yara Adel	    2	    3.4	2024-09-01 00:00:00.0000000
                //  2   Omar Hesham     3       2.8 2023 - 09 - 01 00:00:00.0000000
                //  3   Nada Samir      1       3.1 2025 - 09 - 01 00:00:00.0000000    
                //  4   Kareem Fouad    4       3.9 2022 - 09 - 01 00:00:00.0000000

                // Remove() has no Async version because it only changes the entity's state in EF Core's Change Tracker (marks it as Deleted).
                // It doesn't communicate  with the database, database DELETE operation happens when SaveChangesAsync() is called.



                // =============================================
                //                  PART D
                // =============================================

                // ==========
                // D.4 STOP — do not apply it yet. Open the migration file and answer in comments:
                // ==========
                //What operation does Up perform on FullName ? (Name it exactly.)
                //Answer: AlterColumn()<String>
                //What are nullable: and oldNullable: set to, and what does the difference mean?
                //Answer:Both were not nullable and difference (nullable: if true then can be null if false cant be null)
                //(OldNullable: if true it could put null value if false it couldnt put null value)
                //What two kinds of existing row could make this migration fail?
                //It fails when FullName already exist data exceds(100) and fails if CourseName already exist data exceds(150)
                //Run a SELECT in SSMS to check whether your table contains either kind.Paste the query and its result count?
                // FOR FULLNAME:
                //SELECT TOP (1000) [Id]
                //  ,[FullName]
                //  ,[YearOfStudy]
                //  ,[Gpa]
                //  ,[EnrollmentDate]
                // FROM[ITI_StudentPortalDB_EF].[dbo].[Students]
                // OUTPUT:
                //1   Yara Adel     2   3.4     2024 - 09 - 01 00:00:00.0000000
                //2   Omar Hesham   3   2.8     2023 - 09 - 01 00:00:00.0000000
                //3   Nada Samir    1   3.1     2025 - 09 - 01 00:00:00.0000000
                //4   Kareem Fouad  4   3.9     2022 - 09 - 01 00:00:00.0000000
                // FOR COURSENAME:
                //SELECT TOP(1000) [Id]
                //  ,[CourseName]
                //  ,[Credits]
                //FROM[ITI_StudentPortalDB_EF].[dbo].[Courses]
                // OUTPUT:
                //1   Web Development Using.NET  5
                //2   Database Fundamentals      5



                // ==========
                // D.5 Apply it with Update-Database. Verify the column type and nullability changed in SSMS.
                // ==========
                // Column type and nullability changed in SSMS
                //Id (PK, int, not null)
                //FullName(nvarchar(100), not null)
                //YearOfStudy(int, not null)
                //Gpa(float, not null)
                //EnrollmentDate(datetime2(7), not null)

                // ==========
                // D.6 Prove it works: inside a try/catch, try to save a student with a null FullName.
                // Catch the database - update exception and print a confirmation.Record the exception type in a comment.
                // ==========

                //try
                //{
                //    var student = new Student{FullName = null!,YearOfStudy = 2, Gpa = 3.5};
                //    await context.Students.AddAsync(student);
                //    await context.SaveChangesAsync();
                //}
                //catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
                //{
                //    // Exception type: DbUpdateException

                //    Console.WriteLine("Database rejected the null FullName as expected.");
                //    Console.WriteLine(ex.InnerException?.Message);
                //}
                // Exception:
                // Microsoft.Data.SqlClient.SqlException (0x80131904): Cannot insert the value NULL into column 'FullName',
                // table 'ITI_StudentPortalDB_EF.dbo.Students'; column does not allow nulls. INSERT fails.



                // =============================================
                //                  PART E
                // =============================================

                // Delete Behaviour : 8 % 2 = 0 ,   Then :DeleteBehavior.Restrict

                // ==========
                // E.1 Add InstructorId (matching your required/nullable choice above) and an Instructor
                // navigation property to Course.
                // ==========

                // Done

                // ==========
                // Follow E.2 In a comment, explain why keeping both would be a mistake.
                // ==========
                // Because now relationship represented by List of Courses no need for AssignedCourseName, if both still that could make duplicates.


                // ==========
                // E.3 Configure the relationship in OnModelCreating with HasOne/WithMany/HasForeignKey, and
                //OnDelete set to your derived behaviour.
                // ==========

                // Done

                // ==========
                // E.4 Add-Migration AddInstructorCourseRelationship. Read it before applying. In comments:
                // ==========
                // -List every operation Up performs?
                //  1. DropColumn() for "AssginedCourseName"
                //  2. AddColumn() for "Instructorid"
                //  3. CreateIndex() for "IX_Courses_InstructorId"
                //  4. AddForeignKey() For "FK_Courses_Instructors_InstructorId"
                // -One of them destroys data.Which one, and what data?
                //      DropColumn() destroys data in table Instructor the "AssginedCourseName"
                // -What would a real project do before that step ?
                //      make a script take data of AssginedCourseName to add it again in Courses List/

                // ==========
                // E.5 Apply it. In SSMS, confirm the InstructorId column exists and find the foreign-key constraint
                // under the table's Keys node. Record its exact name.
                // ==========
                // Done Update-Database
                // Table Course in Database:
                //  Id(PK, int, not null)
                //  CourseName(nvarchar(150), not null)
                //  Credits(int, not null)
                //  Instructorld(FK, int, not null)
                // ForeignKey: FK_Courses_Instructors_InstructorId


                // ==========
                // E.6 Link the "Web Development Using .NET" course to Hamdy by setting the FK property to his Id
                // (do not load or assign the navigation property). Save and verify in SSMS.
                // ==========

                //var dotnet = await context.Courses.FirstOrDefaultAsync(c => c.CourseName == "Web Development Using .NET");
                //if (dotnet != null)
                //{
                //    dotnet.InstructorId = (await context.Instructors.FirstOrDefaultAsync(i => i.FullName == "Hamdy")).Id;
                //    await context.SaveChangesAsync();
                //}
                // Evidence from SSMS Course Table:
                //  1   Web Development Using.NET  5   1
                //  2   Database Fundamentals      5   1
                // Evidence from SSMS Instructor Table:
                //  1   Hamdy   10
                //  2   Mona    5

                // ==========
                // E.7 Prove the constraint is real. In a try/catch, create a course with InstructorId = 9999
                // and save. Record the exception and the constraint name from its message. In a comment, state
                // what AssignedCourseName would have done with the same bad data.
                // ==========
                //try
                //{
                //    var courseTest = new Course { CourseName = "Test" ,Credits = 3, InstructorId = 9999};
                //    await context.Courses.AddAsync(courseTest);
                //    await context.SaveChangesAsync();
                //}
                //catch(Microsoft.EntityFrameworkCore.DbUpdateException ex)
                //{
                //    Console.WriteLine("Database rejected the value of InstructoprId as expected.");
                //    Console.WriteLine(ex.InnerException?.Message);
                //}
                // Exception:
                // ---> Microsoft.Data.SqlClient.SqlException (0x80131904): The INSERT statement conflicted with the FOREIGN KEY constraint "FK_Courses_Instructors_InstructorId".
                // The conflict occurred in database "ITI_StudentPortalDB_EF", table "dbo.Instructors", column 'Id'.

                // If AssignedCourseName would have accepted it.

                // =============================================
                //                  PART F
                // =============================================

                // Course Count : {8 % 3} + 2 = 4 

                // ==========
                // F.1 Create your number of extra courses, all assigned to Hamdy, so there's enough data for the
                // loading differences to be visible.Name them anything; record the count in a comment.
                // ==========
                //var hamdyId = (await context.Instructors.FirstOrDefaultAsync(i => i.FullName == "Hamdy")).Id;
                //var newCourses = new List<Course>
                //{
                //    new Course{CourseName = "C#" , Credits = 7, InstructorId = hamdyId},
                //    new Course{CourseName = "Machine Learning", Credits = 10 , InstructorId = hamdyId},
                //    new Course{CourseName = "C++", Credits = 5 , InstructorId = hamdyId},
                //    new Course{CourseName = "Django", Credits = 8 , InstructorId = hamdyId}
                //};
                //// Count of Courses of Hamdy = 5
                //await context.Courses.AddRangeAsync(newCourses);
                //await context.SaveChangesAsync();

                // ==========
                // F.2 Enable SQL logging in OnConfiguring?
                // ==========
                // Done

                // ==========
                // F.3 Load all instructors without Include, and loop printing each one's name and
                // Courses.Count.Record: the counts printed, and how many SQL queries the log shows.
                // ==========

                //var allInstructors = await context.Instructors.ToListAsync();
                //foreach(var instructor in allInstructors)
                //{
                //    Console.WriteLine($"{instructor.FullName}: {instructor.Courses.Count()} Courses");
                //}
                // Only one query
                // Hamdy: 0 Courses
                // Mona : 0 Courses

                // ==========
                // F.4 Load them again with Include, loop with a nested inner loop printing course names. Record
                // the counts and the query count from the log.
                // ==========

                //var allInstructorsInc = context.Instructors.Include(i => i.Courses);

                //foreach (var instructor in allInstructorsInc)
                //{
                //    Console.WriteLine($"{instructor.FullName}: {instructor.Courses.Count()} Courses");
                //    foreach(var course in instructor.Courses)
                //    {
                //        Console.WriteLine(course.CourseName);
                //    }
                //}
                // 2 Queries
                // Hamdy: 6 Courses
                // Mona : 0 Courses

                // ==========
                // F.5 In a comment, answer: the Include version returned more rows from SQL Server than there are
                // instructors.Explain why, and what EF did with the duplicates.
                // ==========
                //because SQL Server uses a Join to retrieve instructors and their courses If an instructor has multiple courses,
                //the instructor's data is repeated once for each course row in the SQL result.
                // EF Core removes these duplicates and creates one Instructor entity with a Courses collection containing all related Course entities.

                // ==========
                // F.6 Use explicit loading on a single instructor: load them, print Courses.Count, then load the
                // collection deliberately and print it again. Record both counts and the query count.
                // ==========

                //var instructorExplicit = await context.Instructors.FirstAsync();
                //Console.WriteLine($"Before loading courses: {instructorExplicit.Courses.Count}");
                // 0 Courses and 1 Query
                //await context.Entry(instructorExplicit)
                //    .Collection(i => i.Courses)
                //    .LoadAsync();

                //Console.WriteLine($"After loading courses: {instructorExplicit.Courses.Count}");
                // 6 Courses and 2 Query

                // ==========
                // F.7 Load all students with AsNoTracking(). Then change one and call SaveChangesAsync(). Record
                // what happened in SSMS and explain why.
                // ==========

                //var allStudents = context.Students.AsNoTracking();

                //var oneStudent = await allStudents.FirstOrDefaultAsync();

                //Console.WriteLine($"Before Change: {oneStudent.FullName}");     // The student is :Yara Adel
                //oneStudent.FullName = "Hamada";
                //await context.SaveChangesAsync();
                //Console.WriteLine($"After Change: {oneStudent.FullName}");
                //output in SSMS:
                //  1	Yara Adel	    2	3.4	    2024-09-01 00:00:00.0000000
                //  2   Omar Hesham     3   2.8     2023 - 09 - 01 00:00:00.0000000
                //  3   Nada Samir      1   3.1     2025 - 09 - 01 00:00:00.0000000
                //  4   Kareem Fouad    4   3.9     2022 - 09 - 01 00:00:00.0000000

                // Nothing Change in database because we used AsNoTracking() and this method make the return collecton as read only it only change in memory but EF dosnt change it in database.


                // =============================================
                //                  PART G
                // =============================================

                // ==========
                // G.1 State your Lab ID and all three derived values, showing the arithmetic. LabId: 8
                // ==========
                // 1. PART C {3.0 + ((your Lab ID mod 7) * 0.1)} = 3.1
                // 2. PART E your Lab ID mod 2 = 0
                // 3. PART F ((your Lab ID mod 3) + 2) = 4

                // ==========
                // G.2 Answer the OnDelete question your Part E row assigned you (see the table).
                // ==========
                // DeleteBehavior.Restrict protects courses because it prevents deleting an Instructor while there are courses that still reference that instructor.

                // ==========
                // G.3 Did any migration fail today? If yes: what failed, exactly which commands you used to roll
                // back, and what you changed before re-applying.If no: write "no rollback needed" — but also
                // state the two commands you would have used.
                // ==========
                // When Add-Migration AddStudentConstraints it fails when open it found that Creating database from scratch, so i use Update-Database <the previous version> and them Remove-Migration


                // ==========
                // G.4 Session 13's multiple enumeration and today's N+1 are described as the same bug in different
                // clothes.Explain what they actually have in common, using your own query counts from Part F as evidence.
                // ==========

                // when call everytime the same query or query that count that runs the queries like in N+1 when loop on a query everytime in loop runs it thats make waste of time and space in memory
                // But in PART F we used include and explicit loading those help in saving that memory and time waste by calling the query only when needed.
                // 



            }

            Console.WriteLine();
            Console.WriteLine("Done.");

        }
    }
}
