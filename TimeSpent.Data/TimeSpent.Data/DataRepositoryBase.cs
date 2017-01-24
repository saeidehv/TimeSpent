using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSpent.Core.Data;
using TimeSpent.Core.Contracts;

namespace TimeSpent.Data
{
    public abstract class DataRepositoryBase<T> : DataRepositoryBase<T, TimeSpentContext>
        where T : class , IIdentifiableEntity, new()        
    {
    }
}
