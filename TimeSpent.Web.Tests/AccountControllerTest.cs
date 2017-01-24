using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TimeSpent.Web.Core;
using TimeSpent.Web.Controllers.MVC;
using System.Web.Mvc;
using TimeSpent.Web.Models;

namespace TimeSpent.Web.Tests
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void Login()
        {
            Mock<ISecurityAdapter> mockSecurityAdapter = new Mock<ISecurityAdapter>();
            mockSecurityAdapter.Setup(obj => obj.Initialize());

            string returnUrl = "testreturnurl";

            AccountController accController = new AccountController(mockSecurityAdapter.Object);

            ActionResult result = accController.Login(returnUrl);
            Assert.IsTrue(result is ViewResult);

            ViewResult viewResult = result as ViewResult;
            Assert.IsTrue(viewResult.Model is AccountLoginModel);

            AccountLoginModel model = viewResult.Model as AccountLoginModel;
            Assert.IsTrue(model.ReturnUrl == returnUrl);

        }
    }
}
