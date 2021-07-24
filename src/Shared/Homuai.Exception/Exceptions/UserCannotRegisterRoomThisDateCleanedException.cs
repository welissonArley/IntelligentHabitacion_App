using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class UserCannotRegisterRoomThisDateCleanedException : HomuaiException
    {
        protected UserCannotRegisterRoomThisDateCleanedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UserCannotRegisterRoomThisDateCleanedException() : base(ResourceTextException.RECORD_CLEANING_ROOM_INVALID_DATE)
        {
        }
    }
}
