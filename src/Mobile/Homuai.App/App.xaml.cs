using Com.OneSignal;
using Com.OneSignal.Abstractions;
using Homuai.App.Notifications;
using Homuai.App.ViewModel.AboutThisProject;
using Homuai.App.ViewModel.CleaningSchedule;
using Homuai.App.ViewModel.ContactUs;
using Homuai.App.ViewModel.Dashboard.NotPartOfHome;
using Homuai.App.ViewModel.Dashboard.PartOfHome;
using Homuai.App.ViewModel.Friends;
using Homuai.App.ViewModel.Home.Informations;
using Homuai.App.ViewModel.Home.Register;
using Homuai.App.ViewModel.Login.DoLogin;
using Homuai.App.ViewModel.MyFoods;
using Homuai.App.ViewModel.Start;
using Homuai.App.ViewModel.User.Register;
using Homuai.App.ViewModel.User.Update;
using Homuai.App.Views.Modal;
using Homuai.App.Views.View.AboutThisProject;
using Homuai.App.Views.View.CleaningSchedule;
using Homuai.App.Views.View.ContactUs;
using Homuai.App.Views.View.Dashboard.NotPartOfHome;
using Homuai.App.Views.View.Dashboard.PartOfHome;
using Homuai.App.Views.View.Friends;
using Homuai.App.Views.View.Home.Informations;
using Homuai.App.Views.View.Home.Register;
using Homuai.App.Views.View.Login.DoLogin;
using Homuai.App.Views.View.MyFoods;
using Homuai.App.Views.View.Start;
using Homuai.App.Views.View.User.Register;
using Homuai.App.Views.View.User.Update;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Forms.Services;
using XLabs.Ioc;
using XLabs.Platform.Services;

namespace Homuai.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            SetAppTheme();

            OneSignalSettings();

            SetDependencyInjection();

            RegisterViews();

            MainPage = new InitializePage();
        }

        private void SetAppTheme()
        {
            Current.UserAppTheme = Current.RequestedTheme == OSAppTheme.Unspecified ? OSAppTheme.Light : Current.RequestedTheme;
        }

        private void SetDependencyInjection()
        {
            Resolver.Resolve<IDependencyContainer>()
                .Register<INavigationService>(t => new NavigationService(MainPage.Navigation)) // New Xlabs nav service
                .Register(t => MainPage.Navigation); // Old Xlabs nav service
        }

        private void OneSignalSettings()
        {
            OneSignal.Current.StartInit(Services.Communication.Notifications.OneSignalKey)
                .InFocusDisplaying(OSInFocusDisplayOption.None)
                .HandleNotificationReceived((notification) =>
                {
                    ManagerNotification.Notification(notification);
                    if (!notification.shown) // if the "show" is false, this means that the app is in focus.
                        Current.MainPage.Navigation.PushPopupAsync(new NotifyModal(notification.payload.title, notification.payload.body));
                }).EndInit();

            OneSignal.Current.RegisterForPushNotifications();

            OneSignal.Current.IdsAvailable(OneSignalId);
        }
        private static void OneSignalId(string playerID, string pushToken)
        {
            Services.Communication.Notifications.SetMyIdOneSignal(playerID);
        }

        private void RegisterViews()
        {
            ViewFactory.Register<GetStartedPage, GetStartedViewModel>();
            ViewFactory.Register<LoginPage, LoginViewModel>();
            ViewFactory.Register<Views.View.Login.ForgotPassword.RequestEmailPage, ViewModel.Login.ForgotPassword.RequestEmailViewModel>();
            ViewFactory.Register<Views.View.Login.ForgotPassword.ResetPasswordPage, ViewModel.Login.ForgotPassword.ResetPasswordViewModel>();
            ViewFactory.Register<RequestNamePage, RequestNameViewModel>();
            ViewFactory.Register<RequestPhoneNumberPage, RequestPhoneNumberViewModel>();
            ViewFactory.Register<RequestEmailPage, RequestEmailViewModel>();
            ViewFactory.Register<RequestEmergencyContact1Page, RequestEmergencyContact1ViewModel>();
            ViewFactory.Register<RequestEmergencyContact2Page, RequestEmergencyContact2ViewModel>();
            ViewFactory.Register<RequestPasswordPage, RequestPasswordViewModel>();
            ViewFactory.Register<UserInformationPage, UserInformationViewModel>();
            ViewFactory.Register<ChangePasswordPage, ChangePasswordViewModel>();
            ViewFactory.Register<MyFriendsPage, MyFriendsViewModel>();
            ViewFactory.Register<FriendInformationsDetailsPage, FriendInformationsDetailsViewModel>();
            ViewFactory.Register<MyFoodsPage, MyFoodsViewModel>();
            ViewFactory.Register<AddEditMyFoodsPage, AddEditMyFoodsViewModel>();
            ViewFactory.Register<AddFriendPage, AddFriendViewModel>();
            ViewFactory.Register<RemoveFriendFromHomePage, RemoveFriendFromHomeViewModel>();
            ViewFactory.Register<RegisterHomePage, RegisterHomeViewModel>();
            ViewFactory.Register<InsertRoomPage, InsertRoomViewModel>();
            ViewFactory.Register<SelectCountryPage, SelectCountryViewModel>();
            ViewFactory.Register<HomeInformationPage, HomeInformationViewModel>();
            ViewFactory.Register<TasksPage, TasksViewModel>();
            ViewFactory.Register<SelectOptionsCleaningHousePage, SelectOptionsCleaningHouseViewModel>();
            ViewFactory.Register<SelectRoomsRegisterCleanedPage, SelectRoomsRegisterCleanedViewModel>();
            ViewFactory.Register<TaskDetailsPage, TaskDetailsViewModel>();
            ViewFactory.Register<CompleteHistoryPage, CompleteHistoryViewModel>();
            ViewFactory.Register<RateTaskPage, RateTaskViewModel>();
            ViewFactory.Register<DetailsAllRatePage, DetailsAllRateViewModel>();
            ViewFactory.Register<IlustrationsInformationsPage, IlustrationsInformationsViewModel>();
            ViewFactory.Register<ContactUsPage, ContactUsViewModel>();
            ViewFactory.Register<PrivacyPolicyPage, PrivacyPolicyViewModel>();
            ViewFactory.Register<ProjectInformationsPage, ProjectInformationViewModel>();
            ViewFactory.Register<TermsOfUsePage, TermsOfUseViewModel>();
            ViewFactory.Register<UserIsPartOfHomeFlyoutPageFlyout, UserIsPartOfHomeFlyoutViewModel>();
            ViewFactory.Register<UserWithoutPartOfHomePageFlyout, UserWithoutPartOfHomeFlyoutViewModel>();
        }
    }
}
