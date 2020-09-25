using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace TennisForEveryone.Models
{
    public partial class Coach
    {
        [Column("ID")]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Biography { get; set; }
        [Column("PhotoURL")]
        public string PhotoUrl { get; set; }
    }
}
