using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TennisForEveryone.Models.ViewModel
{
    public class ScheduleViewModel
    {
        public string Schedule { get; set; }
        public string CoachName { get; set; }
        public int CoachId { get; set; }
        public string Email { get; set; }
        public DateTime ScheduleDate { get; set; }
    }
}
