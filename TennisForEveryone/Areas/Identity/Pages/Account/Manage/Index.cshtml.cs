using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisForEveryone.Models;

namespace TennisForEveryone.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "First Name")]

            public string Fname { get; set; }
            [Display(Name = "Last Name")]
            public string Lname { get; set; }

            [Display(Name = "Age")]
            public int Age { get; set; }

            [Display(Name = "Street No.")]
            public int StreetNumber { get; set; }

            [Display(Name = "Street Name")]
            public string StreetName { get; set; }

            [Display(Name = "Suburb")]
            public string Suburb { get; set; }

            [Display(Name = "Country")]
            public string Country { get; set; }

            [Display(Name = "Pincode")]
            public int Pincode { get; set; }

            [Display(Name = "Username")]
            public string Username { get; set; }
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var firstName = user.Fname;
            var lastName = user.Lname;
            var age = user.Age;
            var streetno = user.StreetNumber;
            var streetname = user.StreetName;
            var suburb = user.Suburb;
            var country = user.Country;
            var pincode = user.Pincode;
            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Username = userName,
                Fname = firstName,
                Lname = lastName,
                Age = age,
                StreetNumber = streetno,
                StreetName = streetname,
                Suburb = suburb,
                Country = country,
                Pincode = pincode
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
            string firstName = user.Fname;
            if (Input.Fname != firstName)
            {
                user.Fname = Input.Fname;
                await _userManager.UpdateAsync(user);
            }
            string lastName = user.Lname;
            if (Input.Lname != lastName)
            {
                user.Lname = Input.Lname;
                await _userManager.UpdateAsync(user);
            }
            int age = user.Age;
            if (Input.Age != age)
            {
                user.Age = Input.Age;
                await _userManager.UpdateAsync(user);
            }

            int streetno = user.StreetNumber;
            if (Input.StreetNumber != streetno)
            {
                user.StreetNumber = Input.StreetNumber;
                await _userManager.UpdateAsync(user);
            }

            string streetname = user.StreetName;
            if (Input.StreetName != streetname)
            {
                user.StreetName = Input.StreetName;
                await _userManager.UpdateAsync(user);
            }

            string suburb = user.Suburb;
            if (Input.Suburb != suburb)
            {
                user.Suburb = Input.Suburb;
                await _userManager.UpdateAsync(user);
            }

            string country = user.Country;
            if (Input.Country != country)
            {
                user.Country = Input.Country;
                await _userManager.UpdateAsync(user);
            }

            int pincode = user.Pincode;
            if (Input.Pincode != pincode)
            {
                user.Pincode = Input.Pincode;
                await _userManager.UpdateAsync(user);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
