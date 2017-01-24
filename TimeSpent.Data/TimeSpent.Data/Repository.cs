using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSpent.Core;
using TimeSpent.Core.Contracts;

namespace TimeSpent.Data
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        public IQueryable<T> Table
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IQueryable<T> TableUntracked
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public T Create()
        {
            throw new NotImplementedException();
        }

        public void Delete(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public T GetById(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }

    //public class Repository<T> : IRepository<T> where T : EntityBase
    //{
    //    private readonly DbContext _context;
    //    private IDbSet<T> _entities;

    //    public Repository()
    //    {
    //        this._context = new ButtonContext();
    //    }

    //    public Repository(DbContext context)
    //    {
    //        this._context = context;
    //    }

    //    private DbSet<T> Entities
    //    {
    //        get
    //        {
    //            if (_entities == null)
    //            {
    //                _entities = _context.Set<T>();
    //            }
    //            return _entities as DbSet<T>;
    //        }
    //    }

    //    public IQueryable<T> Table
    //    {
    //        get
    //        {
    //            return this.Entities;
    //        }
    //    }

    //    public T GetById(object id)
    //    {
    //        if (id == null)
    //            throw new ArgumentNullException(nameof(id));

    //        return this.Entities.Find(id);
    //    }

    //    public void Insert(T entity)
    //    {
    //        RunCommand(() =>
    //        {
    //            if (entity == null)
    //                throw new ArgumentNullException(nameof(entity));

    //            this.Entities.Add(entity);
    //            _context.SaveChanges();
    //        });
    //    }

    //    public void Insert(IEnumerable<T> entities)
    //    {
    //        RunCommand(() =>
    //        {
    //            if (entities == null)
    //                throw new ArgumentNullException(nameof(entities));

    //            if (entities.Any())
    //            {
    //                this.Entities.AddRange(entities);
    //                _context.SaveChanges();
    //            }
    //        });
    //    }

    //    public void Update(T entity)
    //    {
    //        RunCommand(() =>
    //        {
    //            if (entity == null)
    //                throw new ArgumentNullException(nameof(entity));

    //            _context.SaveChanges();
    //        });
    //    }

    //    public void Update(IEnumerable<T> entities)
    //    {
    //        RunCommand(() =>
    //        {
    //            if (entities == null)
    //                throw new ArgumentNullException(nameof(entities));

    //            if (entities.Any())
    //            {
    //                _context.SaveChanges();
    //            }
    //        });
    //    }

    //    public void Delete(T entity)
    //    {
    //        RunCommand(() =>
    //        {
    //            if (entity == null)
    //                throw new ArgumentNullException(nameof(entity));

    //            this.Entities.Remove(entity);
    //            _context.SaveChanges();
    //        });
    //    }

    //    public void Delete(IEnumerable<T> entities)
    //    {
    //        RunCommand(() =>
    //        {
    //            if (entities == null)
    //                throw new ArgumentNullException(nameof(entities));

    //            if (entities.Any())
    //            {
    //                this.Entities.RemoveRange(entities);
    //                _context.SaveChanges();
    //            }
    //        });
    //    }

    //    protected void RunCommand(Action action)
    //    {
    //        try
    //        {
    //            action();
    //        }
    //        catch (DbEntityValidationException ex)
    //        {
    //            throw new Exception(GetEntityValidationError(ex));
    //        }
    //    }

    //    protected string GetEntityValidationError(DbEntityValidationException ex)
    //    {
    //        var result = Empty;

    //        foreach (var validationError in ex.EntityValidationErrors)
    //            result += Join(Environment.NewLine, validationError.ValidationErrors.Select(x => Format($"Property Name: {x.PropertyName} Error Message: {x.ErrorMessage}")));

    //        return result;
    //    }
    //}

}
