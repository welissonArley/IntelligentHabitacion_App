using Homuai.App.Views.Modal;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Templates.Date
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonthSelectorTemplate : ContentView
    {
        public DateTime Month
        {
            get => (DateTime)GetValue(MonthProperty);
            set => SetValue(MonthProperty, value);
        }
        public ICommand OnMonthSelected
        {
            get => (ICommand)GetValue(OnMonthSelectedProperty);
            set => SetValue(OnMonthSelectedProperty, value);
        }

        public static readonly BindableProperty MonthProperty = BindableProperty.Create(
                                                        propertyName: "Month",
                                                        returnType: typeof(DateTime),
                                                        declaringType: typeof(MonthSelectorTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: MonthChanged);

        public static readonly BindableProperty OnMonthSelectedProperty = BindableProperty.Create(
                                                        propertyName: "OnMonthSelected",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(MonthSelectorTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: null);

        private static void MonthChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                var date = (DateTime)newValue;
                var month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month);

                var component = (MonthSelectorTemplate)bindable;
                component.LabelMonth.Text = $"{month.First().ToString().ToUpper() + month.Substring(1)}, {date.Year}";
            }
        }

        public MonthSelectorTemplate()
        {
            InitializeComponent();
        }

        private void MonthSelector_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PushPopupAsync(new CalendarMonth(Month, new Command((date) =>
            {
                OnMonthSelected.Execute(date);
            })));
        }
    }
}