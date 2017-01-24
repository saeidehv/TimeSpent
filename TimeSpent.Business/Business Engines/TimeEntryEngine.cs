using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using TimeSpent.Business.Common;


namespace TimeSpent.Business
{
    [Export(typeof(ITimeEntryEngine))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TimeEntryEngine : ITimeEntryEngine
    {
        public bool IsTimeAvaiable(DateTime date, DateTime from, DateTime to /*,User user*/)
        {
            bool isAvaiable = false;


            return isAvaiable;
        }
    }
}
