using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSpent.Core;
using FluentValidation;

namespace TimeSpent.Client.Entities
{
    public class TimeEntry : ObjectBase
    {
        private int _timeEntryId;
        private DateTime _date;
        //private TimeSpan? _start;
        //private TimeSpan? _stop;
        private float _duration; 
        private string _description;
        private int _accountId;
        private float _miles;
        private int _projectId;
        private int _categoryId;

        public int TimeEntryId
        {
            get { return _timeEntryId; }
            set
            {
                _timeEntryId = value;
                OnPropertyChanged(() => TimeEntryId);
            }
        }
        
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged(() => Date);
            }
        }

        public float Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                _duration = value;
                OnPropertyChanged(() => Duration);
            }
        }

        public float Miles
        {
            get
            {
                return _miles;
            }
            set
            {
                _miles = value;
                OnPropertyChanged(() => Miles);
            }
        }


        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
                OnPropertyChanged(() => Description);
            }
        }

        public int AccountId
        {
            get { return _accountId;  }

            set
            {
                _accountId = value;
                OnPropertyChanged(() => AccountId); 
            }
        }

        public int ProjectId
        {
            get { return _projectId; }

            set
            {
                _projectId = value;
                OnPropertyChanged(() => ProjectId);
            }
        }

        public int CategoryId
        {
            get { return _categoryId; }

            set
            {
                _categoryId = value;
                OnPropertyChanged(() => AccountId);
            }
        }


        class TimeEntryValidator : AbstractValidator<TimeEntry>
        {
            public TimeEntryValidator()
            {
                RuleFor(obj => obj.Date).NotEmpty();
                RuleFor(obj => obj.Duration).GreaterThan(0);
                RuleFor(obj => obj.ProjectId).NotEmpty();
                RuleFor(obj => obj.AccountId).NotEmpty();
                RuleFor(obj => obj.CategoryId).NotEmpty();
            }
        }

        protected override IValidator GetValidator()
        {
            return new TimeEntryValidator();
        }


    }
}
