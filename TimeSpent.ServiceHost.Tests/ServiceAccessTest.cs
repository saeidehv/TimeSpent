using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;
using TimeSpent.Business.Contracts;

namespace TimeSpent.ServiceHost.Tests
{
    [TestClass]
    public class ServiceAccessTest
    {
        [TestMethod]
        public void test_time_entry_manager_as_service()
        {
            ChannelFactory<ITimeEntryService> channelFactory = new ChannelFactory<ITimeEntryService>("");

            ITimeEntryService proxy = channelFactory.CreateChannel();

            (proxy as ICommunicationObject).Open();
            channelFactory.Close();

        }
    }
}
