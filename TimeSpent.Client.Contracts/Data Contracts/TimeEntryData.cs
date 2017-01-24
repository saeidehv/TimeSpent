using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;
using TimeSpent.Core.ServiceModel;


namespace TimeSpent.Client.Contracts
{
    [DataContract]
    public class TimeEntryData : DataContractBase
    {
        [DataMember]
        public int TimeEntryId { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public float Duration { get; set; }

        [DataMember]
        public string ProjectName { get; set; }

        [DataMember]
        public string CategoryName { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public string Description { get; set; }
    }

    
}
