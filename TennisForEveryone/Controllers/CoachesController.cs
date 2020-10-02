using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TennisForEveryone.Data;
using TennisForEveryone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace TennisForEveryone.Controllers
{
  
    public class CoachesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CoachesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        
        // GET: Coaches
        public async Task<IActionResult> Index()
        {
            ApplicationUser usr = await _userManager.GetUserAsync(HttpContext.User);
            var coachList = _context.Coach;

            if (usr != null)
            {
                var userIsCoach = false;
                foreach (var coach in coachList)
                {
                    if (usr.Email == coach.Email)
                    {
                        userIsCoach = true;
                    }
                }

                if (userIsCoach)
                {
                    var x = await _context.Coach.FirstOrDefaultAsync(c => c.Email == usr.Email);
                    List<Coach> caoch = new List<Coach>();
                    caoch.Add(x);
                    return View(caoch);
                }
            }
            return View(await _context.Coach.ToListAsync());
        }

       
        // GET: Coaches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coach = await _context.Coach
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coach == null)
            {
                return NotFound();
            }

            return View(coach);
        }

        [Authorize(Roles = "Coach, Admin")]
        // GET: Coaches/Create
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Coach, Admin")]
        // POST: Coaches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Biography,PhotoUrl")] Coach coach)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coach);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coach);
        }

        [Authorize(Roles = "Admin, Coach")]
        // GET: Coaches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coach = await _context.Coach.FindAsync(id);
            if (coach == null)
            {
                return NotFound();
            }
            return View(coach);
        }

        [Authorize(Roles = "Admin, Coach")]
        // POST: Coaches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Biography,PhotoUrl")] Coach coach)
        {
            if (id != coach.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
            return View(coach);
        }

        [Authorize(Roles = "Admin, Coach")]
        // GET: Coaches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coach = await _context.Coach
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coach == null)
            {
                return NotFound();
            }

            return View(coach);
        }

        [Authorize(Roles = "Admin, Coach")]
        // POST: Coaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coach = await _context.Coach.FindAsync(id);
            _context.Coach.Remove(coach);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoachExists(int id)
        {
            return _context.Coach.Any(e => e.Id == id);
        }
    }
}
