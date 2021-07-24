using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Templates.CardIlustrationOptions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardIlustrationOptions : ContentView
    {
        public ImageSource Ilustration
        {
            set
            {
                ImageIlustration.Source = value;
            }
            get { return ImageIlustration.Source; }
        }

        public string TitleCard
        {
            set
            {
                LabelTitle.Text = value;
            }
            get { return LabelTitle.Text; }
        }

        public string DescriptionCard
        {
            set
            {
                LabelDescriptionCard.Text = value;
            }
            get { return LabelDescriptionCard.Text; }
        }

        public static readonly BindableProperty TappedCardCommandProperty = BindableProperty.Create(propertyName: "TappedCard",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(CardIlustrationOptions),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

        public ICommand TappedCardCommand
        {
            get => (ICommand)GetValue(TappedCardCommandProperty);
            set => SetValue(TappedCardCommandProperty, value);
        }

        public void Card_OnTapped(object sender, System.EventArgs e)
        {
            TappedCardCommand?.Execute(null);
        }

        public CardIlustrationOptions()
        {
            InitializeComponent();
        }
    }
}