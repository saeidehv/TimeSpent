using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSpent.Common
{
    [Serializable]
    public class AuthorizationValidationException : ApplicationException
    {
        public AuthorizationValidationException(string msg):base(msg)
        {

        }

        public AuthorizationValidationException(string msg, Exception exp):base(msg , exp)
        {

        }
    }
}
