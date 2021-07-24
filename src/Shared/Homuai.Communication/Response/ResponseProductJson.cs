using Homuai.Communication.Enums;

namespace Homuai.Communication.Response
{
    public class ResponseProductJson
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Manufacturer { get; set; }
        public ProductType Type { get; set; }
    }
}
