using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Security.Permissions;
using TimeSpent.Business.Contracts;
using TimeSpent.Business.Entities;
using TimeSpent.Core;
using TimeSpent.Core.Contracts;
using TimeSpent.Data.Contracts.RepositoryInterfaces;
using TimeSpent.Core.Exceptions;
using TimeSpent.Business.Common;



namespace TimeSpent.Business.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple,
        ReleaseServiceInstanceOnTransactionComplete = false,
        IncludeExceptionDetailInFaults =true)]
    public class TimeEntryManager : ManagerBase, ITimeEntryService
    {
        public TimeEntryManager()
        {

        }

        public TimeEntryManager(IDataRepositoryFactory repFactory)
        {
            _repositoryFactory = repFactory;
        }

        public TimeEntryManager(IBusinessEngineFactory busFactory)
        {
            _businessEnginFactory = busFactory;
        }

        public TimeEntryManager(IDataRepositoryFactory repFactory, IBusinessEngineFactory busFactory)
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

     //   [PrincipalPermission(SecurityAction.Demand, Role = Security.TimeSpentAdminRole)]
    //    [PrincipalPermission(SecurityAction.Demand, Name = Security.TimeSpentUser)]
        public TimeEntry[] GetAllTimeEntries()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                ITimeEntryRepository repository = _repositoryFactory.GetRepository<ITimeEntryRepository>();
                IEnumerable<TimeEntry> entries = repository.Get();
                return entries.ToArray();


            });
          
        }

     //   [PrincipalPermission(SecurityAction.Demand, Role = Security.TimeSpentAdminRole)]
      //  [PrincipalPermission(SecurityAction.Demand, Name = Security.TimeSpentUser)]
        public TimeEntry GetTimeEntry(int id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                ITimeEntryRepository repository = _repositoryFactory.GetRepository<ITimeEntryRepository>();
                TimeEntry entry = repository.GetById(id);

                if (entry == null)
                {
                    NotFoundException exp = new NotFoundException(string.Format("Time entry with id {0} not found", id));
                    throw new FaultException<NotFoundException>(exp, exp.Message);

                }
                return entry;
            });

        }

        [OperationBehavior(TransactionScopeRequired = true)]
  //      [PrincipalPermission(SecurityAction.Demand, Role = Security.TimeSpentAdminRole)]
  //      [PrincipalPermission(SecurityAction.Demand, Name = Security.TimeSpentUser)]
        public TimeEntry UpdateTimeEntry(string loginName, TimeEntry entry)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IAccountRepository accountRepository = _repositoryFactory.GetRepository<IAccountRepository>();
                Account account = accountRepository.GetByLogin(loginName);

                if (account == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("No account found for login '{0}'.", loginName));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                ValidateAuthorization(account);

                ITimeEntryRepository repository = _repositoryFactory.GetRepository<ITimeEntryRepository>();
                TimeEntry updatedEntry = null;

                entry.AccountId = account.AccountId;

                if (entry.TimeEntryId == 0)
                {
                    updatedEntry = repository.Add(entry);
                }
                else
                {
                    updatedEntry = repository.Update(entry);
                }
                return updatedEntry;

            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]  // for any update and delete operations
   //     [PrincipalPermission(SecurityAction.Demand, Role = Security.TimeSpentAdminRole)]
   //     [PrincipalPermission(SecurityAction.Demand, Name = Security.TimeSpentUser)]
        public void DeleteTimeEntry(int id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                ITimeEntryRepository repository = _repositoryFactory.GetRepository<ITimeEntryRepository>();

                TimeEntry entry = repository.GetById(id);
                repository.Delete(entry);
            });
        }


    //    [PrincipalPermission(SecurityAction.Demand, Role = Security.TimeSpentAdminRole)]
    //    [PrincipalPermission(SecurityAction.Demand, Name = Security.TimeSpentUser)]
        public bool IsTimeAvaiable(DateTime date, DateTime from, DateTime to)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                bool isAvailable = false;
                                                                      
                ITimeEntryEngine businessEngin = _businessEnginFactory.GetBusinessEngine<ITimeEntryEngine>();
                isAvailable = businessEngin.IsTimeAvaiable(date, from, to);
                return isAvailable;
            });
        }




        //    [PrincipalPermission(SecurityAction.Demand, Role = Security.TimeSpentAdminRole)]
        //    [PrincipalPermission(SecurityAction.Demand, Name = Security.TimeSpentUser)]

        public TimeEntryData[] GetTimeEntriesByAccount(string loginName)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                ITimeEntryRepository entryRepository = _repositoryFactory.GetRepository<ITimeEntryRepository>();
                IAccountRepository accountRepository = _repositoryFactory.GetRepository<IAccountRepository>();
                Account user = accountRepository.GetByLogin(loginName);

                if (user == null)
                {
                    NotFoundException exp = new NotFoundException(string.Format("No account found for user {0}", loginName));
                    throw new FaultException<NotFoundException>(exp, exp.Message);
                }

                ValidateAuthorization(user);

                
                var timeEntriesData = entryRepository.GetTimeHistoryByAccount(user);
                return timeEntriesData.ToArray();

            });
        }

        //    [PrincipalPermission(SecurityAction.Demand, Role = Security.TimeSpentAdminRole)]
        //    [PrincipalPermission(SecurityAction.Demand, Name = Security.TimeSpentUser)]
        public ProjectCategoryReport[] GetProjectReport(string loginName)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                ITimeEntryRepository entryRepository = _repositoryFactory.GetRepository<ITimeEntryRepository>();
                IAccountRepository accountRepository = _repositoryFactory.GetRepository<IAccountRepository>();
                Account user = accountRepository.GetByLogin(loginName);

                if (user == null)
                {
                    NotFoundException exp = new NotFoundException($"No account found for user {loginName}");
                    throw new FaultException<NotFoundException>(exp, exp.Message);
                }

                ValidateAuthorization(user);

                IEnumerable<ProjectCategoryReport> timeEntriesData = entryRepository.GetProjectReport(user.AccountId);
                return timeEntriesData.ToArray();

            });
        }

        //    [PrincipalPermission(SecurityAction.Demand, Role = Security.TimeSpentAdminRole)]
        //    [PrincipalPermission(SecurityAction.Demand, Name = Security.TimeSpentUser)]
        public ProjectCategoryReport[] GetProjectCategoryReport(string loginName)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                ITimeEntryRepository entryRepository = _repositoryFactory.GetRepository<ITimeEntryRepository>();
                IAccountRepository accountRepository = _repositoryFactory.GetRepository<IAccountRepository>();
                Account user = accountRepository.GetByLogin(loginName);

                if (user == null)
                {
                    NotFoundException exp = new NotFoundException($"No account found for user {loginName}");
                    throw new FaultException<NotFoundException>(exp, exp.Message);
                }

                ValidateAuthorization(user);

                IEnumerable<ProjectCategoryReport> timeEntriesData = entryRepository.GetProjectCategoryReport(user.AccountId);
                return timeEntriesData.ToArray();

            });
        }
    }
}
