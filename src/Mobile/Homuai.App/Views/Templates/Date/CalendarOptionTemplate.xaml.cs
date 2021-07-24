using Homuai.App.Views.Modal;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Templates.Date
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarOptionTemplate : ContentView
    {
        public DateTime Date
        {
            get => (DateTime)GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }
        public static readonly BindableProperty DateProperty = BindableProperty.Create(
                                                        propertyName: "Date",
                                                        returnType: typeof(DateTime),
                                                        declaringType: typeof(CalendarOptionTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: DateChanged);

        public ICommand OnDateSelectedCommand
        {
            get => (ICommand)GetValue(OnDateSelectedCommandProperty);
            set => SetValue(OnDateSelectedCommandProperty, value);
        }
        public static readonly BindableProperty OnDateSelectedCommandProperty = BindableProperty.Create(propertyName: "OnDateSelected",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(CalendarOptionTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

        private static void DateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            newValue = oldValue != null && newValue == null ? oldValue : newValue;

            if (newValue != null)
            {
                var date = (DateTime)newValue;
                var component = (CalendarOptionTemplate)bindable;

                var dateString = date.ToString("MMMM yyyy");
                component.LabelDate.Text = $"{dateString.First().ToString().ToUpper()}{dateString.Substring(1)}";
            }
        }

        public CalendarOptionTemplate()
        {
            InitializeComponent();
        }

        private void ChangeDate_Tapped(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new CalendarMonth(Date, new Command((date) =>
            {
                OnDateSelected((DateTime)date);
            })));
        }

        private void OnDateSelected(DateTime date)
        {
            Date = date;
            OnDateSelectedCommand.Execute(Date);
        }
    }
}