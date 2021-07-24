using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Templates.Date
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ColumnMonthTemplate : ContentView
    {
        private int _month;
        private readonly Action<int> _callbackMonthSelected;

        public ColumnMonthTemplate(int month, Action<int> callbackMonthSelected, bool selected = false)
        {
            InitializeComponent();

            _callbackMonthSelected = callbackMonthSelected;
            _month = month;

            var monthString = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month);

            LabelMonth.Text = $"{monthString.ToCharArray().First().ToString().ToUpper()}{monthString.Substring(1)}";

            if (selected)
                MonthSelected();
        }

        private void SelectedDay(object sender, EventArgs e)
        {
            MonthSelected();
            _callbackMonthSelected(_month);
        }

        public void DeselectMonth()
        {
            SelectComponent.IsVisible = false;
            LabelMonth.TextColor = Application.Current.RequestedTheme == OSAppTheme.Dark ? Color.White : Color.Black;
        }

        private void MonthSelected()
        {
            SelectComponent.IsVisible = true;
            LabelMonth.TextColor = Color.White;
        }
    }
}