using XLabs.Data;

namespace Homuai.App.Model
{
    public class SelectOptionModel : ObservableObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Assigned { get; set; }
    }
}
