using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSpent.Core.Contracts
{
    public interface IRepository
    {

    }

    public interface IRepository<T> : IRepository
       where T : class,IIdentifiableEntity,new()
        {
        IQueryable<T> Table { get; }

        T GetById(int  id);
        IEnumerable<T> Get();

        T Add(T entity);

        T Update(T entity);

        void Update(IEnumerable<T> entities);

        void Delete(T entity);

        void Delete(IEnumerable<T> entities);

    }
    
}
