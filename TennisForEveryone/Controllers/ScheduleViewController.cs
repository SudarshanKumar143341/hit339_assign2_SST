using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TennisForEveryone.Models;
using TennisForEveryone.Data;
using TennisForEveryone.Models.ViewModel;

namespace TennisForEveryone.Controllers
{
    public class ScheduleViewController: Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private int xx;
        public ScheduleViewController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Index()
        {
            List<ScheduleViewModel> model = new List<ScheduleViewModel>();
            var data = _context.Schedule;
            var schData = _context.ScheduleMembers;
            var coachList = _context.Coach;

            ApplicationUser usr = await GetCurrentUserAsync();
            foreach (var s in data)
            {
                foreach (var m in schData)
                {
                    if (usr.Email == m.MemberEmail && s.Id == m.ScheduleId)
                    {
                        foreach (var coach in coachList)
                        {
                            if (s.CoachEmail == coach.Email)
                            {
                                xx = coach.Id;
                            }
                        }

                        var viewModel = new ScheduleViewModel
                        {
                            ScheduleDate = s.When,
                            Schedule = s.Description,
                            CoachName = s.CoachEmail,
                            CoachId = xx
                        };

                        model.Add(viewModel);
                    }
                }
            }
            return View(model);
        }




        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
