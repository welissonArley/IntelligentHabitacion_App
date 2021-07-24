using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class QuantityProductsInvalidException : HomuaiException
    {
        protected QuantityProductsInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public QuantityProductsInvalidException() : base(ResourceTextException.QUANTITY_PRODUCTS_INVALID)
        {
        }
    }
}
