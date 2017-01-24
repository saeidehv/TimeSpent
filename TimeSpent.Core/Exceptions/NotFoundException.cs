using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TimeSpent.Core.Exceptions
{
    [Serializable]
    public class NotFoundException:ApplicationException
    {
        public NotFoundException(string message)
            :base(message)
        {

        }


        public NotFoundException(string message , Exception exception)
            :base(message,exception)
        {

        }

        // This protected constructor is used for deserialization.
        protected NotFoundException(SerializationInfo info,
            StreamingContext context) :
             base(info, context)
        { }


    }
}
