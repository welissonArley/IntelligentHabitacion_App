using Homuai.App.ValueObjects.Enum;
using System;
using XLabs.Data;

namespace Homuai.App.Model
{
    public class FoodModel : ObservableObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal Quantity { get; set; }
        public string Manufacturer { get; set; }
        public ProductEnum Type { get; set; }

        public FoodModel Clone()
        {
            return new FoodModel
            {
                Id = Id,
                Name = Name,
                DueDate = DueDate,
                Quantity = Quantity,
                Manufacturer = Manufacturer,
                Type = Type
            };
        }
    }
}
