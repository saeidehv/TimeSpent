using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.Composition;
using TimeSpent.Web.Core;

namespace TimeSpent.Web.Controllers.MVC
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
  //  [Authorize]
    [RoutePrefix("timeentry")]
    public class TimeEntryController : ViewControllerBase
    {
       
        [HttpGet]
        [Authorize]
        public ActionResult TimeEntry()
        {
            return View();
        }

        [HttpGet]
        //[Route("addentry")]
        [Authorize]
        public ActionResult AddEntry()
        {
            return View();
        }


        

    }
}