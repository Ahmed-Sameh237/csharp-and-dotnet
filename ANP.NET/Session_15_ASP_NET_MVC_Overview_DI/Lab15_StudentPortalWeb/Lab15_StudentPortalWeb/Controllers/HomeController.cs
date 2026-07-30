using Lab15_StudentPortalWeb.Models;
using Lab15_StudentPortalWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Lab15_StudentPortalWeb.Controllers
{
    public class HomeController : Controller
    {
        
        // ############### PART C.5 ################
        private readonly StudentPortalContext _context;

        // ############### PART E.5 ################
        private readonly IAhmedStampService _stampA;
        private readonly IAhmedStampService _stampB;
        public HomeController(StudentPortalContext context, IAhmedStampService stampA, IAhmedStampService stampB)
        {
            _context = context;
            _stampA= stampA;
            _stampB = stampB;
        }

        public async Task<IActionResult> Index()
        {

            ViewBag.Owner = _stampA.Owner;
            ViewBag.StampA = _stampA.Stamp;
            ViewBag.StampB = _stampB.Stamp;

            // ##################### PART C.6 #################
            var students = await _context.Students.OrderBy(s => s.FullName).ToListAsync();
            return View(students);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
