using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class ZipCodeEmptyException : HomuaiException
    {
        protected ZipCodeEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ZipCodeEmptyException() : base(ResourceTextException.ZIPCODE_EMPTY)
        {
        }
    }
}
