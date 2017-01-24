using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSpent.Core;
using FluentValidation;

namespace TimeSpent.Client.Entities
{
    public class Account : ObjectBase
    {
        private int _id;
        private string _loginName;
        private string _password;
        string _FirstName;
        string _LastName;
        string _Address;

        public int AccountId
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
                OnPropertyChanged(() => AccountId);
            }
        }

        public string LoginName
        {
            get
            {
                return _loginName;
            }

            set
            {
                _loginName = value;
                OnPropertyChanged(() => LoginName);
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
                OnPropertyChanged(() => Password);
            }
        }

        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                if (_FirstName != value)
                {
                    _FirstName = value;
                    OnPropertyChanged(() => FirstName);
                }
            }
        }

        public string LastName
        {
            get { return _LastName; }
            set
            {
                if (_LastName != value)
                {
                    _LastName = value;
                    OnPropertyChanged(() => LastName);
                }
            }
        }

        public string Address
        {
            get { return _Address; }
            set
            {
                if (_Address != value)
                {
                    _Address = value;
                    OnPropertyChanged(() => Address);
                }
            }
        }

        //class AccountValidator : AbstractValidator<Account>
        //{
        //    public AccountValidator()
        //    {
        //        RuleFor(obj => obj.LoginName).NotEmpty();
        //        RuleFor(obj => obj.Password).NotEmpty();
                
        //    }
        //}

        //protected override IValidator GetValidator()
        //{
        //    return new AccountValidator();
        //}

    }
}
