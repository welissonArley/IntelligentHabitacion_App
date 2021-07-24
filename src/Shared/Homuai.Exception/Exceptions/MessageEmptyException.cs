using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class MessageEmptyException : HomuaiException
    {
        protected MessageEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public MessageEmptyException() : base(ResourceTextException.MESSAGE_EMPTY)
        {
        }
    }
}
