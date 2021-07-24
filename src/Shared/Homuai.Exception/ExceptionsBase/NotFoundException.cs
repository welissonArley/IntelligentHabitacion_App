using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.ExceptionsBase
{
    [Serializable]
    public class NotFoundException : HomuaiException
    {
        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public NotFoundException(string mensage) : base(mensage)
        {
        }
    }
}
