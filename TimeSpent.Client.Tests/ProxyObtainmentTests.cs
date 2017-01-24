using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeSpent.Client;
using TimeSpent.Core;
using TimeSpent.Client.Bootstrapper;
using TimeSpent.Client.Contracts;
using TimeSpent.Client.Proxies;
using TimeSpent.Core.Contracts;

namespace TimeSpent.Client.Tests
{
    [TestClass]
    public class ProxyObtainmentTests
    {
        [TestInitialize]
        public void Initialize()
        {
            ObjectBase.Container = MEFLoader.Init();
        }

        [TestMethod]
        public void obtain_proxy_from_container_using_service_contract()
        {
            ITimeEntryService proxy = ObjectBase.Container.GetExportedValue<ITimeEntryService>();
            Assert.IsTrue(proxy is ITimeEntryService);
        }


        [TestMethod]
        public void obtain_proxy_from_service_factory()
        {
            IServiceFactory factory = new ServiceFactory();
            ITimeEntryService proxy = factory.CreateClient<ITimeEntryService>();

            Assert.IsTrue(proxy is TimeEntryClient);
        }

        [TestMethod]
        public void obtain_service_factory_and_proxy_from_container()
        {
            IServiceFactory factory = ObjectBase.Container.GetExportedValue<IServiceFactory>();
            ITimeEntryService proxy = factory.CreateClient<ITimeEntryService>();

            Assert.IsTrue(proxy is TimeEntryClient);
        }
    }
}
