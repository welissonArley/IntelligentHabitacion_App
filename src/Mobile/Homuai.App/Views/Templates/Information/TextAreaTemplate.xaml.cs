using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Templates.Information
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextAreaTemplate : ContentView
    {
        public string PlaceHolderText { set => Input.Placeholder = value; }
        public string PropertyToBindindEntry
        {
            get => (string)GetValue(PropertyToBindindEntryProperty);
            set => SetValue(PropertyToBindindEntryProperty, value);
        }
        public int MaximumCaracteres
        {
            get => (int)GetValue(MaximumCaracteresProperty);
            set => SetValue(MaximumCaracteresProperty, value);
        }

        public static readonly BindableProperty PropertyToBindindEntryProperty = BindableProperty.Create(
                                                        propertyName: "PropertyToBindindEntry",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(TextAreaTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: PropertyToBindindEntryChanged);

        public static readonly BindableProperty MaximumCaracteresProperty = BindableProperty.Create(
                                                        propertyName: "MaximumCaracteres",
                                                        returnType: typeof(int),
                                                        declaringType: typeof(TextAreaTemplate),
                                                        defaultValue: 255,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: PropertyMaximumCaracteresChanged);

        private static void PropertyToBindindEntryChanged(BindableObject bindable, object oldValue, object newValue)
        {
            Binding binding = new Binding(newValue.ToString())
            {
                Mode = BindingMode.TwoWay
            };

            var bindableComponent = (TextAreaTemplate)bindable;

            bindableComponent.Input.SetBinding(Entry.TextProperty, binding);
        }

        private static void PropertyMaximumCaracteresChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var bindableComponent = (TextAreaTemplate)bindable;
            var maximum = (int)newValue;

            if (maximum <= 0)
            {
                bindableComponent.MaximumCaracteres = (int)oldValue;
                return;
            }

            bindableComponent.LabelCount.Text = $"0/{maximum}";
        }

        public TextAreaTemplate()
        {
            InitializeComponent();

            InputTextChanged();
        }

        private void InputTextChanged()
        {
            Input.TextChanged += (sender, e) =>
            {
                if (string.IsNullOrEmpty(Input.Text))
                    LabelCount.Text = $"0/{MaximumCaracteres}";
                else
                {
                    if (Input.Text.Length > MaximumCaracteres)
                        Input.Text = Input.Text.Substring(0, MaximumCaracteres);

                    LabelCount.Text = $"{Input.Text.Length}/{MaximumCaracteres}";
                }
            };
        }
    }
}