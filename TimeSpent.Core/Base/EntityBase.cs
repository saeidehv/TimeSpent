using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace TimeSpent.Core
{
    [DataContract]
    public partial class EntityBase : IExtensibleDataObject
    {
        public ExtensionDataObject ExtensionData { get; set; }

    }
}
