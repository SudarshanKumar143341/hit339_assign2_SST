using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TennisForEveryone.Data;
using TennisForEveryone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace TennisForEveryone.Controllers
{
    public class ScheduleMembersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ScheduleMembersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ScheduleMembers
        public async Task<IActionResult> Index()
        {
            ApplicationUser usr = await _userManager.GetUserAsync(HttpContext.User);
            var data = _context.Schedule;
            var coachList = _context.Coach;
            var scheduleMembers = await _context.ScheduleMembers.ToListAsync();
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
                    List<Schedule> coachSchedules = data.ToList().FindAll(s => s.CoachEmail.Equals(usr.Email));
                    List<ScheduleMembers> filteredMembers = new List<ScheduleMembers>();
                    foreach (var coachSchedule in coachSchedules)
                    {
                        List<ScheduleMembers> filteredMembersTemp = scheduleMembers.FindAll(sm => sm.ScheduleId == coachSchedule.Id);
                        filteredMembers.AddRange(filteredMembersTemp);
                    }
                    return View(filteredMembers);
                }
            }
            return View(scheduleMembers);
        }

        // GET: ScheduleMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduleMembers = await _context.ScheduleMembers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scheduleMembers == null)
            {
                return NotFound();
            }

            return View(scheduleMembers);
        }

        // GET: ScheduleMembers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ScheduleMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ScheduleId,MemberEmail")] ScheduleMembers scheduleMembers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scheduleMembers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(scheduleMembers);
        }

        // GET: ScheduleMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduleMembers = await _context.ScheduleMembers.FindAsync(id);
            if (scheduleMembers == null)
            {
                return NotFound();
            }
            return View(scheduleMembers);
        }

        // POST: ScheduleMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ScheduleId,MemberEmail")] ScheduleMembers scheduleMembers)
        {
            if (id != scheduleMembers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scheduleMembers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleMembersExists(scheduleMembers.Id))
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
            return View(scheduleMembers);
        }

        // GET: ScheduleMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduleMembers = await _context.ScheduleMembers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scheduleMembers == null)
            {
                return NotFound();
            }

            return View(scheduleMembers);
        }

        // POST: ScheduleMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scheduleMembers = await _context.ScheduleMembers.FindAsync(id);
            _context.ScheduleMembers.Remove(scheduleMembers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
               [Authorize(Roles = "Member")]
                // GET: ScheduleMembers/Enroll/5
                public async Task<IActionResult> Enroll(int? id)
                {
                    ApplicationUser usr = await GetCurrentUserAsync();
                    if (id == null)
                    {
                        return NotFound();
                    }
                    var schedule = await _context.Schedule.FindAsync(id);
                    if (schedule == null)
                    {
                        return NotFound();
                    }
                    ViewBag.userEmail = usr.Email;
                    return View(schedule);
                }


                    //POST: ScheduleMembers/Enroll/6
                    [HttpPost, ActionName("Enroll")]
                    [ValidateAntiForgeryToken]
                    public async Task<IActionResult> EnrollConfirmed([Bind("ScheduleId,MemberEmail")] ScheduleMembers scheduleMembers)
                    {
                        if (ModelState.IsValid)
                        {
                            _context.Add(scheduleMembers);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                       return View(scheduleMembers);
                    }
          

        private bool ScheduleMembersExists(int id)
        {
            return _context.ScheduleMembers.Any(e => e.Id == id);
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
