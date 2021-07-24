using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Ioc;

namespace Homuai.App.Views.Modal.MenuOptions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FloatActionTaskCleaningScheduleModal : Rg.Plugins.Popup.Pages.PopupPage
    {
        private ICommand RegisterRoomsCleanedCommand { get; }
        private ICommand RememberUserCleanRoomCommand { get; }
        private ICommand SelectViewCompleteHistoryCommand { get; }

        public FloatActionTaskCleaningScheduleModal(ICommand registerRoomsCleanedCommand,
            ICommand rememberUserCleanRoom, ICommand selectViewCompleteHistoryCommand)
        {
            InitializeComponent();

            RegisterRoomsCleanedCommand = registerRoomsCleanedCommand;
            RememberUserCleanRoomCommand = rememberUserCleanRoom;
            SelectViewCompleteHistoryCommand = selectViewCompleteHistoryCommand;
        }

        private async Task CloseThisModal()
        {
            var navigation = Resolver.Resolve<INavigation>();
            await navigation.PopPopupAsync();
        }

        private async void RegisterRoomsCleaned_Tapped(object sender, System.EventArgs e)
        {
            await CloseThisModal();
            RegisterRoomsCleanedCommand.Execute(null);
        }

        private async void RememberUserCleanRoom_Tapped(object sender, System.EventArgs e)
        {
            await CloseThisModal();
            RememberUserCleanRoomCommand.Execute(null);
        }
        private async void ViewCompleteHistory_Tapped(object sender, System.EventArgs e)
        {
            await CloseThisModal();
            SelectViewCompleteHistoryCommand.Execute(null);
        }
    }
}