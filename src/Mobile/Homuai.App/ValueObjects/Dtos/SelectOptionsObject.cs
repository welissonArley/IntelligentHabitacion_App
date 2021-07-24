using Homuai.App.Model;
using System.Collections.Generic;
using System.Windows.Input;

namespace Homuai.App.ValueObjects.Dtos
{
    public class SelectOptionsObject
    {
        public string Title { get; set; }
        public string Phrase { get; set; }
        public string SubTitle { get; set; }
        public ICommand CallbackOnConclude { get; set; }
        public IList<SelectOptionModel> Options { get; set; }
    }
}
