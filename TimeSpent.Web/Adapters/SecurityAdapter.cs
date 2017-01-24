using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeSpent.Web.Core;
using System.ComponentModel.Composition;
using WebMatrix.WebData;


namespace TimeSpent.Web.Adapters
{
    [Export(typeof(ISecurityAdapter))]
    [PartCreationPolicy(CreationPolicy.NonShared)]

    public class SecurityAdapter:ISecurityAdapter
    {
        public bool ChangePassword(string loginName, string oldPassword, string newPassword)
        {
            return WebSecurity.ChangePassword(loginName, oldPassword, newPassword);
        }

        public void Initialize()
        {
            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("TimeSpent", "Account", "AccountId", "LoginName", autoCreateTables: false);
        }

        public bool Login(string loginName, string password, bool rememberMe)
        {
           
            return WebSecurity.Login(loginName, password,persistCookie: rememberMe);
        }

        public void Logout()
        {
            WebSecurity.Logout();
        }

        public void Register(string loginEmail, string password, object propertyValues)
        {
            WebSecurity.CreateUserAndAccount(loginEmail, password, propertyValues);
        }

        public bool UserExists(string loginName)
        {
            return WebSecurity.UserExists(loginName);
        }
    }
}