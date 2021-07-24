using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class EmailAlreadyBeenRegisteredException : HomuaiException
    {
        protected EmailAlreadyBeenRegisteredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public EmailAlreadyBeenRegisteredException() : base(ResourceTextException.EMAIL_ALREADYBEENREGISTERED)
        {
        }
    }
}
