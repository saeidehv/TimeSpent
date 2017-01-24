using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSpent.Core.Contracts
{
    public interface IDataRepositoryFactory
    {
        T GetRepository<T>() where T : IRepository;
    }
}
