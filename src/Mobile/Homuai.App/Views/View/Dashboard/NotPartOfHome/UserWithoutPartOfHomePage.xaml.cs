using Homuai.App.ViewModel.Dashboard.NotPartOfHome;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Forms.Mvvm;

namespace Homuai.App.Views.View.Dashboard.NotPartOfHome
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserWithoutPartOfHomePage : FlyoutPage
    {
        public UserWithoutPartOfHomePage()
        {
            InitializeComponent();
            
            Flyout = (Page)ViewFactory.CreatePage<UserWithoutPartOfHomeFlyoutViewModel, UserWithoutPartOfHomeFlyoutViewModel>();

            Detail.BindingContext = new UserWithoutPartOfHomeDetailViewModel(Navigation);
        }
    }
}