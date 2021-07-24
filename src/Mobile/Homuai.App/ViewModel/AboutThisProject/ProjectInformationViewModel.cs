using Homuai.App.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace Homuai.App.ViewModel.AboutThisProject
{
    public class ProjectInformationViewModel : BaseViewModel
    {
        public string UserName { get; }
        public string ProfileColor { get; }

        public ICommand PrivacyPolicyCommand { get; }
        public ICommand TermsOfUseCommand { get; }
        public ICommand ShowMeTheLinksCommand { get; }

        public ProjectInformationViewModel(UserPreferences userPreferences)
        {
            UserName = userPreferences.Name;
            ProfileColor = userPreferences.ProfileColor();

            PrivacyPolicyCommand = new Command(async () => await Navigation.PushAsync<PrivacyPolicyViewModel>());
            TermsOfUseCommand = new Command(async () => await Navigation.PushAsync<TermsOfUseViewModel>());
            ShowMeTheLinksCommand = new Command(async () => await Navigation.PushAsync<IlustrationsInformationsViewModel>());
        }
    }
}
