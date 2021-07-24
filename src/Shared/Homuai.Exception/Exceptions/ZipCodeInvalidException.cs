using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class ZipCodeInvalidException : HomuaiException
    {
        protected ZipCodeInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ZipCodeInvalidException(string message) : base(message)
        {
        }
    }
}
