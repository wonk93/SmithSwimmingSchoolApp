using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmithSwimmingSchoolApp.Data;
using SmithSwimmingSchoolApp.Models;
using SmithSwimmingSchoolApp.ViewModel;
using X.PagedList.Extensions;

namespace SmithSwimmingSchoolApp.Controllers
{
   
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;            
        }
        
        public IActionResult Index(int? page, string searchName)
        {
            var applicationDbContext = _context.Courses.Include(c => c.Coach).AsQueryable();
            if (!string.IsNullOrEmpty(searchName))
            {
                applicationDbContext = applicationDbContext.Where(c => c.Title!.Contains(searchName));
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

            var course = await _context.Courses
                .Include(c => c.Coach)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        public async Task<IActionResult> AllSwimmers(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           
            var enrollments = await _context.Enrollments
                .Include(e => e.Swimmer) 
                .Include(e => e.Course) 
                .Where(e => e.CourseId == id) 
                .ToListAsync();

            if (!enrollments.Any())
            {
                return NotFound();
            }
            
            var course = enrollments.FirstOrDefault()?.Course;
          
            var viewModel = new SwimmerInCourseViewModel
            {
                Course = course,
                Swimmer = null
            };
            ViewBag.Swimmers = enrollments.Select(e => e.Swimmer).ToList(); 
            return View(viewModel);
        }
        
        public IActionResult Create()
        {
            ViewData["CoachName"] = new SelectList(_context.Coaches, "Id", "Name");
            ViewData["LevelCourse"] = new SelectList(_context.Courses, "LevelCourse", "LevelCourse");
            return View();
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CoachId,Title,Coach,LevelCourse")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoachName"] = new SelectList(_context.Coaches, "Id", "Name", course.Coach!.Name);
            ViewData["LevelCourse"] = new SelectList(_context.Courses, "LevelCourse", "LevelCourse", course.LevelCourse);
            return View(course);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["CoachName"] = new SelectList(_context.Coaches, "Id", "Name");
            ViewData["LevelCourse"] = new SelectList(_context.Courses, "LevelCourse", "LevelCourse");
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CoachId, Title, Coach, LevelCourse")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
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
            ViewData["CoachName"] = new SelectList(_context.Coaches, "Id", "Name", course.Coach!.Name);
            ViewData["LevelCourse"] = new SelectList(_context.Courses, "LevelCourse", "LevelCourse", course.LevelCourse);
            return View(course);
        }
       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Coach)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
