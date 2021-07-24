using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class UserIsPartOfAHomeException : HomuaiException
    {
        protected UserIsPartOfAHomeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UserIsPartOfAHomeException() : base(ResourceTextException.USER_IS_PART_OF_A_HOME)
        {
        }
    }
}
