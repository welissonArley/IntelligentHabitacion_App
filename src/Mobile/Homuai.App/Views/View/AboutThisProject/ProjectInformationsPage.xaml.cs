using Homuai.App.Services.Interface;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.View.AboutThisProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectInformationsPage : ContentPage
    {
        public ProjectInformationsPage()
        {
            InitializeComponent();

            LabelVersion.Text = $"{ResourceText.TITLE_VERSION} {DependencyService.Get<IAppVersion>().GetVersionNumber()}";
        }
    }
}