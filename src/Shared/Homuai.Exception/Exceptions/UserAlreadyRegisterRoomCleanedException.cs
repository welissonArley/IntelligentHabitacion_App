using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class UserAlreadyRegisterRoomCleanedException : HomuaiException
    {
        protected UserAlreadyRegisterRoomCleanedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UserAlreadyRegisterRoomCleanedException() : base(ResourceTextException.THERE_IS_CLEAN_ROOM_RECORD_THIS_DATE)
        {
        }
    }
}
