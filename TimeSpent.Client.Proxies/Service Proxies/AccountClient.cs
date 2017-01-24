using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ComponentModel.Composition;
using TimeSpent.Client.Contracts;
using TimeSpent.Client.Entities;
using TimeSpent.Core.ServiceModel;

namespace TimeSpent.Client.Proxies
{
    [Export(typeof(IAccountService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AccountClient : UserClientBase<IAccountService>, IAccountService
    {
        public Account GetAccountInfo(string userName)
        {
            return Channel.GetAccountInfo(userName);
        }

        public Account UpdateAccountInfo(Account account)
        {
            return Channel.UpdateAccountInfo(account);
        }
    }
}
