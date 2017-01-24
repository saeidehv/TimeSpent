using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using TimeSpent.Core;
using TimeSpent.Core.Contracts;

namespace TimeSpent.Business.Entities
{
    [DataContract]
    public class TimeEntry : EntityBase, IIdentifiableEntity, IAccountOwnedEntity
    {
        [DataMember]
        public int TimeEntryId { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        //[DataMember]
        //public TimeSpan? StartTime { get; set; }

        //[DataMember]
        //public TimeSpan? StopTime { get; set; }

        [DataMember]
        public float Duration { get; set; }

        [DataMember]
        public float Miles { get; set; }
    
        [DataMember]
        public int AccountId { get; set; }

        [DataMember]
        public int ProjectId { get; set; }

        [DataMember]
        public int CategoryId { get; set; }

        [DataMember]
        public string Description { get; set; }

        //[DataMember]
        //public float Mileage { get; set; }

        //public bool Active { get; set; }

        //public DateTime? DeletedOn { get; set; }

        //public DateTime CreatedOn { get; set; }

        // public DateTime ModifiedOn { get; set; }

        public Project Project { get; set; }
        public Category Category { get; set; }

        public int EntityId
        {
            get
            {
                return TimeEntryId;
            }
            set
            {
                TimeEntryId = value;
            }
        }

        int IAccountOwnedEntity.OwnerAccountId
        {
            get
            {
                return AccountId;
            }
        }
    }
}
