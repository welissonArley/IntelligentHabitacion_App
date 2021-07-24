using Homuai.App.ViewModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Modal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingWithMessagesModal : Rg.Plugins.Popup.Pages.PopupPage
    {
        public LoadingWithMessagesModal(List<string> messages)
        {
            InitializeComponent();

            CloseWhenBackgroundIsClicked = false;
            BindingContext = new LoadingWithMessagesViewModel(messages);
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }

    public class LoadingWithMessagesViewModel : BaseViewModel
    {
        private readonly List<string> _messages;
        private Timer _timer { get; set; }
        private int _index { get; set; }

        public string Operation { get; set; }

        public LoadingWithMessagesViewModel(List<string> messages)
        {
            _messages = messages;
            Operation = messages.First();
            _index = 1;

            Device.BeginInvokeOnMainThread(() =>
            {
                _timer = new Timer(2000);
                _timer.Elapsed += ElapsedTimer;
                _timer.Start();
            });
        }

        private void ElapsedTimer(object sender, ElapsedEventArgs e)
        {
            if (_index < _messages.Count)
            {
                Operation = _messages.ElementAt(_index++);
                OnPropertyChanged(new PropertyChangedEventArgs("Operation"));
            }
            else
            {
                _timer?.Stop();
                _timer?.Dispose();
                _timer = null;
            }
        }
    }
}