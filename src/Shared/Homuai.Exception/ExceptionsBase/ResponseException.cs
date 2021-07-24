using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.ExceptionsBase
{
    [Serializable]
    public class ResponseException : SystemException
    {
        protected ResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ResponseException()
        {

        }

        public string Token { get; set; }
        public object Exception { get; set; }
    }
}
