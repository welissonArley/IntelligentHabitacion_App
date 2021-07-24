using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Templates.Loading
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingTemplate : ContentView
    {
        public string Text { set { LabelText.Text = value; } get { return LabelText.Text; } }

        public LoadingTemplate()
        {
            InitializeComponent();
        }
    }
}