using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class CodeEmptyException : HomuaiException
    {
        protected CodeEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public CodeEmptyException() : base(ResourceTextException.CODE_EMPTY)
        {
        }
    }
}
