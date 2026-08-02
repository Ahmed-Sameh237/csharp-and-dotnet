using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortalWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace StudentPortalWeb.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentPortalContext _context;
        public StudentsController(StudentPortalContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _context.Students
                .OrderBy(s => s.FullName)
                .ToListAsync();

            return View(students);
        }

        // =======================
        //         PART C
        // =======================
        public async Task<IActionResult> Top(int count)
        {

                var students = await _context.Students
                .OrderByDescending(s => s.Gpa)
                .Take(count)
                .ToListAsync();

            return View("Index",students);
        }

        // =======================
        //         PART D
        // =======================
        public async Task<IActionResult> Intake(string code)
        {
            var students = await _context.Students
                .OrderBy(s => s.FullName)
                .ToListAsync();

            return View("Index", students);
        }

        // =======================
        //         PART E
        // =======================
        // /Students/About returns 404 because this action uses attribute routing,
        // so it is only reachable through the route "about/ahmed" and not the default route.
        //  URL                     Result
        // /about/ahmed	            works: Students with GPA ≥ 3.5
        // /about/ahmed?minGpa=3.9   works: Fewer students
        // /Students/About	        404
        [Route("about/ahmed")]
        public async Task<IActionResult> About([FromQuery] double minGpa =3.5)
        {
            var students = await _context.Students
                .Where(s => s.Gpa >= minGpa)
                .OrderBy(s => s.FullName)
                .ToListAsync();

            return View("Index", students);
        }
        public async Task<IActionResult> Details(int id)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student is null)
            {
                // The route matched, the action ran, the row did not
                // exist. That is a genuinely different failure from a URL
                // the route table refused, and Block 3 makes you tell
                // them apart from the console log alone.
                return NotFound();
            }

            return View(student);
        }

        public async Task<IActionResult> ByYear(int year)
        {
            var students = await _context.Students
                .Where(s => s.YearOfStudy == year)
                .OrderBy(s => s.FullName)
                .ToListAsync();

            ViewData["Year"] = year;

            return View(students);
        }

        public async Task<IActionResult> Honours(string band)
        {
            // Guard clause before work — and note that this is NOT redundant
            // with the route constraint, even though the constraint makes it
            // unreachable through the honours route. The default route can
            // still reach this action as /Students/Honours with no band at
            // all, and an action must never assume it was only ever called
            // the way you intended. Constraints filter URLs; guard clauses
            // protect behaviour. Both, always.
            if (string.IsNullOrWhiteSpace(band))
            {
                return NotFound();
            }

            IQueryable<Student> query = _context.Students;

            if (string.Equals(band, "first", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(s => s.Gpa >= 3.5);
            }
            else if (string.Equals(band, "second", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(s => s.Gpa >= 3.0 && s.Gpa < 3.5);
            }
            else
            {
                query = query.Where(s => s.Gpa < 3.0);
            }

            var students = await query
                .OrderBy(s => s.FullName)
                .ToListAsync();

            ViewData["Band"] = band.ToLowerInvariant();

            return View(students);
        }

        [Route("students/search")]
        public async Task<IActionResult> Searching([FromQuery] string name)
        {
            IQueryable<Student> query = _context.Students;

            // Guard clause before work: an empty search box is not an
            // error, it just means "no filter". Passing null straight
            // into Contains would throw instead.
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(s => s.FullName.Contains(name));
            }

            var students = await query
                .OrderBy(s => s.FullName)
                .ToListAsync();

            ViewData["Name"] = name;

            return View(students);
        }
    }
}
