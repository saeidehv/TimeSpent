using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSpent.Core;
using TimeSpent.Core.Contracts;
using System.Data.Entity;
using TimeSpent.Core.Utils;

namespace TimeSpent.Core.Data
{
    public abstract class DataRepositoryBase<T, U> : IRepository<T>
        where T : class, IIdentifiableEntity, new()
        where U : DbContext, new()
    {

        protected abstract T AddEntity(U context, T entity);
        protected abstract T GetEntity(U context , int id);
        protected abstract IEnumerable<T> GetEntities(U context);
        protected abstract T UpdateEntity(U context, T entity);
        
        public T Add(T entity)
        {
            using (U context = new U())
            {
                AddEntity(context, entity);
                context.SaveChanges();
                return entity;
                
            }
        }

        public void Delete(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            using (U context = new U())
            {
                T entity = GetEntity(context , id);
                context.Entry<T>(entity).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public void Delete(T entity)
        {
            using (U context = new U())
            {
                context.Entry<T>(entity).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public T GetById(int id)
        {
            using (U context = new U())
            {
                return GetEntity(context, id);
            }
        }

        public IEnumerable<T> Get()
        {
            using (U context = new U())
            {
                return GetEntities(context).ToList();
            }

        }

        public void Update(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public T Update(T entity)
        {
            using (U context = new U())
            {
                T origin = UpdateEntity(context, entity);
                SimpleMapper.PropertyMap(entity, origin);
                context.SaveChanges();
                return origin;

            }
        }

        public IQueryable<T> Table
        {
            get
            {
                throw new NotImplementedException();
            }
        }

    }

   

}
