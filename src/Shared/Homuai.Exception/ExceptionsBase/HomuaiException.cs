using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.ExceptionsBase
{
    [Serializable]
    public class HomuaiException : SystemException
    {
        protected HomuaiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public HomuaiException(string mensagem) : base(mensagem)
        {
        }
    }
}
