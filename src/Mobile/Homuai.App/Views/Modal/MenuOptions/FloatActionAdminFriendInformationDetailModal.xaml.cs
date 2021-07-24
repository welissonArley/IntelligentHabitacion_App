using Homuai.App.Services;
using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Ioc;

namespace Homuai.App.Views.Modal.MenuOptions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FloatActionAdminFriendInformationDetailModal : Rg.Plugins.Popup.Pages.PopupPage
    {
        private readonly ICommand _notifyFriendOrderHasArrivedOption;
        private readonly ICommand _changeDateJoinOnOption;
        private readonly ICommand _removeFriendFromHomeOption;

        public FloatActionAdminFriendInformationDetailModal(ICommand notifyFriendOrderHasArrivedOption, ICommand changeDateJoinOnOption, ICommand removeFriendFromHomeOption)
        {
            InitializeComponent();

            _notifyFriendOrderHasArrivedOption = notifyFriendOrderHasArrivedOption;
            _changeDateJoinOnOption = changeDateJoinOnOption;
            _removeFriendFromHomeOption = removeFriendFromHomeOption;

            var userPreferences = Resolver.Resolve<UserPreferences>();

            OptionChangeDateJoinOn.IsVisible = userPreferences.IsAdministrator;
            OptionRemoveFriend.IsVisible = userPreferences.IsAdministrator;
        }

        private async void NotifyFriendOrderHasArrived_Tapped(object sender, System.EventArgs e)
        {
            await CloseThisModal();
            _notifyFriendOrderHasArrivedOption.Execute(null);
        }
        private async void ChangeDateJoinOn_Tapped(object sender, System.EventArgs e)
        {
            await CloseThisModal();
            _changeDateJoinOnOption.Execute(null);
        }
        private async void RemoveFriendFromHome_Tapped(object sender, System.EventArgs e)
        {
            await CloseThisModal();
            _removeFriendFromHomeOption.Execute(null);
        }

        private async Task CloseThisModal()
        {
            var navigation = Resolver.Resolve<INavigation>();
            await navigation.PopPopupAsync();
        }
    }
}