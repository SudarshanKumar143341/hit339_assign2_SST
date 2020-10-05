using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TennisForEveryone.Models
{
    public class ApplicationUser:IdentityUser
    {

        public string Fname { get; set; }

        public string Lname { get; set; }


        public int Age { get; set; }


        public int StreetNumber { get; set; }


        public string StreetName { get; set; }


        public string Suburb { get; set; }

        public string Country { get; set; }


        public int Pincode { get; set; }
    }
}
