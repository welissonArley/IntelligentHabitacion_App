using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Templates.Date
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ColumnDayTemplate : ContentView
    {
        private readonly Action<int> _callbackDaySelected;

        public ColumnDayTemplate(int day, Action<int> callbackDaySelected, bool dayUnavailable, bool selected = false)
        {
            InitializeComponent();

            _callbackDaySelected = callbackDaySelected;
            LabelDay.Text = $"{day}";

            if (dayUnavailable)
                DayUnavailable();
            else if (selected)
                DaySelected();
        }

        private void DayUnavailable()
        {
            DayContent.IsEnabled = false;
            DayContent.Opacity = 0.1;
        }

        private void SelectedDay(object sender, EventArgs e)
        {
            DaySelected();
            _callbackDaySelected(int.Parse(LabelDay.Text));
        }

        public void DeselectDay()
        {
            SelectComponent.IsVisible = false;
            LabelDay.TextColor = Application.Current.RequestedTheme == OSAppTheme.Light ? Color.Black : Color.White;
        }

        private void DaySelected()
        {
            SelectComponent.IsVisible = true;
            LabelDay.TextColor = Color.White;
        }
    }
}