using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSpent.Core.Contracts
{
    public interface IAccountOwnedEntity
    {
        //string OwnerAccountId { get; }
        int OwnerAccountId { get; }
    }
}
