using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TimeSpent.Business.Entities;
using TimeSpent.Core.Exceptions;

namespace TimeSpent.Business.Contracts
{
    [ServiceContract]
    public interface IProjectService
    {
        [OperationContract]
        Project UpdateProject(Project project);

        [OperationContract]
        [FaultContract(typeof(CascadeDeleteException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteProject(int id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Project GetProject(int id);

        [OperationContract]
        Project[] GetAllProjects();
    }
}
