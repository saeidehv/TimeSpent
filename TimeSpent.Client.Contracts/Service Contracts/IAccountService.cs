using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using TimeSpent.Core.Contracts;
using TimeSpent.Client.Entities;
using TimeSpent.Common;
using TimeSpent.Core.Exceptions;

namespace TimeSpent.Client.Contracts
{
    [ServiceContract]
    public interface IAccountService:IServiceContract
    {
        [OperationContract]
   //     [FaultContract(typeof(AuthorizationValidationException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Account UpdateAccountInfo(Account account);


        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
    //    [FaultContract(typeof(AuthorizationValidationException))]
        Account GetAccountInfo(string loginName);

    }
}
