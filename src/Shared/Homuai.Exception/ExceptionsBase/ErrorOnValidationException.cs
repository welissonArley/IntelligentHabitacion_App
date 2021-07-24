using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Homuai.Exception.ExceptionsBase
{
    [Serializable]
    public class ErrorOnValidationException : HomuaiException
    {
        public List<string> ErrorMensages { get; set; }
        public ErrorOnValidationException(List<string> listErrors) : base("")
        {
            ErrorMensages = listErrors;
        }

        protected ErrorOnValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
