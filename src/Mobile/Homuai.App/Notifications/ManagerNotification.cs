using Com.OneSignal.Abstractions;
using Homuai.App.Services;
using Homuai.App.ValueObjects.Enum;
using Homuai.App.Views.View.Dashboard.NotPartOfHome;
using Homuai.App.Views.View.Dashboard.PartOfHome;
using Rg.Plugins.Popup.Extensions;
using System.Linq;
using Xamarin.Forms;
using XLabs.Ioc;

namespace Homuai.App.Notifications
{
    public class ManagerNotification
    {
        public static void Notification(OSNotification notification)
        {
            var userPreferences = Resolver.Resolve<UserPreferences>();
            var key = notification.payload.additionalData.Keys.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(key))
                return;

            switch (key)
            {
                case EnumNotifications.OrderReceived:
                    {
                        userPreferences.UserHasOrder(true);
                        RefreshHeader();
                    }
                    break;
                case EnumNotifications.NewAdmin:
                    {
                        userPreferences.UserIsAdministrator(true);
                        RefreshHeader();
                    }
                    break;
                case EnumNotifications.RemovedFromHome:
                case EnumNotifications.HomeDeleted:
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            userPreferences.UserIsPartOfOneHome(false);
                            var navigation = Resolver.Resolve<INavigation>();
                            var page = navigation.NavigationStack.FirstOrDefault();
                            if (page is UserIsPartOfHomeFlyoutPage)
                            {
                                try { await navigation.PopAllPopupAsync(); } catch { /* If one exception is throwed its beacause dont have any popup */ }
                                await navigation.PopToRootAsync();
                                Application.Current.MainPage = new NavigationPage(new UserWithoutPartOfHomePage());
                            }
                        });
                    }
                    break;
            }
        }

        private static void RefreshHeader()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var navigation = Resolver.Resolve<INavigation>();
                var page = navigation.NavigationStack.FirstOrDefault();
                if (page is UserIsPartOfHomeFlyoutPage refreshPage)
                {
                    var pageDetail = ((FlyoutPage)page).Detail;
                    var navigationPage = (NavigationPage)pageDetail;
                    ((UserIsPartOfHomeFlyoutPageDetail)navigationPage.CurrentPage).RefreshHeader();
                }
            });
        }
    }
}
