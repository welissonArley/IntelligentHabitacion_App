using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class CityEmptyException : HomuaiException
    {
        protected CityEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public CityEmptyException() : base(ResourceTextException.CITY_EMPTY)
        {
        }
    }
}
