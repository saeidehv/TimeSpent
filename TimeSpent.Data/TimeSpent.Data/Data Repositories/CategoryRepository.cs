using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSpent.Core.Data;
using TimeSpent.Business.Entities;
using System.ComponentModel.Composition;
using TimeSpent.Data.Contracts.RepositoryInterfaces;


namespace TimeSpent.Data
{
    [Export( typeof( ICategoryRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CategoryRepository : DataRepositoryBase<Category>, ICategoryRepository
    {
        protected override Category AddEntity(TimeSpentContext context, Category entity)
        {
            return context.CategorySet.Add(entity);
        }

        protected override IEnumerable<Category> GetEntities(TimeSpentContext context)
        {
            return from c in context.CategorySet
                   select c;
        }

        protected override Category GetEntity(TimeSpentContext context, int id)
        {
            var query = from c in context.CategorySet
                        where c.CategoryId == id
                        select c;
            return query.FirstOrDefault();
        }

        protected override Category UpdateEntity(TimeSpentContext context, Category entity)
        {
            var query = from c in context.CategorySet
                        where c.CategoryId == entity.CategoryId
                        select c;
            return query.FirstOrDefault();
        }
    }
}
