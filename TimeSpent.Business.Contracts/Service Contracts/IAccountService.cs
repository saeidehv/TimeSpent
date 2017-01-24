using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using TimeSpent.Business.Entities;
using TimeSpent.Core.Exceptions;
using TimeSpent.Common;

namespace TimeSpent.Business.Contracts
{
    [ServiceContract]
    public interface IAccountService
    {
        [OperationContract]
 //       [FaultContract(typeof(AuthorizationValidationException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Account UpdateAccountInfo(Account account);

       
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
   //     [FaultContract(typeof(AuthorizationValidationException))]
        Account GetAccountInfo(string loginName);

        //[OperationContract]
        //Account[] Get();

        //[OperationContract]
        //[TransactionFlow(TransactionFlowOption.Allowed)]
        //void DeleteAccount(int id);


    }
}
