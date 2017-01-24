using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using TimeSpent.Client.Entities;
using TimeSpent.Core.Exceptions;
using TimeSpent.Core.Contracts;
using TimeSpent.Common;
namespace TimeSpent.Client.Contracts
{
    [ServiceContract]
    public interface ITimeEntryService: IServiceContract
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
     //   [FaultContract(typeof(AuthorizationValidationException))]
        TimeEntry GetTimeEntry(int id);

        [OperationContract]
     //   [FaultContract(typeof(AuthorizationValidationException))]
        TimeEntry[] GetAllTimeEntries();

        [OperationContract]
        TimeEntryData[] GetTimeEntriesByAccount(string loginName);

        [OperationContract]
        [TransactionFlow( TransactionFlowOption.Allowed)]
     //   [FaultContract(typeof(AuthorizationValidationException))]
        TimeEntry UpdateTimeEntry(string loginName, TimeEntry entry);


        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
    //    [FaultContract(typeof(AuthorizationValidationException))]
        void DeleteTimeEntry(int id);

        [OperationContract]
     //   [FaultContract(typeof(AuthorizationValidationException))]
        bool IsTimeAvaiable(DateTime date, DateTime start, DateTime stop);
                

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ProjectCategoryReport[] GetProjectCategoryReport(string loginName);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ProjectCategoryReport[] GetProjectReport(string loginName);

        [OperationContract]
        Task<ProjectCategoryReport[]> GetProjectCategoryReportAsync(string loginName);

        [OperationContract]
        Task<ProjectCategoryReport[]> GetProjectReportAsync(string loginName);

        //   [OperationContract]
        ////   [FaultContract(typeof(AuthorizationValidationException))]
        //   TimeEntryData[] GetSpentTimeHistory(string loginName);


        //[OperationContract]
        //IEnumerable<UserEntryData> GetSpentTimeFullHistory(string loginName);

        //[OperationContract]
        //Task<TimeEntry> UpdateTimeEntryAsync(TimeEntry entry);

        //[OperationContract]
        //Task DeleteTimeEntryAsync(int id);

    }
}
