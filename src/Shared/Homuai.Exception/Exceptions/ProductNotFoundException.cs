using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class ProductNotFoundException : NotFoundException
    {
        protected ProductNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ProductNotFoundException() : base(ResourceTextException.PRODUCT_NOT_FOUND)
        {
        }
    }
}
