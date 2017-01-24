using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.ComponentModel.Composition;
using TimeSpent.Web.Core;
using TimeSpent.Web.Models;

namespace TimeSpent.Web.Controllers.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/account")]
    //[RequireHttps]
    public class AccounApiController : ApiControllerBase
    {
        ISecurityAdapter _securityAdapter;

        [ImportingConstructor]
        public AccounApiController (ISecurityAdapter securityAdapter)
        {
            _securityAdapter = securityAdapter;
        }

        [HttpPost]
        [Route("login")]

        public HttpResponseMessage Login(HttpRequestMessage request, 
            [FromBody] AccountLoginModel model)
        {
            return GetHttpResponse(request, () =>
           {
               HttpResponseMessage response = null;

               bool success = _securityAdapter.Login(model.LoginEmail, model.Password, model.RememberMe);

               if (success)
                   response = request.CreateResponse(HttpStatusCode.OK);
               else
                   response = request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized login");

               return response;
           });
        }

        [HttpPost]
        [Route("register/validate1")]

        public HttpResponseMessage ValidateRegistrationStep1(HttpRequestMessage request,
            [FromBody] AccountRegisterModel model)
        {
            return GetHttpResponse(request, () => 
            {
                HttpResponseMessage response = null;
                List<string> errors = new List<string>();
                // TODO: validate other fields here in case smart user tries to access this API outside of the site
                if (errors.Count == 0)
                    response = request.CreateResponse(HttpStatusCode.OK);
                else
                    response = request.CreateResponse<string[]>(HttpStatusCode.BadRequest, errors.ToArray());

                return response;

            });
          
        }

        [HttpPost]
        [Route("register/validate2")]

        public HttpResponseMessage ValidateRegistrationStep2(HttpRequestMessage request,
         [FromBody] AccountRegisterModel model)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<string> errors = new List<string>();

                // TODO: validate other fields here in case smart user tries to access this API outside of the site

                if (_securityAdapter.UserExists(model.LoginEmail))
                    errors.Add("An account is already registered with this email address");

                if (errors.Count == 0)
                    response = request.CreateResponse(HttpStatusCode.OK);
                else
                    response = request.CreateResponse<string[]>(HttpStatusCode.BadRequest, errors.ToArray());

                return response;

            });

        }


        [HttpPost]
        [Route("register")]

        public HttpResponseMessage CreateAccount(HttpRequestMessage request,
            [FromBody]AccountRegisterModel accountModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // if user tried to create account directly by api call (secure against hacks)
                if(ValidateRegistrationStep1(request, accountModel).IsSuccessStatusCode &&
                   ValidateRegistrationStep2(request, accountModel).IsSuccessStatusCode)
                {
                    _securityAdapter.Register(accountModel.LoginEmail, accountModel.Password,
                        propertyValues: new
                        {
                            FirstName = accountModel.FirstName,
                            LastName = accountModel.LastName,
                            Address = accountModel.Address,
                        /*    City = accountModel.City,
                            State = accountModel.State,
                            ZipCode = accountModel.ZipCode,*****/

                     
                        });

                    _securityAdapter.Login(accountModel.LoginEmail, accountModel.Password, false);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;

            });

        }

        [HttpPost]
        [Route("changepw")]
        [Authorize]
        public HttpResponseMessage ChangePassword(HttpRequestMessage request, [FromBody]AccountChangePasswordModel passwordModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ValidateAuthorizedUser(passwordModel.LoginEmail);

                bool success = _securityAdapter.ChangePassword(passwordModel.LoginEmail, passwordModel.OldPassword, passwordModel.NewPassword);
                if (success)
                    response = request.CreateResponse(HttpStatusCode.OK);
                else
                    response = request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unable to change password.");

                return response;
            });
        }
    }
}
