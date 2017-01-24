using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSpent.Core.Contracts;
using TimeSpent.Business.Entities;


namespace TimeSpent.Data.Contracts.RepositoryInterfaces
{
    public interface IProjectRepository:IRepository<Project>
    {
    }
}
