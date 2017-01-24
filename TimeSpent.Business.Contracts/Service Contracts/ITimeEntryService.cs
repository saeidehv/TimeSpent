using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using TimeSpent.Business.Entities;
using TimeSpent.Common;
using TimeSpent.Core.Exceptions;
using TimeSpent.Business.Contracts;

namespace TimeSpent.Business.Contracts
{
    [ServiceContract]
    public interface ITimeEntryService
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        TimeEntry GetTimeEntry(int id);

        [OperationContract]
        TimeEntry[] GetAllTimeEntries();

        [OperationContract]
        TimeEntryData[] GetTimeEntriesByAccount(string loginName);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        TimeEntry UpdateTimeEntry(string loginName, TimeEntry entry);


        [OperationContract]
        [FaultContract(typeof(CascadeDeleteException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteTimeEntry(int id);

        [OperationContract]
        bool IsTimeAvaiable(DateTime date, DateTime start, DateTime stop);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ProjectCategoryReport[] GetProjectCategoryReport(string loginName);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ProjectCategoryReport[] GetProjectReport(string loginName);

       

    }
}
