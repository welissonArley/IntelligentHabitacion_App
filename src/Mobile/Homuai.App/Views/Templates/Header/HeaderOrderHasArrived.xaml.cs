using Homuai.App.Services;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Templates.Header
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeaderOrderHasArrived : ContentView
    {
        public ICommand ButtonClickedCommand
        {
            get => (ICommand)GetValue(ButtonClickedCommandProperty);
            set => SetValue(ButtonClickedCommandProperty, value);
        }

        public static readonly BindableProperty ButtonClickedCommandProperty = BindableProperty.Create(propertyName: "ButtonClicked",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(HeaderOrderHasArrived),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

        public HeaderOrderHasArrived()
        {
            InitializeComponent();

            var deviceWidth = HomuaiDevice.Width();

            ImageOrderHasArrived.WidthRequest = deviceWidth * 0.42;
            ImageOrderHasArrived.HeightRequest = ImageOrderHasArrived.WidthRequest * 1.45;
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            ButtonClickedCommand?.Execute(null);
        }
    }
}