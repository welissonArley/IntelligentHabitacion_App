using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Templates.TextWithLabel
{
    public enum IconOption
    {
        Show = 0,
        Hide = 1
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InputPasswordWithLabelComponent : ContentView
    {
        private IconOption _iconOption { get; set; }

        public string PropertyToBindindEntry
        {
            get => (string)GetValue(PropertyToBindindEntryProperty);
            set => SetValue(PropertyToBindindEntryProperty, value);
        }

        public static readonly BindableProperty PropertyToBindindEntryProperty = BindableProperty.Create(
                                                        propertyName: "PropertyToBindindEntry",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(InputPasswordWithLabelComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: PropertyToBindindEntryChanged);

        private static void PropertyToBindindEntryChanged(BindableObject bindable, object oldValue, object newValue)
        {
            Binding binding = new Binding(newValue.ToString())
            {
                Mode = BindingMode.TwoWay
            };

            var bindableComponent = (InputPasswordWithLabelComponent)bindable;

            bindableComponent.Input.SetBinding(Entry.TextProperty, binding);
        }

        public string LabelTitle { get; set; }
        public string PlaceHolderText { set { Input.Placeholder = value; } get { return Input.Placeholder; } }

        public InputPasswordWithLabelComponent()
        {
            InitializeComponent();

            InputTextChanged();

            HidePassword();
        }

        private void InputTextChanged()
        {
            Input.TextChanged += (sender, e) =>
            {
                if (string.IsNullOrEmpty(Input.Text))
                    Label.Text = " ";
                else
                    Label.Text = LabelTitle;
            };
        }

        private void ShowPassword()
        {
            _iconOption = IconOption.Show;
            Input.IsPassword = false;
            IlustrationShowHidePassword.Source = "IconEye.png";
            IlustrationShowHidePassword.HeightRequest = 14;
            IlustrationShowHidePassword.Margin = new Thickness(0, 15, 0, 0);
        }
        private void HidePassword()
        {
            _iconOption = IconOption.Hide;
            Input.IsPassword = true;
            IlustrationShowHidePassword.Source = "IconEyeHidden.png";
            IlustrationShowHidePassword.HeightRequest = 18;
            IlustrationShowHidePassword.Margin = new Thickness(0, 13, 0, 0);
        }

        private void ShowHiddenPassword_Tapped(object sender, System.EventArgs e)
        {
            if (_iconOption == IconOption.Show)
                HidePassword();
            else
                ShowPassword();
        }
    }
}