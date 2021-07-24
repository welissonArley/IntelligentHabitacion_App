using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Templates.Header
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CenterCircleWithTextComponent : ContentView
    {
        public string CircleColor
        {
            get => (string)GetValue(CircleColorProperty);
            set => SetValue(CircleColorProperty, value);
        }
        public string CircleText
        {
            get => (string)GetValue(CircleTextProperty);
            set => SetValue(CircleTextProperty, value);
        }

        public static readonly BindableProperty CircleColorProperty = BindableProperty.Create(
                                                        propertyName: "CircleColor",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(CenterCircleWithTextComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: CircleColorChanged);

        public static readonly BindableProperty CircleTextProperty = BindableProperty.Create(
                                                        propertyName: "CircleText",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(CenterCircleWithTextComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: CircleTextChanged);

        private static void CircleColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                var component = (CenterCircleWithTextComponent)bindable;
                component.CircleBoxView.Fill = new SolidColorBrush(Color.FromHex(newValue.ToString()));
            }
        }
        private static void CircleTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                var component = (CenterCircleWithTextComponent)bindable;
                component.LabelCircleText.Text = newValue.ToString();
            }
        }

        public CenterCircleWithTextComponent()
        {
            InitializeComponent();
        }
    }
}