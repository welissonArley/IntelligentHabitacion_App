using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Templates.Header
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CircleWithTextAndDescriptionComponent : ContentView
    {
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
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

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
                                                        propertyName: "Title",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(CircleWithTextAndDescriptionComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: TitleChanged);

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
                                                        propertyName: "Text",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(CircleWithTextAndDescriptionComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: TextChanged);

        public static readonly BindableProperty CircleColorProperty = BindableProperty.Create(
                                                        propertyName: "CircleColor",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(CircleWithTextAndDescriptionComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: CircleColorChanged);

        public static readonly BindableProperty CircleTextProperty = BindableProperty.Create(
                                                        propertyName: "CircleText",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(CircleWithTextAndDescriptionComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: CircleTextChanged);

        private static void CircleColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                var component = (CircleWithTextAndDescriptionComponent)bindable;
                component.CircleBoxView.Fill = new SolidColorBrush(Color.FromHex(newValue.ToString()));
            }
        }
        private static void CircleTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                var component = (CircleWithTextAndDescriptionComponent)bindable;
                component.LabelCircleText.Text = newValue.ToString();
            }
        }
        private static void TextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                var component = (CircleWithTextAndDescriptionComponent)bindable;
                component.LabelText.Text = newValue.ToString();
            }
        }
        private static void TitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                var component = (CircleWithTextAndDescriptionComponent)bindable;
                component.LabelTitle.Text = newValue.ToString();
            }
        }

        public CircleWithTextAndDescriptionComponent()
        {
            InitializeComponent();
        }
    }
}