using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using TimeSpent.Core.ServiceModel;

namespace TimeSpent.Client.Contracts
{
    [DataContract]
    public class ProjectCategoryReport : DataContractBase
    {

        [DataMember]
        public string x { get; set; }

        [DataMember]
        public float[] y { get; set; }

    }
}
