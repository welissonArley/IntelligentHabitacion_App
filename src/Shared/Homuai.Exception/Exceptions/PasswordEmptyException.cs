using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class PasswordEmptyException : HomuaiException
    {
        protected PasswordEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public PasswordEmptyException() : base(ResourceTextException.PASSWORD_EMPTY)
        {
        }
    }
}
