﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace TennisForEveryone.Models
{
    public partial class Schedule
    {
        [Column("ID")]
        public int Id { get; set; }
        public DateTime When { get; set; }
        public string Description { get; set; }
        public string CoachEmail { get; set; }
        public string Location { get; set; }
    }
}
