using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace TimeSpent.Business.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple,
        ReleaseServiceInstanceOnTransactionComplete = false)]
    public class ProjectManager : ManagerBase, IProjectService
    {
        public ProjectManager()
        {
        }

        public ProjectManager(IDataRepositoryFactory repFactory)
        {
            _repositoryFactory = repFactory;
        }

        public ProjectManager(IBusinessEngineFactory busFactory)
        {
            _businessEnginFactory = busFactory;
        }

        public ProjectManager(IDataRepositoryFactory repFactory, IBusinessEngineFactory busFactory)
        {
            _repositoryFactory = repFactory;
            _businessEnginFactory = busFactory;
        }

        [Import]
        IDataRepositoryFactory _repositoryFactory;

        [Import]
        IBusinessEngineFactory _businessEnginFactory;

        [OperationBehavior(TransactionScopeRequired = true)]  // for any update and delete operations
//        [PrincipalPermission(SecurityAction.Demand, Role = Security.TimeSpentAdminRole)]
        public void DeleteProject(int id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                IProjectRepository repository = _repositoryFactory.GetRepository<IProjectRepository>();
                Project entry = repository.GetById(id);

                try
                {
                    repository.Delete(entry);
                }
                catch (DbUpdateException ex)
                {
                    var sqlException = ex.GetBaseException() as SqlException;
                    if (sqlException != null)
                    {
                        var number = sqlException.Number;
                        if (number == 547)
                        {
                            //throw new CascadeDeleteException("There is an item dependent on this project.");
                            CascadeDeleteException exp = new CascadeDeleteException("There is an item dependent on this project.");
                            throw new FaultException<CascadeDeleteException>(exp , exp.Message);

                        }
                    }
                }


            });
        }

  //      [PrincipalPermission(SecurityAction.Demand, Role = Security.TimeSpentAdminRole)]
  //      [PrincipalPermission(SecurityAction.Demand, Name = Security.TimeSpentUser)]
        public Project[] GetAllProjects()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IProjectRepository repository = _repositoryFactory.GetRepository<IProjectRepository>();
                IEnumerable<Project> projects = repository.Get();
                return projects.ToArray();
            });
        }

  //      [PrincipalPermission(SecurityAction.Demand, Role = Security.TimeSpentAdminRole)]
   //     [PrincipalPermission(SecurityAction.Demand, Name = Security.TimeSpentUser)]
        public Project GetProject(int id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IProjectRepository repository = _repositoryFactory.GetRepository<IProjectRepository>();
                Project project = repository.GetById(id);

                if (project == null)
                {
                    NotFoundException exp = new NotFoundException(string.Format("Project with id {0} not found", id));
                    throw new FaultException<NotFoundException>(exp, exp.Message);

                }
                return project;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
    //    [PrincipalPermission(SecurityAction.Demand, Role = Security.TimeSpentAdminRole)]
        public Project UpdateProject(Project project)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IProjectRepository repository = _repositoryFactory.GetRepository<IProjectRepository>();
                Project updatedProject = null;

                if (project.ProjectId == 0)
                {
                    updatedProject = repository.Add(project);
                }
                else
                {
                    updatedProject = repository.Update(project);
                }
                return updatedProject;

            });
        }
    }
}
