using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TennisForEveryone.Models;
using TennisForEveryone.Models.ViewModel;

namespace TennisForEveryone.Controllers
{
    public class ApplicationRoleController: Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public UserManager<ApplicationUser> UserManager { get; }

        public ApplicationRoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            List<RoleViewModel> model = new List<RoleViewModel>();

            model = roleManager.Roles.Select(r => new RoleViewModel
            {
                RoleName = r.Name,
                Id = r.Id
            }).ToList();
            var roles = roleManager.Roles;
            return View(model);
        }

        public async Task<IActionResult> EditRole(string id)
        {
            var r = await roleManager.FindByIdAsync(id);

            if (r == null)
            {
                return NotFound();
            }

            var model = new RoleViewModel
            {
                Id = r.Id,
                RoleName = r.Name
            };

            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, r.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = "Not found";
                return NotFound();
            }
            var model = new List<UserRoleViewModel>();
            foreach (var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    Username = user.UserName
                };
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.Isselected = true;
                }
                else
                {
                    userRoleViewModel.Isselected = true;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);

        }
    }
}
