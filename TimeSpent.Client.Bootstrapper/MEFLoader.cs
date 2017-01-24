using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.Hosting;
using TimeSpent.Client.Proxies;

namespace TimeSpent.Client.Bootstrapper
{
    public static class MEFLoader
    {
        public static CompositionContainer Init()
        {
            return Init(null);
        }

        public static CompositionContainer Init(ICollection<ComposablePartCatalog> catalogParts )
        {
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add( new AssemblyCatalog(typeof(TimeEntryClient).Assembly));

            // for the types from the client project which should be resolved (for avoiding circular dependency)
            if (catalogParts != null)
                foreach (var part in catalogParts)
                    catalog.Catalogs.Add(part);


            CompositionContainer container = new CompositionContainer(catalog);

            return container;
        }
    }

}
