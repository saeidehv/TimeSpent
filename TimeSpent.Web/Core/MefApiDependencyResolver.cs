using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using System.ComponentModel.Composition.Hosting;
using TimeSpent.Core.Extensions;

namespace TimeSpent.Web.Core
{
    public class MefApiDependencyResolver : IDependencyResolver
    {
        CompositionContainer _container = null;

        public MefApiDependencyResolver(CompositionContainer container)
        {
            _container = container;
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
            
        }

        public object GetService(Type serviceType)
        {
            return _container.GetExportedValueByType(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetExportedValuesByType(serviceType);
        }
    }
}