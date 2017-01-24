using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using TimeSpent.Business.Entities;
using TimeSpent.Data.Contracts.RepositoryInterfaces;
using TimeSpent.Data;

namespace TimeSpent.Data
{
    [Export(typeof(IAccountRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AccountRepository :DataRepositoryBase<Account>, IAccountRepository
    {
       
        protected override Account AddEntity(TimeSpentContext context, Account entity)
        {
            return context.AccountSet.Add(entity);
        }

        protected override IEnumerable<Account> GetEntities(TimeSpentContext context)
        {
            return from a in context.AccountSet
                   select a;
        }

        protected override Account GetEntity(TimeSpentContext context, int id)
        {
            var query = from a in context.AccountSet
                        where a.AccountId == id
                        select a;

            return query.FirstOrDefault();

        }

        protected override Account UpdateEntity(TimeSpentContext context, Account entity)
        {
            var query = from a in context.AccountSet
                        where a.AccountId == entity.AccountId
                        select a;

            return query.FirstOrDefault();
        }


        public Account GetByLogin(string loginName)
        {
            using (TimeSpentContext context = new TimeSpentContext())
            {
                var query = from a in context.AccountSet
                            where a.LoginName == loginName
                            select a;

                return query.FirstOrDefault();
            }

        }

    }
}
