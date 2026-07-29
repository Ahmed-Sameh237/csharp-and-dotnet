using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using StudentPortalConsole;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

//==================== PART F.3 =====================
//1- Up() creates the following tables:
// Courses
// Instructors
// Students

//2- EF choose:
// Gpa- (double)
// FullName- (string) (nvarchar(max))

//3- FullName is nullable.
//   I did not tell EF that.
//   EF inferred it from the entity model and conventions.

//4- Down() drops all the tables created by Up().





namespace StudentPortalConsole
{

    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public int YearOfStudy { get; set; }
        public double Gpa { get; set; }
        public DateTime EnrollmentDate { get; set; }  // LabId :8 , so own property number 2 (8 % 3 = 2)
    }

    public class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; } = "";
        public int Credits { get; set; }
    }

    public class Instructor
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public int YearsOfExperience { get; set; }
        public string? AssignedCourseName { get; set; }
    }

    // ========================== PART E.5 ==========================

    public static class StudentQueryExtensions
    {
        public static IEnumerable<Student> MyTopStudents(this IEnumerable<Student> source)
        {
            return source.Where(s => s.Gpa >= 2.5);
        }
    }

    // ======================= PART F.1 ============================
    public class StudentPortalContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=.;Database=ITI_StudentPortalDB_EF;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }



    internal class Program
    {

        static void Main(string[] args)
        {

            List<Student> students = new List<Student>
            {
                new Student { FullName = "Yara Adel",    YearOfStudy = 2, Gpa = 3.5 },
                new Student { FullName = "Omar Hesham",  YearOfStudy = 3, Gpa = 2.8 },
                new Student { FullName = "Nada Samir",   YearOfStudy = 1, Gpa = 3.9 },
                new Student { FullName = "Kareem Fouad", YearOfStudy = 4, Gpa = 3.2 }
            };

            List<Instructor> instructors = new List<Instructor>
            {
                new Instructor { FullName = "Hamdy",       YearsOfExperience = 10,
                                 AssignedCourseName = "Web Development Using .NET" },
                new Instructor { FullName = "Mona Khalil", YearsOfExperience = 6,
                                 AssignedCourseName = "Database Fundamentals" }
            };

            List<Course> courses = new List<Course>
            {
                new Course { CourseName = "Web Development Using .NET", Credits = 4 },
                new Course { CourseName = "Database Fundamentals",      Credits = 3 }
            };

            Console.WriteLine();
            Console.WriteLine("======= PART B =======");

            Console.WriteLine("====== B.1 ======");
            // B1.What does this print, and why?
            List<Student> empty = new List<Student>();
            Console.WriteLine(empty.Count());           // output: 0
            Console.WriteLine(empty.Any());             // output: False
            //Console.WriteLine(empty.Average(s => s.Gpa)); // throw an expection "InvalidOperationException"

            Console.WriteLine("====== B.2 ======");
            // B2. With today's four students, what does this print — and is the order what you'd expect?
            foreach (var g in students.GroupBy(s => s.YearOfStudy))
            {
                Console.Write($"{g.Key} ");
            }
            // output:
            // 2 3 1 4
            // not the order i would expect because groupby() just groups what comes first


            Console.WriteLine();
            Console.WriteLine("====== B.3 ======");
            // B3. What number prints?
            //var q = students.Where(s => s.YearOfStudy >= 3);
            //students.Add(new Student { FullName = "Test Person", YearOfStudy = 3, Gpa = 3.0 });
            //Console.WriteLine(q.Count());
            // prints : 3

            Console.WriteLine();
            Console.WriteLine("======= PART C =======");

            // LabId=8,  2.5 + ( {8 % 4} * 0.3 ) = 2.5  GPA threshold


            Console.WriteLine("======= C.1 =======");
            Console.WriteLine($"Students count: {students.Count()}");
            Console.WriteLine($"Students count above threshold Gpa: {students.Count(s => s.Gpa > 2.5)}");
            Console.WriteLine($"Students Avg Gpa: {students.Average(s => s.Gpa):F2}");
            Console.WriteLine($"Students Highest Gpa: {students.Max(s => s.Gpa)}");
            Console.WriteLine($"Students Lowest Gpa: {students.Min(s => s.Gpa)}");
            Console.WriteLine($"Is any student failed: {students.Any(s => s.Gpa < 2.0)}");
            Console.WriteLine($"Are all students success: {students.All(s => s.Gpa >= 2.0)}");

            Console.WriteLine("======= C.2 =======");
            List<Student> emptyStudents = new List<Student>();
            Console.WriteLine($"emptyList count: {emptyStudents.Count()}");     // 0
            Console.WriteLine($"emptyList any: {emptyStudents.Any(s => s.Gpa > 2.0)}");         // False
            //Console.WriteLine($"emptyList Avg: {emptyStudents.Average(s => s.Gpa)}");   // InvalidOperationException

            Console.WriteLine("======= C.3 =======");
            if (emptyStudents.Any())
            {
                Console.WriteLine($"emptyList Avg: {emptyStudents.Average(s => s.Gpa)}");
            }
            else
            {
                Console.WriteLine("Cannot calculate average because the collection is empty.");
            }

            Console.WriteLine("======= C.4 =======");
            var groupedStudents = students.GroupBy(s => s.YearOfStudy);
            foreach (var g in groupedStudents)
            {
                Console.WriteLine($"Group Key:{g.Key}, Group count: {g.Count()}");
                foreach (var student in g)
                {
                    Console.WriteLine($"Student name:{student.FullName}");
                }
            }
            // The groups came out not sorted because GroupBy() dosnt order the keys it just takes first value input 


            Console.WriteLine("======= C.5 =======");
            foreach (var group in students.GroupBy(s => s.Gpa > 2.5 ? "AboveThreshold" : "BelowThreshold"))
            {
                Console.WriteLine($"GroupKey: {group.Key} - GroupCount: {group.Count()}");
                foreach (var student in group)
                {
                    Console.WriteLine($"GroupMember:{student.FullName}");
                }
            }

            Console.WriteLine("======= C.6 =======");

            var groupedStudentsQ6 = students.GroupBy(s => s.YearOfStudy).OrderBy(g => g.Key);
            foreach (var g in groupedStudentsQ6)
            {
                Console.WriteLine($"Group Key:{g.Key}, Group count: {g.Count()}");
                foreach (var student in g)
                {
                    Console.WriteLine($"Student name:{student.FullName}");
                }
            }
            // Added OrderBy(g => g.Key) this sort them by order of groupKey



            Console.WriteLine();
            Console.WriteLine("======= PART D =======");

            //LabId= 8,    ( 8 % 5 ) + 3 = 6    Instructor's exepeirence value = 6

            Console.WriteLine("======= D.1 =======");

            var instructorTeaches = instructors.Join(
                courses,
                i => i.AssignedCourseName,
                c => c.CourseName,
                (i, c) => $"Instructor:{i.FullName} Teaches Course:{c.CourseName}( credits:{c.Credits})"
                );
            foreach (var i in instructorTeaches)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("======= D.2 =======");

            var instructorTeachesQuery = from i in instructors
                                         join c in courses on i.AssignedCourseName equals c.CourseName
                                         select $"{i.FullName} teach {c.CourseName} ({c.Credits} Credits)";
            foreach (var i in instructorTeachesQuery)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("======= D.3 and D.4 =======");
            var iamInstructor = new Instructor { FullName = "Ahmed" , YearsOfExperience = 6 , 
            AssignedCourseName = "Machine Learning"};
            instructors.Add(iamInstructor);
            //From D.1
            foreach (var i in instructorTeaches)
            {
                Console.WriteLine(i);
            }
            // Same output 2 only while 3 input thats because the join made on CourseName and the "Machine Learning" course
            // dosent exit in courses

            Console.WriteLine("======= D.5 =======");
            Console.WriteLine("If want to appear Instructors assigned to courses not exist in courses list");
            Console.WriteLine("Use LeftJoin or GroupJoin with DefaultIfEmpty() thats a C#");


            Console.WriteLine();
            Console.WriteLine("======= PART E =======");


            Console.WriteLine("======= E.1 =======");
            // The count will be 4 because the query is deferred.
            // Layla will be included when Count() executes.
            var query = students.Where(s => s.Gpa > 3.0);
            var newStudent = new Student{ FullName = "Layla Mostafa", YearOfStudy = 2,Gpa = 3.7 };
            students.Add(newStudent);
            Console.WriteLine(query.Count()); // i was right output: 4

            Console.WriteLine("======= E.2 =======");
            students.RemoveAll(s => s.FullName == "Layla Mostafa");
            Console.WriteLine("Layla removed from list");

            Console.WriteLine("======= E.3 =======");
            var highStudents = students.Where(s => s.Gpa > 3.0);
            Console.WriteLine(highStudents.Count());

            foreach (var s in highStudents)
            {
                Console.WriteLine(s.FullName);
            }
            Console.WriteLine(highStudents.Average(s => s.Gpa));
            // Run 3 Times in each one of them : Count() , loop , Avg


            Console.WriteLine("======= E.4 =======");
            var highStudentsList = students.Where(s => s.Gpa > 3.0).ToList();
            Console.WriteLine(highStudents.Count());
            foreach (var s in highStudents)
            {
                Console.WriteLine(s.FullName);
            }
            Console.WriteLine(highStudents.Average(s => s.Gpa));
            // add to the query .ToList() so now only one time query execute and save it in the variable as a list
            // when call in each one of the 3 times only read the list.


            Console.WriteLine("======= E.5 =======");
            var studentsAboveMyThreshold = students.
                MyTopStudents()
                .OrderBy(s => s.FullName)
                .Select(s => s.FullName)
                .ToList();
            foreach (var s in studentsAboveMyThreshold)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine("======= E.6 =======");
            Console.WriteLine("MyTopStudents is deferred as using Where() so it inherates it for free");


            Console.WriteLine();
            Console.WriteLine("======= PART F =======");


            Console.WriteLine("======= F.1 =======");
            Console.WriteLine("StudentPortalContext Completed");

            Console.WriteLine("======= F.2 =======");
            Console.WriteLine("Add-Migration Completed");

            Console.WriteLine("======= F.3 =======");
            Console.WriteLine("Questions Answered at top of Program.cs");

            Console.WriteLine("======= F.4 =======");
            Console.WriteLine("Yes, ITI_StudentPortalDB_EF  Dose not exist yey");
            Console.WriteLine("Because Add-Migration just making file all its instructions pending waiting for call Update-databas");

            Console.WriteLine("======= F.5 =======");
            Console.WriteLine("Update-database Done");

            Console.WriteLine("======= F.6 =======");
            Console.WriteLine("Student Table at session 3 has : Email and JoinDate columns");
            Console.WriteLine("Student Table at today session has: YearOfStudy and Gpa columns");



            Console.WriteLine();
            Console.WriteLine("======= PART G =======");

            Console.WriteLine("======= G.1 =======");
            Console.WriteLine("Seed Database Completed");

            Console.WriteLine("======= G.2 =======");
            Console.WriteLine("Property to Student entity added");

            Console.WriteLine("======= G.3 =======");
            Console.WriteLine("Add-Migration done");

            Console.WriteLine("======= G.4 =======");
            Console.WriteLine("Up() doing AddColumn method");
            Console.WriteLine("because we are not creating a new table it just a new column in a table and a default value for this column for all data inside");

            Console.WriteLine("======= G.5 =======");
            Console.WriteLine("Update-database done");

            Console.WriteLine("======= G.6 =======");
            Console.WriteLine("New Column exist and data is still exist");


            Console.WriteLine();
            Console.WriteLine("======= PART H =======");

            Console.WriteLine("======= H.1 =======");
            //using (StudentPortalContext context = new StudentPortalContext())
            //{
            //    if (!context.Students.Any())
            //    {
            //        context.Students.AddRange(
            //            new Student{ FullName = "Yara Adel",YearOfStudy = 2,Gpa = 3.4,EnrollmentDate = new DateTime(2024, 9, 1)},
            //            new Student{FullName = "Omar Hesham",YearOfStudy = 3,Gpa = 2.8,EnrollmentDate = new DateTime(2023, 9, 1)},
            //            new Student{FullName = "Nada Samir",YearOfStudy = 1,Gpa = 3.3,EnrollmentDate = new DateTime(2025, 9, 1)},
            //            new Student{FullName = "Kareem Fouad",YearOfStudy = 4,Gpa = 3.9,EnrollmentDate = new DateTime(2022, 9, 1)}

            //        );
            //        context.SaveChanges();
            //    }
            //}
            using (StudentPortalContext context = new StudentPortalContext())
            {
                var allStudents = context.Students.ToList();

                foreach (var s in allStudents)
                {
                    Console.WriteLine($"{s.FullName} - GPA: {s.Gpa}");
                }
            }

            Console.WriteLine("======= H.2 =======");

            using (StudentPortalContext context = new StudentPortalContext())
            {

                var topStudents = context.Students
                    .Where(s => s.Gpa > 3.0)
                    .OrderByDescending(s => s.Gpa)
                    .Select(s => s.FullName)
                    .ToList();

                foreach (var s in topStudents)
                {
                    Console.WriteLine(s);
                }
            }

            Console.WriteLine("======= H.3 =======");

            using (StudentPortalContext context = new StudentPortalContext())
            {
                Console.WriteLine($"Students Count:{context.Students.Count()}");
                Console.WriteLine($"Students AvgGpa:{context.Students.Average(s => s.Gpa)}");
            }

            Console.WriteLine("======= H.4 =======");
            //context.Students.Where(s => s.Gpa > 3.0).ToList();    // This query filter students that has gpa > 3.0 then retrieves them from database;
            //context.Students.ToList().Where(s => s.Gpa > 3.0);    // This query retrieves all students into memory then filter them with Where();



            // ================ PART I =====================
            // ============= I.1 =============
            // LabId = 8, then Drived  values:
            // 1- PART C GPA threshold : 2.5 ( {8 % 4} * 0.3) = 2.5
            // 2- PART D Years Of Experience ( {8 % 5} + 3 ) = 6
            // 3- PART G Property 8 % 3 = 2     EntrollmentDate Property

            // ============= I.2 =============
            // PART D
            //silently missing join row is more dangerous than a crash?
            //because the program continues to run without showing any error.
            //This can make the user believe the results are complete even though some matching data is missing.

            // ============= I.3 =============
            // PART F
            //Add-Migration and Update-Database are separate commands because it is safer?
            //Add - Migration only creates a migration file that lets me review the changes
            //before anything happens to the database.After checking that the migration is correct
            //I run Update - Database to apply those changes.
            //This separation helps prevent accidental database changes or data loss.

            // ============== I.4 =============
            // PART H
            //The LINQ query I wrote against the database looks the same as the LINQ query I used with a List?
            //But EF Core translates the query into SQL and executes it on SQL Server instead of running it directly on objects in memory.
            //Deferred execution is more important with a database because the SQL query is not sent until the query is executed.

        }
    }
}
