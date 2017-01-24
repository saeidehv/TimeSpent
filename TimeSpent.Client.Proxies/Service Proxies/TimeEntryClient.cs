using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.ComponentModel.Composition;
using TimeSpent.Client.Contracts;
using TimeSpent.Client.Entities;
using TimeSpent.Core.ServiceModel;


namespace TimeSpent.Client.Proxies
{
    [Export(typeof(ITimeEntryService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TimeEntryClient : UserClientBase<ITimeEntryService>, ITimeEntryService
    {
        public void DeleteTimeEntry(int id)
        {
            Channel.DeleteTimeEntry(id);

        }

        public TimeEntry[] GetAllTimeEntries()
        {
            return Channel.GetAllTimeEntries();
           
        }

        public ProjectCategoryReport[] GetProjectCategoryReport(string loginName)
        {
            return Channel.GetProjectCategoryReport(loginName);
        }

        public Task<ProjectCategoryReport[]> GetProjectCategoryReportAsync(string loginName)
        {
            return Channel.GetProjectCategoryReportAsync(loginName);
        }

        public ProjectCategoryReport[] GetProjectReport(string loginName)
        {
            return Channel.GetProjectReport(loginName);
        }

        public Task<ProjectCategoryReport[]> GetProjectReportAsync(string loginName)
        {
            return Channel.GetProjectReportAsync(loginName);
        }

       
        public TimeEntryData[] GetTimeEntriesByAccount(string loginName)
        {
            return Channel.GetTimeEntriesByAccount(loginName);
        }

        public TimeEntry GetTimeEntry(int id)
        {
            return Channel.GetTimeEntry(id);
        }

        public bool IsTimeAvaiable(DateTime date, DateTime start, DateTime stop)
        {
            return Channel.IsTimeAvaiable(date, start, stop);
        }

        public TimeEntry UpdateTimeEntry(string loginName, TimeEntry entry)
        {
            return Channel.UpdateTimeEntry(loginName, entry);
        }

       
    }
}
