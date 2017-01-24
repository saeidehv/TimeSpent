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
    [RoutePrefix("category")]
    public class CategoryController :ViewControllerBase
    {
        
        //// GET: Category
        [Authorize]
        public ActionResult Index()
        {
            
            return View();
        }

        [HttpGet]
        [Route("Add")]
        [Authorize]
        public ActionResult AddCategory()
        {
            return View();
        }

      

    }
}