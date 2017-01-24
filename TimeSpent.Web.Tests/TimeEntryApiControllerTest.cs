using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeSpent.Web;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Formatting;
using Moq;
using TimeSpent.Client.Contracts;
using TimeSpent.Client.Entities;
using TimeSpent.Web.Controllers.API;

namespace TimeSpent.Web.Tests
{
    [TestClass]
    public class TimeEntryApiControllerTest
    {
        [TestInitialize]
        public void Initializer()
        {
            _request = GetRequest();
        }

        HttpRequestMessage GetRequest()
        {
            HttpConfiguration config = new HttpConfiguration();
            HttpRequestMessage request = new HttpRequestMessage();
            request.Properties["MS_HttpConfiguration"] = config;
            return request;
        }

        T GetResponse<T>(HttpResponseMessage result)
        {
            ObjectContent<T> content = result.Content as ObjectContent<T>;
            if(content != null)
            {
                T data = (T)(content.Value);
                return data;
            }
            else
            {
                return default(T);  
            }
        }

        HttpRequestMessage _request;

        [TestMethod]
        public void GetTimeEntryHistory()
        {
            Mock<ITimeEntryService> mockTimeEntryService = new Mock<ITimeEntryService>();

            TimeEntry[] entries =
            {
                new TimeEntry() { TimeEntryId = 1 },
                new TimeEntry() { TimeEntryId = 2 }
            };

            mockTimeEntryService.Setup(obj => obj.GetAllTimeEntries()).Returns(entries);

            TimeEntryApiController controller = new TimeEntryApiController(mockTimeEntryService.Object);
            HttpResponseMessage result = controller.GetTimeEntryHistory(_request, DateTime.Now, DateTime.Now.AddDays(1));

            Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.OK);
            TimeEntry[] data = GetResponse<TimeEntry[]>(result);

            Assert.IsTrue(data == entries);
        }
    }
}
