using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.Serialization;
using TimeSpent.Core;
using TimeSpent.Core.Contracts;

namespace TimeSpent.Business.Entities
{
    [DataContract]
    public class Account : EntityBase, IIdentifiableEntity, IAccountOwnedEntity
    {
        
        [DataMember]
        public int AccountId { get; set; }
        
        [DataMember]
        public string LoginName { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Address { get; set; }

        //public bool Active { get; set; }

        //public DateTime? DeletedOn { get; set; }

        //public DateTime CreatedOn { get; set; }

        //public DateTime ModifiedOn { get; set; }

        //public int OwnerAccountId
        //{
        //    get
        //    {
        //        return AccountId;
        //    }
        //}

        int IAccountOwnedEntity.OwnerAccountId
        {
            get
            {
                return AccountId;
            }
        }

        public int EntityId
        {
            get
            {
                return AccountId;
            }
            set
            {
                AccountId = value;
            }
         }
    }
}
