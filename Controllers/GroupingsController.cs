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
    public class GroupingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GroupingsController(ApplicationDbContext context)
        {
            _context = context;
        }
             
        public IActionResult Index(int? page, string searchName)
        {
            var grouping = _context.Groupings.AsQueryable();

            if (!string.IsNullOrEmpty(searchName))
            {
                grouping = grouping.Where(c => c.Level!.Contains(searchName));
            }

            ViewData["SearchName"] = searchName;

            int pageSize = 5;
            int pageNumber = page ?? 1;

            return View(grouping.ToPagedList(pageNumber, pageSize));            
        }
       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grouping = await _context.Groupings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grouping == null)
            {
                return NotFound();
            }

            return View(grouping);
        }
       
        public IActionResult Create()
        {            
            var levels = new List<SelectListItem>
            {
                new SelectListItem { Value = "Beginner", Text = "Beginner" },
                new SelectListItem { Value = "Intermediate", Text = "Intermediate" },
                new SelectListItem { Value = "Advanced", Text = "Advanced" }
            };

            ViewData["LevelList"] = new SelectList(levels, "Value", "Text");
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Level,StartDate,EndDate,StartTime,Places")] Grouping grouping)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grouping);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            var levels = new List<SelectListItem>
            {
                new SelectListItem { Value = "Beginner", Text = "Beginner" },
                new SelectListItem { Value = "Intermediate", Text = "Intermediate" },
                new SelectListItem { Value = "Advanced", Text = "Advanced" }
            };

            ViewData["LevelList"] = new SelectList(levels, "Value", "Text");
            return View(grouping);
        }
       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grouping = await _context.Groupings.FindAsync(id);
            if (grouping == null)
            {
                return NotFound();
            }
            return View(grouping);
        }
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Level,StartDate,EndDate,StartTime,Places")] Grouping grouping)
        {
            if (id != grouping.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grouping);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupingExists(grouping.Id))
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
            return View(grouping);
        }
    
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grouping = await _context.Groupings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grouping == null)
            {
                return NotFound();
            }

            return View(grouping);
        }
  
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grouping = await _context.Groupings.FindAsync(id);
            if (grouping != null)
            {
                _context.Groupings.Remove(grouping);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupingExists(int id)
        {
            return _context.Groupings.Any(e => e.Id == id);
        }
    }
}
