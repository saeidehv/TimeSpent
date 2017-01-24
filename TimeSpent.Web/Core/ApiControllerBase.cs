using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security;
using System.Web;
using System.Web.Http;
using System.ServiceModel;
using TimeSpent.Core.Contracts;
using TimeSpent.Core.Exceptions;

namespace TimeSpent.Web.Core
{
    public class ApiControllerBase : ApiController, IServiceAwareController
    {
        List<IServiceContract> _disposableServices;


        List<IServiceContract> IServiceAwareController.DisposableServices
        {
            get
            {
                if (_disposableServices == null)
                {
                    _disposableServices = new List<IServiceContract>();
                }
                return _disposableServices;
            }

        }

        protected virtual void RegisterServices(List<IServiceContract> disposableServices)
        {

        }

         void IServiceAwareController.RegisterDisposableServices(List<IServiceContract> services)
        {
            RegisterServices(services);
        }

        protected void ValidateAuthorizedUser(string userRequested)
        {
            string loginUser = User.Identity.Name;
            if (loginUser != userRequested)
            {
                throw new SecurityException("Attempting to access data for another user.");
            }
        }

        protected HttpResponseMessage GetHttpResponse(HttpRequestMessage request, Func<HttpResponseMessage> codeToExecute)
        {
            HttpResponseMessage response = null;
            try
            {
                response = codeToExecute.Invoke();
            }
            catch (SecurityException exp)
            {
                response = request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, exp.Message);
            }
            catch (FaultException<CascadeDeleteException> exp)
            {
                response = request.CreateResponse(System.Net.HttpStatusCode.MethodNotAllowed, exp.Message);
            }
            catch (FaultException exp)
            {
                response = request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, exp.Message);

            }
            catch (Exception exp)
            {
                response = request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, exp.Message);
            }
            return response;

        }

    }
}