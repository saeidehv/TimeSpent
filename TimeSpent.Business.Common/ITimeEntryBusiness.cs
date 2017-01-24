using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSpent.Core.Contracts;


namespace TimeSpent.Business.Common
{
    public interface ITimeEntryEngine:IBusinessEngine
    {
        bool IsTimeAvaiable(DateTime date, DateTime from, DateTime to /*,User user*/);
    }
}
