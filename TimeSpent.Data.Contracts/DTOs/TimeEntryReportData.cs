using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSpent.Data.Contracts
{
    public class TimeEntryReportData
    {
        public string ProjectName { get; set; }

        public string CategoryName { get; set; }

        public float Duration { get; set; }

    }
}
