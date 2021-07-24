using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class NameEmptyException : HomuaiException
    {
        protected NameEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public NameEmptyException() : base(ResourceTextException.NAME_EMPTY)
        {
        }
    }
}
