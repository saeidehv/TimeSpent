using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using TimeSpent.Data.Contracts.RepositoryInterfaces;
using TimeSpent.Business.Entities;

namespace TimeSpent.Data
{ 
    [Export(typeof(IProjectRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProjectRepository : DataRepositoryBase<Project>, IProjectRepository
    {
        protected override Project AddEntity(TimeSpentContext context, Project entity)
        {
            return context.ProjectSet.Add(entity);
        }

        protected override IEnumerable<Project> GetEntities(TimeSpentContext context)
        {
            return from p in context.ProjectSet
                   select p;
        }

        protected override Project GetEntity(TimeSpentContext context, int id)
        {
            var query = from p in context.ProjectSet
                        where p.ProjectId == id
                        select p;

            return query.FirstOrDefault();
        }

        protected override Project UpdateEntity(TimeSpentContext context, Project entity)
        {
            var query = from p in context.ProjectSet
                        where p.ProjectId == entity.ProjectId
                        select p;

            return query.FirstOrDefault();
        }
    }
}
