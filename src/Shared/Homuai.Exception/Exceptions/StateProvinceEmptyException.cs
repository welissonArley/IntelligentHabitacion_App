using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class StateProvinceEmptyException : HomuaiException
    {
        protected StateProvinceEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public StateProvinceEmptyException() : base(ResourceTextException.STATEPROVINCE_EMPTY)
        {
        }
    }
}
