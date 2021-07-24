using Homuai.App.Model;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Templates.Information
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CountryComponent : ContentView
    {
        public CountryModel Country
        {
            get => (CountryModel)GetValue(CountryProperty);
            set => SetValue(CountryProperty, value);
        }

        public static readonly BindableProperty CountryProperty = BindableProperty.Create(
                                                        propertyName: "Country",
                                                        returnType: typeof(CountryModel),
                                                        declaringType: typeof(CountryComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: CountryChanged);

        public ICommand TappedCardCommand
        {
            get => (ICommand)GetValue(TappedCardCommandProperty);
            set => SetValue(TappedCardCommandProperty, value);
        }

        public static readonly BindableProperty TappedCardCommandProperty = BindableProperty.Create(propertyName: "TappedCard",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(CountryComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

        private static void CountryChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                var countryModel = (CountryModel)newValue;
                var component = (CountryComponent)bindable;
                component.CountryPhoneCode.Text = countryModel.PhoneCode;
                component.CountryFlag.Source = ImageSource.FromUri(new Uri(countryModel.Flag));
                component.CountryName.Text = countryModel.Name;
            }
        }

        public CountryComponent()
        {
            InitializeComponent();
        }

        private void SelectCountry_Tapped(object sender, EventArgs e)
        {
            TappedCardCommand?.Execute(Country);
        }
    }
}