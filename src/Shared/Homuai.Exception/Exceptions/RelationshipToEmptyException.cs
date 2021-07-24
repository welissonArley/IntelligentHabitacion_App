using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class RelationshipToEmptyException : HomuaiException
    {
        protected RelationshipToEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public RelationshipToEmptyException() : base(ResourceTextException.RELATIONSHIPTO_EMPTY)
        {
        }
    }
}
