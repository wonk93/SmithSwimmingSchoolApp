using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmithSwimmingSchoolApp.Data;
using SmithSwimmingSchoolApp.Models;
using X.PagedList.Extensions;

namespace SmithSwimmingSchoolApp.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id, int? page, string searchName)
        {
            ViewData["id"] = id;


            var vm =  _context.Reports.Include(a => a.Enrollment!.Swimmer).Where(a => a.Enrollment!.Id == id).AsQueryable();
            if (!string.IsNullOrEmpty(searchName))
            {
                vm = vm.Where(c => c.Enrollment!.Swimmer!.Name!.Contains(searchName));
            }          

            ViewData["SearchName"] = searchName;

            int pageSize = 5;
            int pageNumber = page ?? 1;
            return View(vm.ToPagedList(pageNumber, pageSize));
        }
        public async Task<IActionResult> AddReport(int id)
        {
            var enrollment = await _context.Enrollments
                .Include(e => e.Swimmer)
                .FirstOrDefaultAsync(e => e.Swimmer.Id == id);

            if (enrollment == null)
            {
                return NotFound();
            }
           
            var model = new Report
            {
                EnrollmentId = enrollment.Id,
                Enrollment = enrollment
            };

            ViewData["id"] = id;
            return View("AddReport", model); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReport(int id, Report report)
        {
            if (ModelState.IsValid)
            {
                report.EnrollmentId = id;
                report.Id = 0;
                _context.Add(report);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), new { id = id });
            }

            ViewData["id"] = id; 
            return View(report);
        }
    }
}
