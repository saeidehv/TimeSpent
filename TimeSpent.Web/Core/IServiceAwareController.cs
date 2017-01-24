using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSpent.Core.Contracts;
using System.ServiceModel;

namespace TimeSpent.Web.Core
{
    public interface IServiceAwareController
    {
        List<IServiceContract> DisposableServices { get; }
        void RegisterDisposableServices(List<IServiceContract> services);
    }
}
