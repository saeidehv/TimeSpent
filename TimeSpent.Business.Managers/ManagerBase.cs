using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSpent.Core;
using System.ComponentModel.Composition;
using System.ServiceModel;
using TimeSpent.Business.Entities;
using TimeSpent.Core.Contracts;
using System.Threading;
using TimeSpent.Common;
using TimeSpent.Core.Extensions;
using TimeSpent.Core.Exceptions;


namespace TimeSpent.Business.Managers
{
    public class ManagerBase
    {
        protected string _loginName = string.Empty;
        protected Account _authorizationAccount = null;

        public ManagerBase()
        {
            OperationContext context = OperationContext.Current;
            if( context != null)
            {
                try
                {
                    _loginName = context.IncomingMessageHeaders.GetHeader<string>("String", "System");

                    if (_loginName.IndexOf(@"\") > -1)   // if it's a windows user 
                        _loginName = string.Empty;
                }
                catch(Exception)
                {
                    _loginName = string.Empty;
                }
            }


            if(ObjectBase.Container != null)
               ObjectBase.Container.SatisfyImportsOnce(this);

            if (_loginName.HasValue())
                _authorizationAccount = LoadAuthorizationValidationAccount(_loginName);
        }


        protected void ValidateAuthorization(IAccountOwnedEntity entity)
        {
            if(!Thread.CurrentPrincipal.IsInRole(Security.TimeSpentAdminRole))
            {
                if(_authorizationAccount != null)
                {
                    if(_loginName != string.Empty && entity.OwnerAccountId != _authorizationAccount.EntityId) 
                    {
                        AuthorizationValidationException exp = new AuthorizationValidationException("Attempt to access a secure record with improper user authorization validation.");
                        throw new FaultException<AuthorizationValidationException>(exp, exp.Message);
                    }
                }

            }
        }

        protected virtual Account LoadAuthorizationValidationAccount(string loginName)
        {
            return null;
        }

        protected T ExecuteFaultHandledOperation<T>(Func<T> codeToExecute)
        {
            try
            {
                return codeToExecute.Invoke();           
            }
            catch (AuthorizationValidationException ex)
            {
                throw new FaultException<AuthorizationValidationException>(ex, ex.Message);
            }
            catch (FaultException exp)
            {
                throw exp;
            }
            catch (Exception exp)
            {
                throw new FaultException(exp.Message);
            }
        }

        protected void ExecuteFaultHandledOperation(Action codeToExecute )
        {
            try
            {
                codeToExecute.Invoke();
            }
            catch (AuthorizationValidationException ex)
            {
                throw new FaultException<AuthorizationValidationException>(ex, ex.Message);
            }
            catch (FaultException exp)
            {
                throw exp;
            }
            catch (Exception exp)
            {
                throw new FaultException(exp.Message);
            }

        }

    }

    
}
