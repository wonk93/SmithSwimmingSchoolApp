using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmithSwimmingSchoolApp.Data;
using SmithSwimmingSchoolApp.Models;
using X.PagedList.Extensions;

namespace SmithSwimmingSchoolApp.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? page, string searchName)
        {
            var applicationDbContext = _context.Enrollments.Include(e => e.Course).Include(e => e.Grouping).Include(e => e.Swimmer).AsQueryable();
            if (!string.IsNullOrEmpty(searchName))
            {
                applicationDbContext = applicationDbContext.Where(c => c.Swimmer!.Name!.Contains(searchName));
            }
            ViewData["SearchName"] = searchName;
            int pageSize = 5;
            int pageNumber = page ?? 1;
            return View(applicationDbContext.ToPagedList(pageNumber, pageSize));
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Grouping)
                .Include(e => e.Swimmer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }
        
        public async Task<IActionResult> Create()
        {
            var user = User.Identity?.Name;
            var swimmer = await _context.Swimmers.SingleOrDefaultAsync(s => s.IdentityUser!.Email == user);

            if (swimmer == null)
            {
                return NotFound("Swimmer not found.");
            }

            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Title");
            ViewData["GroupingId"] = new SelectList(_context.Groupings, "Id", "Level");         
            ViewData["SwimmerId"] = swimmer.Id;
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseId,SwimmerId,GroupingId")] Enrollment enrollment)
        {
            var group = await _context.Groupings.FindAsync(enrollment.GroupingId);
            if (group != null && group.Places > 0)
            {
                _context.Enrollments.Add(enrollment);

                group.Places--;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
            
        }
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Grouping)
                .Include(e => e.Swimmer)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (enrollment == null)
            {
                return NotFound();
            }

            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Title", enrollment.CourseId);
            ViewData["GroupingId"] = new SelectList(_context.Groupings, "Id", "Level", enrollment.GroupingId);
            ViewData["SwimmerId"] = new SelectList(_context.Swimmers, "Id", "Name", enrollment.SwimmerId);
            return View(enrollment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,SwimmerId,GroupingId")] Enrollment enrollment)
        {
            if (id != enrollment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Title", enrollment.CourseId);
            ViewData["GroupingId"] = new SelectList(_context.Groupings, "Id", "Level", enrollment.GroupingId);
            ViewData["SwimmerId"] = new SelectList(_context.Swimmers, "Id", "Name", enrollment.SwimmerId);
            return View(enrollment);
        }
       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Grouping)
                .Include(e => e.Swimmer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollments.Any(e => e.Id == id);
        }
    }
}
