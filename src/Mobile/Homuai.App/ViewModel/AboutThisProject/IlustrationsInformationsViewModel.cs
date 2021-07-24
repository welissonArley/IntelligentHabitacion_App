using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Homuai.App.ViewModel.AboutThisProject
{
    public class IlustrationsInformationsViewModel : BaseViewModel
    {
        public ICommand LinkCommand { protected set; get; }

        public IlustrationsInformationsViewModel()
        {
            LinkCommand = new Command((url) =>
            {
                Launcher.OpenAsync(new Uri(url.ToString()));
            });
        }
    }
}
