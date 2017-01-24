using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSpent.Core;

namespace TimeSpent.Client.Entities
{
    public class Category:ObjectBase
    {
        private int _id;
        private string _name;
        private string _description;
   //   private bool _active;

              
        //public DateTime? DeletedOn { get; set; }

        //public DateTime CreatedOn { get; set; }

        //public DateTime ModifiedOn { get; set; }

        public int CategoryId
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
                OnPropertyChanged(() => CategoryId);
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
