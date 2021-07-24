using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class CurrentPasswordEmptyException : HomuaiException
    {
        protected CurrentPasswordEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public CurrentPasswordEmptyException() : base(ResourceTextException.CURRENT_PASSWORD_EMPTY)
        {
        }
    }
}
