using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class FriendNotFoundException : NotFoundException
    {
        protected FriendNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public FriendNotFoundException() : base(ResourceTextException.FRIEND_NOT_FOUND)
        {
        }
    }
}
