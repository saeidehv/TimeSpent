using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.Composition;
using TimeSpent.Web.Core;
using TimeSpent.Web.Models;

namespace TimeSpent.Web.Controllers.MVC
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("account")]
    public class AccountController : ViewControllerBase
    {
        ISecurityAdapter _securityAdapter;

        [ImportingConstructor]
        public AccountController(ISecurityAdapter securityAdapter)
        {
            _securityAdapter = securityAdapter;
        }
     
        [HttpGet]
        [Route("login")]
        public ActionResult Login(string returnUrl)
        {
            _securityAdapter.Initialize();
            return View(new AccountLoginModel { ReturnUrl = returnUrl });

        }

        [HttpGet]
        [Route("Logout")]
        public ActionResult Logout()
        {
            _securityAdapter.Logout();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        // route in routeconfig
        public ActionResult Register()
        {
            _securityAdapter.Initialize();
            return View();
        }

        [HttpGet]
        [Route("changepassword")]
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpGet]
        [Route("forgotpassword")]
        [Authorize]
        public ActionResult ForgotPassword()
        {
            return View();
        }

    }
}