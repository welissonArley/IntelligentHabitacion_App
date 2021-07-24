using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.ExceptionsBase
{
    [Serializable]
    public class TokenExpiredException : SystemException
    {
        protected TokenExpiredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public TokenExpiredException()
        {

        }
    }
}
