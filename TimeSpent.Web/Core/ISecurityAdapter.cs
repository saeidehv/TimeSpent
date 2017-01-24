using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSpent.Web.Core
{
    public interface ISecurityAdapter
    {
        void Initialize();

        void Register(string loginEmail, string password, object propertyValues);

        bool Login(string loginName, string password, bool rememberMe);

        void Logout();

        bool ChangePassword(string loginName, string oldPassword, string newPassword);

        bool UserExists(string loginName);
    }
}
