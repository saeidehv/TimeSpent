using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TimeSpent.Business.Contracts;
using TimeSpent.Business.Entities;
using TimeSpent.Common;
using TimeSpent.Core.Contracts;
using TimeSpent.Core.Exceptions;
using TimeSpent.Data.Contracts.RepositoryInterfaces;

namespace TimeSpent.Business.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple,
        ReleaseServiceInstanceOnTransactionComplete = false,
        IncludeExceptionDetailInFaults = true)]
    public class CategoryManager : ManagerBase, ICategoryService
    {
        public CategoryManager()
        {
        }

        public CategoryManager(IDataRepositoryFactory repFactory)
        {
            _repositoryFactory = repFactory;
        }

        public CategoryManager(IBusinessEngineFactory busFactory)
        {
            _businessEnginFactory = busFactory;
        }

        public CategoryManager(IDataRepositoryFactory repFactory, IBusinessEngineFactory busFactory)
        {
            _repositoryFactory = repFactory;
            _businessEnginFactory = busFactory;
        }

        [Import]
        IDataRepositoryFactory _repositoryFactory;

        [Import]
        IBusinessEngineFactory _businessEnginFactory;

        [OperationBehavior(TransactionScopeRequired = true)]  // for any update and delete operations
    //    [PrincipalPermission(SecurityAction.Demand, Role = Security.TimeSpentAdminRole)]
        public void DeleteCategory(int id)
        {
           
            ExecuteFaultHandledOperation(() =>
            {
                ICategoryRepository repository = _repositoryFactory.GetRepository<ICategoryRepository>();
                Category category = repository.GetById(id);

                try
                {
                    repository.Delete(category);
                }
                catch (DbUpdateException ex)
                {
                    var sqlException = ex.GetBaseException() as SqlException;
                    if (sqlException != null)
                    {
                        var number = sqlException.Number;
                        if (number == 547)
                        {
                           // throw new CascadeDeleteException("There is an item dependent on this category.");
                            CascadeDeleteException exp = new CascadeDeleteException("There is an item dependent on this category.");
                            throw new FaultException<CascadeDeleteException>(exp, exp.Message);

                        }
                    }
                }


            });
        }

      //  [PrincipalPermission(SecurityAction.Demand, Role = Security.TimeSpentAdminRole)]
     //   [PrincipalPermission(SecurityAction.Demand, Name = Security.TimeSpentUser)]
        public Category[] GetAllCategories()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                ICategoryRepository repository = _repositoryFactory.GetRepository<ICategoryRepository>();
                IEnumerable<Category> categories = repository.Get();
                return categories.ToArray();

            });
        }

        //[PrincipalPermission(SecurityAction.Demand, Role = Security.TimeSpentAdminRole)]
      //  [PrincipalPermission(SecurityAction.Demand, Name = Security.TimeSpentUser)]
        public Category GetCategory(int id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                ICategoryRepository repository = _repositoryFactory.GetRepository<ICategoryRepository>();
                Category category = repository.GetById(id);

                if (category == null)
                {
                    NotFoundException exp = new NotFoundException(string.Format("Category with id {0} not found", id));
                    throw new FaultException<NotFoundException>(exp, exp.Message);

                }
                return category;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
      //  [PrincipalPermission(SecurityAction.Demand, Role = Security.TimeSpentAdminRole)]
        public Category UpdateCategory(Category category)
        {
           
            return ExecuteFaultHandledOperation(() =>
            {
                ICategoryRepository repository = _repositoryFactory.GetRepository<ICategoryRepository>();
                Category updatedCategory = null;

                if (category.CategoryId == 0)
                {
                    updatedCategory = repository.Add(category);
                }
                else
                {
                    updatedCategory = repository.Update(category);
                }
                return updatedCategory;

            });
        }
    }
}
