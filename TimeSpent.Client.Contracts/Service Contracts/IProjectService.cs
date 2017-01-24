using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TimeSpent.Client.Entities;
using TimeSpent.Core.Contracts;
using TimeSpent.Core.Exceptions;

namespace TimeSpent.Client.Contracts
{

    [ServiceContract]
    public interface IProjectService : IServiceContract
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Project GetProject(int id);

        [OperationContract]
        Project[] GetAllProjects();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        //   [FaultContract(typeof(AuthorizationValidationException))]
        Project UpdateProject(Project project);


        [OperationContract]
      //  [TransactionFlow(TransactionFlowOption.Allowed)]
        [FaultContract(typeof(CascadeDeleteException))]
        //  [FaultContract(typeof(AuthorizationValidationException))]
        void DeleteProject(int id);



        //[OperationContract]
        //Task DeleteProjectAsync(int id);
    }
}

