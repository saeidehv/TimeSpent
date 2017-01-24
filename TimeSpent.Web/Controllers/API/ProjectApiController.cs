using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeSpent.Client.Contracts;
using TimeSpent.Client.Entities;
using TimeSpent.Core.Contracts;
using TimeSpent.Web.Core;
using TimeSpent.Web.Models;

namespace TimeSpent.Web.Controllers.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize]
    [RoutePrefix("api/project")]
    [UsesDisposableService]
    public class ProjectApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ProjectApiController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_projectService);
        }

        IProjectService _projectService;

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage AddProject(HttpRequestMessage request, [FromBody]ProjectModel projectModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                Project c = new Project { ProjectId = projectModel.ProjectId, Name = projectModel.Name, Description = projectModel.Description };
                Project project = _projectService.UpdateProject(c);
                response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });
        }

        [HttpGet]
        [Route("list")]
        public HttpResponseMessage GetProjectList(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                Project[] list = _projectService.GetAllProjects();
                response = request.CreateResponse<Project[]>(HttpStatusCode.OK, list);

                return response;
            });
        }

        [HttpPost]
        [Route("delete")]
        public HttpResponseMessage DeleteProject(HttpRequestMessage request, [FromBody]int projectId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _projectService.DeleteProject(projectId);
                response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });

        }
    }
}
