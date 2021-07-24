using Homuai.App.UseCases.ContactUs;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace Homuai.App.ViewModel.ContactUs
{
    public class ContactUsViewModel : BaseViewModel
    {
        private readonly Lazy<IContactUsUseCase> useCase;
        private IContactUsUseCase _useCase => useCase.Value;

        public string Message { get; set; }

        public ICommand SendMessageCommand { get; }

        public ContactUsViewModel(Lazy<IContactUsUseCase> useCase)
        {
            this.useCase = useCase;
            SendMessageCommand = new Command(async () => await SendMessage());
        }

        private async Task SendMessage()
        {
            try
            {
                SendingData();

                await _useCase.Execute(Message);

                CurrentState = LayoutState.Success;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentState"));
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
    }
}
