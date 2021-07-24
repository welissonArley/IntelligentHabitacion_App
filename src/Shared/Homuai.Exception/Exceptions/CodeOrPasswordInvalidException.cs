using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class CodeOrPasswordInvalidException : HomuaiException
    {
        protected CodeOrPasswordInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public CodeOrPasswordInvalidException() : base(ResourceTextException.CODE_OR_PASSWORD_INVALID)
        {
        }
    }
}
