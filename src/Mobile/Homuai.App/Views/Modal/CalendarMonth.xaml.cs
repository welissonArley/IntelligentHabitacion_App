using Homuai.App.Views.Templates.Date;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Modal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarMonth : Rg.Plugins.Popup.Pages.PopupPage
    {
        private DateTime _date;
        private readonly ICommand _onSelectedDate;

        public CalendarMonth(DateTime date, ICommand onSelectedDate)
        {
            InitializeComponent();

            _onSelectedDate = onSelectedDate;

            _date = date;

            FillModal();
        }

        private void Button_Cancel(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }
        private void Button_PreviousYear(object sender, EventArgs e)
        {
            _date = _date.AddYears(-1);
            LabelYear.Text = $"{_date.Year}";
        }
        private void Button_NextYear(object sender, EventArgs e)
        {
            _date = _date.AddYears(1);
            LabelYear.Text = $"{_date.Year}";
        }
        private void Button_Ok(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
            _onSelectedDate.Execute(_date);
        }

        private void FillModal()
        {
            LabelYear.Text = $"{_date.Year}";

            var row = 0;
            var column = 0;

            while (MonthContent.Children.Count > 4)
                MonthContent.Children.RemoveAt(4);

            for (var month = 1; month <= 12; month++)
            {
                MonthContent.Children.Add(new ColumnMonthTemplate(month, MonthSelected, month == _date.Month), column++, row);

                if (column == 4)
                {
                    column = 0;
                    row++;
                }
            }
        }

        private void MonthSelected(int month)
        {
            var selectedDayemplate = (ColumnMonthTemplate)MonthContent.Children.ElementAt(_date.Month - 1);
            selectedDayemplate.DeselectMonth();
            _date = new DateTime(_date.Year, month, 1);
        }
    }
}