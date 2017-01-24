using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSpent.Core;

namespace TimeSpent.Client.Entities
{
    public class Project:ObjectBase

    {
        private int _Id;

        private string _name;

        private string _description;

        //private bool _active;

        //public DateTime? DeletedOn { get; set; }

        //public DateTime CreatedOn { get; set; }

        //public DateTime ModifiedOn { get; set; }

        public int ProjectId
        {
            get
            {
                return _Id;
            }

            set
            {
                _Id = value;
                OnPropertyChanged(() => ProjectId);
            }
        }

       

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
                OnPropertyChanged(() => Name);
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

        
    }
}
