using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using TimeSpent.Client.Bootstrapper;
using System.Web.Http;
using TimeSpent.Web.Core;
using WebMatrix.WebData;
using System.Threading;

namespace TimeSpent.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register); 
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            CompositionContainer container = MEFLoader.Init(catalog.Catalogs);

            DependencyResolver.SetResolver(new MefDependencyResolver(container)); // view controllers 
            GlobalConfiguration.Configuration.DependencyResolver = new MefApiDependencyResolver(container); // web api controllers

            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }
    }

    public class SimpleMembershipInitializer
    {
        public SimpleMembershipInitializer()
        {
            //using (var context = new UsersContext())
              //  context.UserProfiles.Find(1);

            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("TimeSpent", "Account", "AccountId", "LoginName", autoCreateTables: false);
        }
    }
}
