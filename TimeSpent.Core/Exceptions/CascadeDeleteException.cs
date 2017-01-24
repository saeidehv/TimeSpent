using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace TimeSpent.Core.Exceptions

{
    //[DataContract]
    [Serializable()]
    public class CascadeDeleteException : ApplicationException
    {

        public CascadeDeleteException(string message)
            :base(message)
        {

        }
        

        public CascadeDeleteException(string message, Exception exp)
            :base(message, exp)
        {

        }

        // This protected constructor is used for deserialization.
        protected CascadeDeleteException(SerializationInfo info,
            StreamingContext context) :
             base(info, context)
        { }

       
        

    }
}
