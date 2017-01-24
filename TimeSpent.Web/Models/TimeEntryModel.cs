using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSpent.Web.Models
{
    public class TimeEntryModel
    {
        public int TimeEntryId { get; set; }
        public float Duration { get; set; }
        public DateTime Date { get; set; }
        public int User { get; set; }
        public int Project { get; set; }
        public int Category { get; set; }
        public string Description { get; set; }
    }
}