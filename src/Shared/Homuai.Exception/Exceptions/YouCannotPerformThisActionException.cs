using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class YouCannotPerformThisActionException : HomuaiException
    {
        protected YouCannotPerformThisActionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public YouCannotPerformThisActionException() : base(ResourceTextException.YOU_CANNNOT_PERMORM_THIS_ACTION)
        {
        }
    }
}
