﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Model.Entities
{
    public class About
    {
        public int AboutId { get; set; }
        public string? StartMessage { get; set; }
        public string? ProfileMessage { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Title { get; set; }
        public string? Email { get; set; }
        public string? CvLink { get; set; }
        public ICollection<Skill> Skills { get; set; }
    }
}
