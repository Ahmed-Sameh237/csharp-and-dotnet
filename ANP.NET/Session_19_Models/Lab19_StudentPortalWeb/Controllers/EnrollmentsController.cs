// LAB 19 — Lab ID: 8 | MIN_GRADE_LAB = 1.0 | COURSE_COUNT = 4
//
// CoursesController.Index only needs Include(c => c.Enrollments)
// because it only loads the Enrollment records.
// CoursesController.Details needs ThenInclude because it also loads
// the related Student objects inside each Enrollment.

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StudentPortalWeb.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace StudentPortalWeb.Controllers
{
    public class EnrollmentsController : Controller
    {

        private readonly StudentPortalContext _context;

        public EnrollmentsController(StudentPortalContext context)
        {
            _context= context;
        }

        public IActionResult Index()
        {
            return View();
        }
        // ==========================
        //          PART B
        // ==========================

        // The Create form contains Student and Course dropdowns, so the action must query the database to load those lists
        // before displaying the form. at Session 17 we make a create() Get for student didnt query because no need to load the students.

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var students = await _context.Students
                .OrderBy(s => s.FullName)
                .ToListAsync();
            var courses = await _context.Courses
                .OrderBy(c => c.CourseName)
                .ToListAsync();
            ViewData["Students"] = students;
            ViewData["Courses"] = courses;
            return View();
           
        }

        // ==========================
        //          PART C
        // ==========================
        // EnrollmentDate is set in the controller because it must use the server's current time and should not be trusted from user input.

        [HttpPost]
        public async Task<IActionResult> Create(Enrollment enrollment)
        {
            if (!ModelState.IsValid)
            {
              
                var students = await _context.Students
                .OrderBy(s => s.FullName)
                .ToListAsync();
                var courses = await _context.Courses
                    .OrderBy(c => c.CourseName)
                    .ToListAsync();
                ViewData["Students"] = students;
                ViewData["Courses"] = courses;
                return View(enrollment);
            }
            enrollment.EnrollmentDate = DateTime.Now;
            await _context.Enrollments.AddAsync(enrollment);
            await _context.SaveChangesAsync();

            var student = await _context.Students.FindAsync(enrollment.StudentId);
            var course = await _context.Courses.FindAsync(enrollment.CourseId);
            if (student != null && course != null)
            {
                TempData["Success"] = $"{student.FullName} enrolled in {course.CourseName} successfully.";
            }
            return RedirectToAction("Details","Students", new { id = enrollment.StudentId });
        }

        // ==========================
        //      Follow PART D
        // ==========================

        //Blank Grade: Accepted because Grade is nullable, so the value is stored as NULL and[Range] is not applied.
        //Grade below 1.0: Rejected because a value was provided, and it does not satisfy the [Range(1.0, 4.0)] validation rule.

        // ==========================
        //      PART E
        // ==========================
        // When create duplicate enrollment: 
        // SqlException: Cannot insert duplicate key row in object 'dbo.Enrollments' with unique index 'IX_Enrollments_StudentId_CourseId'.
        //      The duplicate key value is (7, 4).

        //what real HTTP/database behaviour did you observe when the duplicate insert was attempted,
        //and does it match what Block 5's console demo showed?

        // Attempting to insert a duplicate enrollment caused the database to reject the insert
        // because of the unique index, matching the behavior demonstrated in Block 5 where the database enforced uniqueness.


    }



}
