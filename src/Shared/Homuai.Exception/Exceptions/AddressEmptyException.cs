using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class AddressEmptyException : HomuaiException
    {
        protected AddressEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public AddressEmptyException() : base(ResourceTextException.ADDRESS_EMPTY)
        {
        }
    }
}
