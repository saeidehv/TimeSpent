using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.ComponentModel.Composition;
using TimeSpent.Web.Core;
using TimeSpent.Client.Contracts;
using TimeSpent.Core.Contracts;
using TimeSpent.Client.Entities;

namespace TimeSpent.Web.Controllers.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize]
    [RoutePrefix("api/user")]
    [UsesDisposableService]
    public class UserApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public UserApiController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_accountService);
        }

        IAccountService _accountService;

        [HttpGet]
        [Route("account")]

        public HttpResponseMessage GetAccountInfo( HttpRequestMessage request )
        {
            return GetHttpResponse(request, () =>
           {
               HttpResponseMessage response = null;

               Account account = _accountService.GetAccountInfo(User.Identity.Name);

               response = request.CreateResponse<Account>(HttpStatusCode.OK, account);

               return response;
           });
        }

        [HttpPost]
        [Route("account")]

        public HttpResponseMessage UpdateUserAccountInfo(HttpRequestMessage request , Account account)
        {
            return GetHttpResponse(request, () =>
          {
              HttpResponseMessage response = null;
              ValidateAuthorizedUser(account.LoginName);
              
              // TODO : Validation need to be added here

              Account updatedAccount = _accountService.UpdateAccountInfo(account);
              response = request.CreateResponse(HttpStatusCode.OK, updatedAccount);

              return response;
          });
        }
    }
}
