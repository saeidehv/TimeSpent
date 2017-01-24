using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.Composition.Hosting;
using TimeSpent.Core.Extensions;

namespace TimeSpent.Web.Core
{
    public class MefDependencyResolver : IDependencyResolver
    {
        CompositionContainer _container;

        public MefDependencyResolver(CompositionContainer container)
        {
            _container = container;
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