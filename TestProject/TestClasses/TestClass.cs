using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSpent.Core;
using FluentValidation.Validators;
using FluentValidation;
using FluentValidation.Results;

namespace Core.Test
{
    class TestClass: ObjectBase
    {
        string _cleanProp = null;
        string _stringProp = null;
        string _dirtyProp = null;

        public string CleanProp
        {
            get
            {
                return _cleanProp;
            }

            set
            {
                if (_cleanProp == value)
                    return;

                _cleanProp = value;
                OnPropertyChanged(() => CleanProp, false);
            }
        }

        public string StringProp
        {
            get
            {
                return _stringProp;
            }

            set
            {
                if (_stringProp == value)
                    return;

                _stringProp = value;
                OnPropertyChanged(() => StringProp);
            }
        }

        public string DirtyProp
        {
            get
            {
                return _dirtyProp;
            }

            set
            {
                if (value == _dirtyProp)
                    return;

                _dirtyProp = value;
                OnPropertyChanged(() => DirtyProp);
            }
        }

        public class TestClassValidator : AbstractValidator<TestClass>
        {
            public TestClassValidator()
            {
                RuleFor(obj => obj.StringProp).NotEmpty();
            }
        }

        protected override IValidator GetValidator()
        {
            return new TestClassValidator();
        }
    }
}
