using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition.Hosting;
using TimeSpent.Data;
using TimeSpent.Business;

namespace TimeSpent.Business.Bootstrapper
{
    public static class MEFLoader
    {
        public static CompositionContainer Init()
        {
            AggregateCatalog catalog = new AggregateCatalog();

            // just one class from every assembly is required, not all classes
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(TimeEntryRepository).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(TimeEntryEngine).Assembly));

            CompositionContainer container = new CompositionContainer(catalog);
            return container;
        }

    }
}
