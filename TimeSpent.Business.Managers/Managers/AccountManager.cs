using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Security.Permissions;
using TimeSpent.Business.Contracts;
using TimeSpent.Business.Entities;
using TimeSpent.Core.Contracts;
using TimeSpent.Common;
using TimeSpent.Data.Contracts.RepositoryInterfaces;
using TimeSpent.Core.Exceptions;

namespace TimeSpent.Business.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple,
        ReleaseServiceInstanceOnTransactionComplete = false)]
    public class AccountManager : ManagerBase, IAccountService
    {
        public AccountManager()
        {

        }

        public AccountManager(IDataRepositoryFactory repFactory)
        {
            _repositoryFactory = repFactory;
        }

        public AccountManager(IBusinessEngineFactory busFactory)
        {
            _businessEnginFactory = busFactory;
        }

        public AccountManager(IDataRepositoryFactory repFactory, IBusinessEngineFactory busFactory)
        {
            _repositoryFactory = repFactory;
            _businessEnginFactory = busFactory;
        }

        [Import]
        IDataRepositoryFactory _repositoryFactory;

        [Import]
        IBusinessEngineFactory _businessEnginFactory;


        protected override Account LoadAuthorizationValidationAccount(string loginName)
        {
            IAccountRepository accountRepository = _repositoryFactory.GetRepository<IAccountRepository>();
            Account user = accountRepository.GetByLogin(loginName);

            if (user == null)
            {
                NotFoundException exp = new NotFoundException(string.Format("Cannot find account for login name {0} to use for security", loginName));
                throw new FaultException<NotFoundException>(exp, exp.Message);
            }
            return user;
        }


        [OperationBehavior(TransactionScopeRequired =true)]
   //     [PrincipalPermission(SecurityAction.Demand , Role = Security.TimeSpentAdminRole)]
        public void DeleteAccount(int id)
        {
            ExecuteFaultHandledOperation(() =>
           {
               IAccountRepository repository = _repositoryFactory.GetRepository<IAccountRepository>();

               Account account = repository.GetById(id);
               repository.Delete(account);
           });
            
        }
       
   //     [PrincipalPermission(SecurityAction.Demand, Role  = Security.TimeSpentAdminRole)]
   //     [PrincipalPermission(SecurityAction.Demand , Name = Security.TimeSpentUser)]
        public Account[] Get()
        {
            return ExecuteFaultHandledOperation(() =>
           {
               IAccountRepository repository = _repositoryFactory.GetRepository<IAccountRepository>();
               return repository.Get().ToArray();
           });
        }

   //     [PrincipalPermission(SecurityAction.Demand, Role = Security.TimeSpentAdminRole)]
   //     [PrincipalPermission(SecurityAction.Demand, Name = Security.TimeSpentUser)]
        public Account Get(int id)
        {
            return ExecuteFaultHandledOperation(() =>
                {
                    IAccountRepository repository = _repositoryFactory.GetRepository<IAccountRepository>();
                    Account account = repository.GetById(id);

                    if( account == null)
                    {
                        NotFoundException exp = new NotFoundException(string.Format("Account with id {0} not found", id));
                        throw new FaultException<NotFoundException>(exp, exp.Message);
                    }

                    return account;
                });
        }

        
        [OperationBehavior(TransactionScopeRequired = true)]
     //   [PrincipalPermission(SecurityAction.Demand , Role = Security.TimeSpentAdminRole)]
     //   [PrincipalPermission(SecurityAction.Demand, Name  = Security.TimeSpentUser)]
        public Account UpdateAccountInfo(Account account)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                Account updatedAccount = null;
                IAccountRepository repository = _repositoryFactory.GetRepository<IAccountRepository>();

                if (account.AccountId == 0)
                {
                    updatedAccount = repository.Add(account);
                }
                else
                {
                    updatedAccount= repository.Update(account);
                }

                return updatedAccount;

            });
        }

    //    [PrincipalPermission(SecurityAction.Demand, Role = Security.TimeSpentAdminRole)]
    //    [PrincipalPermission(SecurityAction.Demand, Name = Security.TimeSpentUser)]
        public Account GetAccountInfo(string loginEmail)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IAccountRepository accountRepository = _repositoryFactory.GetRepository<IAccountRepository>();

                Account accountEntity = accountRepository.GetByLogin(loginEmail);
                if (accountEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Account with login {0} is not in database", loginEmail));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                ValidateAuthorization(accountEntity);

                return accountEntity;
            });
        }


    }
}
