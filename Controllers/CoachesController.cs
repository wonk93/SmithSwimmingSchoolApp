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
    public class CoachesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoachesController(ApplicationDbContext context)
        {
            _context = context;
        }
                
        public IActionResult Index(int? page, string searchName)
        {
            var applicationDbContext = _context.Coaches.Include(c => c.IdentityUser).AsQueryable();
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

            var coach = await _context.Coaches
                .Include(c => c.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coach == null)
            {
                return NotFound();
            }

            return View(coach);
        }
     
        public IActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["CoachSexOptions"] = Enum.GetValues(typeof(CoachSex))
                                      .Cast<CoachSex>()
                                      .Select(e => new SelectListItem
                                      {
                                          Value = e.ToString(),
                                          Text = e.ToString()
                                      }).ToList();
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PhoneNumber,CoachSex")] Coach coach)
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

            coach.IdentityUserId = newUserId;
         
            if (ModelState.IsValid)
            {
                _context.Add(coach);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(coach);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coach = await _context.Coaches
                                      .Include(c => c.IdentityUser) 
                                      .FirstOrDefaultAsync(c => c.Id == id);
            if (coach == null)
            {
                return NotFound();
            }

            ViewData["CoachSexOptions"] = Enum.GetValues(typeof(CoachSex))
                                              .Cast<CoachSex>()
                                              .Select(e => new SelectListItem
                                              {
                                                  Value = e.ToString(),
                                                  Text = e.ToString()
                                              }).ToList();

            return View(coach);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PhoneNumber,IdentityUserId,CoachSex")] Coach coach)
        {
            if (id != coach.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   
                    var existingUser = await _context.Users.FindAsync(coach.IdentityUserId);

                    if (existingUser == null)
                    {
                       
                        var newUser = new ApplicationUser
                        {
                            Id = coach.IdentityUserId, 
                            UserName = coach.IdentityUserId,
                            NormalizedUserName = coach.IdentityUserId.ToUpper(),
                            Email = $"{coach.IdentityUserId}@example.com",
                            NormalizedEmail = $"{coach.IdentityUserId}@example.com".ToUpper(),
                            EmailConfirmed = true,
                            PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "DefaultPassword123!")
                        };

                        _context.Users.Add(newUser);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {                       
                        existingUser.UserName = coach.IdentityUserId;
                        existingUser.NormalizedUserName = coach.IdentityUserId.ToUpper();
                        _context.Users.Update(existingUser);
                        await _context.SaveChangesAsync();
                    }
                    
                    _context.Update(coach);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoachExists(coach.Id))
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

            ViewData["CoachSexOptions"] = Enum.GetValues(typeof(CoachSex))
                                              .Cast<CoachSex>()
                                              .Select(e => new SelectListItem
                                              {
                                                  Value = e.ToString(),
                                                  Text = e.ToString()
                                              }).ToList();

            return View(coach);
        }
     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coach = await _context.Coaches.FindAsync(id);
            if (coach != null)
            {
                _context.Coaches.Remove(coach);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoachExists(int id)
        {
            return _context.Coaches.Any(e => e.Id == id);
        }
    }
}
