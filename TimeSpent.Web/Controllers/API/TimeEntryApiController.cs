using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.ComponentModel.Composition;
using TimeSpent.Web.Core;
using TimeSpent.Client.Contracts;
using TimeSpent.Client.Entities;
using TimeSpent.Core.Contracts;
using TimeSpent.Web.Models;

namespace TimeSpent.Web.Controllers.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize]
    [RoutePrefix("api/timeentry")]
    [UsesDisposableService]
    public class TimeEntryApiController : ApiControllerBase
    {
       
        [ImportingConstructor]
        public TimeEntryApiController( ITimeEntryService timeEntryService)
        {
            _timeEntryService = timeEntryService;
        }

        
        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_timeEntryService);
        }

        ITimeEntryService _timeEntryService;
        
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage AddEntry(HttpRequestMessage request, [FromBody]TimeEntry entryModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                TimeEntry e = new TimeEntry {
                    TimeEntryId = entryModel.TimeEntryId,
                    Duration = entryModel.Duration,
                    CategoryId = entryModel.CategoryId,
                    ProjectId = entryModel.ProjectId,
                    Date = entryModel.Date,
                    Description = entryModel.Description,
                    
                };
                TimeEntry entry = _timeEntryService.UpdateTimeEntry(User.Identity.Name , e);
                response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });
        }

        [HttpGet]
        [Route("list")]
        public HttpResponseMessage GetTimeEntryList(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                TimeEntryData[] list = _timeEntryService.GetTimeEntriesByAccount(User.Identity.Name);

                response = request.CreateResponse<TimeEntryData[]>(HttpStatusCode.OK, list);
                return response;
            });
        }

        [HttpGet]
        [Route("history/{fromDate}/{toDate}")]
        // route and parameters shall be the same (case sensitive)
        public HttpResponseMessage GetTimeEntryHistory(HttpRequestMessage request, DateTime fromDate , DateTime toDate)
        {
            return GetHttpResponse(request, () =>
           {
               string user = User.Identity.Name;

               HttpResponseMessage response = null;
               TimeEntry[]  list = _timeEntryService.GetAllTimeEntries();

               response = request.CreateResponse<TimeEntry[]>(HttpStatusCode.OK, list);
               return response;
           });
        }

        [HttpGet]
        [Route("dashboard/projectcategoryreport")]
        // route and parameters shall be the same (case sensitive)
        public HttpResponseMessage GetProjectCategoryReport(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string user = User.Identity.Name;

                HttpResponseMessage response = null;
                ProjectCategoryReport[] list = _timeEntryService.GetProjectCategoryReport(user);

                response = request.CreateResponse<ProjectCategoryReport[]>(HttpStatusCode.OK, list);
                return response;
            });
        }

        [HttpGet]
        [Route("dashboard/projectreport")]
        // route and parameters shall be the same (case sensitive)
        public HttpResponseMessage GetProjectReport(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string user = User.Identity.Name;

                HttpResponseMessage response = null;
                ProjectCategoryReport[] list = _timeEntryService.GetProjectReport(user);

                response = request.CreateResponse<ProjectCategoryReport[]>(HttpStatusCode.OK, list);
                return response;
            });
        }


        [HttpPost]
        [Route("delete")]
        public HttpResponseMessage DeleteTimeEntry(HttpRequestMessage request,[FromBody]int itemId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _timeEntryService.DeleteTimeEntry(itemId);
                response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });
        }

        [HttpPost]
        [Route("get")]
        public HttpResponseMessage GetTimeEntry(HttpRequestMessage request, [FromBody]int itemId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                TimeEntry entry = _timeEntryService.GetTimeEntry(itemId);
                response = request.CreateResponse<TimeEntry>(HttpStatusCode.OK, entry);

                return response;
            });
        }


    }
}
