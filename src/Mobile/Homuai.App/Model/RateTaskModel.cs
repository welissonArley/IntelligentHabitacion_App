using System;
using XLabs.Data;

namespace Homuai.App.Model
{
    public class RateTaskModel : ObservableObject
    {
        public string TaskId { get; set; }
        public string Room { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public int RatingStars { get; set; }
        public string Feedback { get; set; }
    }
}
