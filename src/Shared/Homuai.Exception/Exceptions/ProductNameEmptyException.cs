using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class ProductNameEmptyException : HomuaiException
    {
        protected ProductNameEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ProductNameEmptyException() : base(ResourceTextException.PRODUCT_NAME_EMPTY)
        {
        }
    }
}
