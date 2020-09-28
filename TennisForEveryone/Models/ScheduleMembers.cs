﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace TennisForEveryone.Models
{
    public partial class ScheduleMembers
    {
        [Column("ID")]
        public int Id { get; set; }
       
        [Column("ScheduleID")]
        public int ScheduleId { get; set; }
        public string MemberEmail { get; set; }
    }
}
