using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class NumberEmptyException : HomuaiException
    {
        protected NumberEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public NumberEmptyException() : base(ResourceTextException.NUMBER_EMPTY)
        {
        }
    }
}
