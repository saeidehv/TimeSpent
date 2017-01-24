using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeSpent.Core.Contracts;

namespace TimeSpent.Web.Core
{
    public class ViewControllerBase : Controller
    {
        List<IServiceContract> _disposableServices = null;

        public List<IServiceContract> DisposableServices
        {
            get
            {
                if(_disposableServices == null)
                {
                    _disposableServices = new List<IServiceContract>();
                }
                return _disposableServices;
            }

           
        }

        protected virtual void RegisterServices( List<IServiceContract> disposableServices)
        {

        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            RegisterServices(DisposableServices);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            foreach(var service in DisposableServices)
            {
                if (service != null && service is IDisposable)
                    (service as IDisposable).Dispose();
            }
        }
    }
}