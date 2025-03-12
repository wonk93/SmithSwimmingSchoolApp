using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmithSwimmingSchoolApp.Data;
using SmithSwimmingSchoolApp.Models;
using X.PagedList.Extensions;

namespace SmithSwimmingSchoolApp.Controllers
{
    public class SwimmersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SwimmersController(ApplicationDbContext context)
        {
            _context = context;
        }
       
        public ActionResult Index(int? page, string searchName)
        {
            
            var applicationDbContext = _context.Swimmers.Include(s => s.IdentityUser).AsQueryable();
            if (!string.IsNullOrEmpty(searchName))
            {
                applicationDbContext = applicationDbContext.Where(c => c.Name!.Contains(searchName));
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

            var swimmer = await _context.Swimmers
                .Include(s => s.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (swimmer == null)
            {
                return NotFound();
            }

            return View(swimmer);
        }
        
        public IActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["SwimmerSexOptions"] = Enum.GetValues(typeof(SwimmerSex))
                                             .Cast<SwimmerSex>()
                                             .Select(e => new SelectListItem
                                             {
                                                 Value = e.ToString(),
                                                 Text = e.ToString()
                                             }).ToList();
            return View();
        }
   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PhoneNumber,Gender,BirthDate,SwimmerSex")] Swimmer swimmer)
        {
            if (ModelState.IsValid)
            {
              
                string newUserId = Guid.NewGuid().ToString();
             
                var newUser = new ApplicationUser
                {
                    Id = newUserId,
                    UserName = newUserId,
                    NormalizedUserName = newUserId.ToUpper(),
                    Email = $"{newUserId}@example.com",
                    NormalizedEmail = $"{newUserId}@example.com".ToUpper(),
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "DefaultPassword123!")
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();               
                swimmer.IdentityUserId = newUserId;               
                _context.Add(swimmer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(swimmer);
        }
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var swimmer = await _context.Swimmers
                                        .Include(s => s.IdentityUser)
                                        .FirstOrDefaultAsync(s => s.Id == id);

            if (swimmer == null)
            {
                return NotFound();
            }
            ViewData["SwimmerSexOptions"] = Enum.GetValues(typeof(SwimmerSex))
                                           .Cast<SwimmerSex>()
                                           .Select(e => new SelectListItem
                                           {
                                               Value = e.ToString(),
                                               Text = e.ToString()
                                           }).ToList();

            return View(swimmer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PhoneNumber,Gender,BirthDate,IdentityUserId,SwimmerSex")] Swimmer swimmer)
        {
            if (id != swimmer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                 
                    var existingUser = await _context.Users.FindAsync(swimmer.IdentityUserId);

                    if (existingUser == null)
                    {                       
                        var newUser = new ApplicationUser
                        {
                            Id = swimmer.IdentityUserId,
                            UserName = swimmer.IdentityUserId,
                            NormalizedUserName = swimmer.IdentityUserId.ToUpper(),
                            Email = $"{swimmer.IdentityUserId}@example.com",
                            NormalizedEmail = $"{swimmer.IdentityUserId}@example.com".ToUpper(),
                            EmailConfirmed = true,
                            PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "DefaultPassword123!")
                        };

                        _context.Users.Add(newUser);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {                        
                        existingUser.UserName = swimmer.IdentityUserId;
                        existingUser.NormalizedUserName = swimmer.IdentityUserId.ToUpper();
                        _context.Users.Update(existingUser);
                        await _context.SaveChangesAsync();
                    }
                
                    _context.Update(swimmer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SwimmerExists(swimmer.Id))
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

            return View(swimmer);
        }
       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var swimmer = await _context.Swimmers
                .Include(s => s.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (swimmer == null)
            {
                return NotFound();
            }

            return View(swimmer);
        }
       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var swimmer = await _context.Swimmers.FindAsync(id);
            if (swimmer != null)
            {
                _context.Swimmers.Remove(swimmer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SwimmerExists(int id)
        {
            return _context.Swimmers.Any(e => e.Id == id);
        }
    }
}
