using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeSpent.Web.Core;

namespace TimeSpent.Web.Controllers.MVC
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("Project")]
    public class ProjectController : ViewControllerBase
    {
        // GET: Project
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Add")]
        [Authorize]
        public ActionResult AddProject()
        {
            return View();
        }
    }
}