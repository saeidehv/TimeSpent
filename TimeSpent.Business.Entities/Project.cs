﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TimeSpent.Core;
using TimeSpent.Core.Contracts;

namespace TimeSpent.Business.Entities
{
    [DataContract]
    public class Project : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int ProjectId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        //public bool Active { get; set; }

        //public DateTime? DeletedOn { get; set; }

        //public DateTime CreatedOn { get; set; }

        //public DateTime ModifiedOn { get; set; }

        public int EntityId
        {
            get
            {
                return ProjectId;
            }

            set
            {
                ProjectId = value;
            }
        }
    }
}
