using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class NeighborhoodEmptyException : HomuaiException
    {
        protected NeighborhoodEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public NeighborhoodEmptyException() : base(ResourceTextException.NEIGHBORHOOD_EMPTY)
        {
        }
    }
}
