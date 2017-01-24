using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSpent.Core.Contracts;
using TimeSpent.Business.Entities;
using TimeSpent.Business.Contracts;

namespace TimeSpent.Data.Contracts.RepositoryInterfaces
{
    // custom operations just for TimeEntry 
    public interface ITimeEntryRepository : IRepository<TimeEntry>
    {
        IEnumerable<TimeEntryData> GetTimeHistoryByAccount(Account account);
        IEnumerable<ProjectCategoryReport> GetProjectCategoryReport(int accountId);
        IEnumerable<ProjectCategoryReport> GetProjectReport(int accountId);

        
    }
}
