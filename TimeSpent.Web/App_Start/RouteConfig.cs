using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TimeSpent.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

                routes.MapRoute(
             name: "addtimeEntry",
             url: "timeEntry/addentry/",
             defaults: new { controller = "TimeEntry", action = "addentry" });

            // these routes(default and catch all) need to be defined before the attribute routes 
            //get defined (routes.MapMvcAttributeRoutes)
            routes.MapRoute(
                name: "accountRegisterRoot",
                url: "account/register",
                defaults: new { Controller = "Account", action = "Register" }
                );

            routes.MapRoute(
                name : "accountRegister",
                url : "account/register/{*catchall}",
                defaults : new { Controller = "account" , action = "Register"}
                );

           

            routes.MapRoute(
              name: "timeEntryRoot",
              url: "timeentry",
              defaults: new { controller = "TimeEntry", action = "timeentry" });

            routes.MapRoute(
                name: "timeEntry",
                url: "timeEntry/{*catchall}",
                defaults: new { controller = "TimeEntry", action = "timeentry" });

          

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
