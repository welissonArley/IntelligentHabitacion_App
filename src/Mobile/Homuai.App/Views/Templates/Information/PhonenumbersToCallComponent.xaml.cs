using FFImageLoading.Svg.Forms;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Templates.Information
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhonenumbersToCallComponent : ContentView
    {
        public string ProfileColor
        {
            get => (string)GetValue(ProfileColorProperty);
            set => SetValue(ProfileColorProperty, value);
        }

        public ICommand TappedMakePhonecallCommand
        {
            get => (ICommand)GetValue(TappedMakePhonecallCommandProperty);
            set => SetValue(TappedMakePhonecallCommandProperty, value);
        }

        public IList<string> PhoneNumbers
        {
            get => (IList<string>)GetValue(PhoneNumbersProperty);
            set => SetValue(PhoneNumbersProperty, value);
        }

        public static readonly BindableProperty PhoneNumbersProperty = BindableProperty.Create(
                                                        propertyName: "PhoneNumbers",
                                                        returnType: typeof(IList<string>),
                                                        declaringType: typeof(PhonenumbersToCallComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: PhoneNumbersChanged);

        public static readonly BindableProperty TappedMakePhonecallCommandProperty = BindableProperty.Create(propertyName: "TappedMakePhonecall",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(PhonenumbersToCallComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

        public static readonly BindableProperty ProfileColorProperty = BindableProperty.Create(propertyName: "ProfileColor",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(PhonenumbersToCallComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

        private static void PhoneNumbersChanged(BindableObject bindable, object oldValue, object newValue)
        {
            newValue = oldValue != null && newValue is null ? oldValue : newValue;
            if (newValue != null)
            {
                var phoneNumbers = (IList<string>)newValue;
                var component = (PhonenumbersToCallComponent)bindable;
                component.Content.Children.Clear();

                foreach (var phoneNumber in phoneNumbers)
                    component.InsertLayout(component.Content, phoneNumber);
            }
        }

        public void InsertLayout(StackLayout stackLayout, string phoneNumer)
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (obj, eventArgs) =>
            {
                TappedMakePhonecallCommand?.Execute(phoneNumer);
            };

            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.End,
                RowDefinitions =
                {
                    new RowDefinition
                    {
                        Height = 30
                    }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition
                    {
                        Width = 30
                    }
                }
            };
            grid.GestureRecognizers.Add(tapGestureRecognizer);

            grid.Children.Add(new Ellipse
            {
                Fill = new SolidColorBrush(Color.FromHex(string.IsNullOrEmpty(ProfileColor) ? "#000000" : ProfileColor)),
                HeightRequest = 30,
                WidthRequest = 30
            }, 0, 0);
            grid.Children.Add(new Image
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HeightRequest = 16,
                WidthRequest = 35,
                Source = ImageSource.FromFile(Application.Current.RequestedTheme == OSAppTheme.Dark ? "IconPhoneDark" : "IconPhoneLight"),
            }, 0, 0);

            stackLayout.Children.Add(new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
                Margin = new Thickness(0, 20, 0, 0),
                Children =
                {
                    new Label
                    {
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        FontSize = 14,
                        Text = phoneNumer,
                        Style = (Style)Application.Current.Resources["LabelMedium"]
                    },
                    grid
                }
            });
        }

        public PhonenumbersToCallComponent()
        {
            InitializeComponent();
        }
    }
}