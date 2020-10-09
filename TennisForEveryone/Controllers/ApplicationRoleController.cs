using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TennisForEveryone.Models;
using TennisForEveryone.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;

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
            return View(model);
        }

        [HttpGet]
        // Get specific role to edit 
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
            //get all the associate users to the role -Sandipa
            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, r.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }

        //Update role in database once user change/edit Role name -Sandipa
        [HttpPost]
        public async Task<IActionResult> EditRole(RoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                return NotFound();
            }
            else
            {
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }


        [HttpGet]
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
                    UserName = user.UserName,
          
                };
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = true;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);

        }

       
        [HttpPost]
        /*Added Http post request method called ‘EditUsersInRole’ 
        to be able to assign roles to users. Such as Admin, Coach, Member.(Sandipa)*/
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return NotFound();
            }
            //Dont know how to fix this error to add users to role
            //ArgumentNullException: Value cannot be null. (Parameter 'user')
            // Microsoft.AspNetCore.Identity.UserManager<TUser>.IsInRoleAsync(TUser user, string role) -- Fixed the error was an error in not populating the model through the view - Tommy
            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }


            return RedirectToAction("EditRole", new { id = roleId });
        }
    }
}
