using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using TimeSpent.Core.Contracts;
using TimeSpent.Core;

namespace TimeSpent.Data
{
    [Export(typeof(IDataRepositoryFactory))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DataRepositoryFactory : IDataRepositoryFactory
    {
        T IDataRepositoryFactory.GetRepository<T>()
        {
            return ObjectBase.Container.GetExportedValue<T>();
        }
    }
}
