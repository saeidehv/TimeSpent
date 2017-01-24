using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeSpent.Client.Proxies;

namespace TimeSpent.Client.Tests
{
    [TestClass]
    public class ServiceAccessTests
    {
        [TestMethod]
        public void test_time_entry_client_connection()
        {
            TimeEntryClient proxy = new TimeEntryClient();

            proxy.Open();
        }
    }
}
