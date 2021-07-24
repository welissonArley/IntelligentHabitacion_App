using Homuai.App.Views.Templates.Date;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Modal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Calendar : Rg.Plugins.Popup.Pages.PopupPage
    {
        private DateTime _date;
        private DateTime? _minimumDate;
        private DateTime? _maximumDate;
        private readonly Func<DateTime, Task> _callbackSelectedDay;

        public Calendar(DateTime startDay, Func<DateTime, Task> callbackSelectedDay, DateTime? minimumDate = null, DateTime? maximumDate = null)
        {
            InitializeComponent();

            if (minimumDate.HasValue && maximumDate.HasValue && minimumDate.Value.Date > maximumDate.Value.Date)
                _minimumDate = _maximumDate = null;
            else
            {
                _minimumDate = minimumDate;
                _maximumDate = maximumDate;
            }

            if (_minimumDate.HasValue && startDay.Date < _minimumDate.Value.Date)
                _date = minimumDate.Value.Date;
            else if (_maximumDate.HasValue && startDay.Date > _maximumDate.Value.Date)
                _date = maximumDate.Value.Date;
            else
                _date = startDay;

            _callbackSelectedDay = callbackSelectedDay;
            FillModal();
        }

        private void Button_Cancel(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }
        private void Button_NextMonth(object sender, EventArgs e)
        {
            _date = _date.AddMonths(1);
            FillModal();
        }
        private void Button_PreviousMonth(object sender, EventArgs e)
        {
            _date = _date.AddMonths(-1);
            FillModal();
        }
        private void Button_PreviousYear(object sender, EventArgs e)
        {
            _date = _date.AddYears(-1);
            FillModal();

        }
        private void Button_NextYear(object sender, EventArgs e)
        {
            _date = _date.AddYears(1);
            FillModal();
        }
        private void Button_Ok(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
            _callbackSelectedDay(_date);
        }

        private void FillModal()
        {
            ValidateMinimumAndMaximumDate();

            LabelYear.Text = $"{_date.Year}";
            LabelMonth.Text = $"{_date.ToString("MMMM").Substring(0, 1).ToUpper()}{_date.ToString("MMMM").Substring(1)}";

            var row = 1;
            var column = (int)new DateTime(_date.Year, _date.Month, 1).DayOfWeek;

            while (DaysContent.Children.Count > 7)
                DaysContent.Children.RemoveAt(7);

            for (var day = 1; day <= DateTime.DaysInMonth(_date.Year, _date.Month); day++)
            {
                var dayUnavailable = false;
                if (_minimumDate.HasValue)
                {
                    var minimumDate = _minimumDate.Value;
                    dayUnavailable = minimumDate.Year == _date.Year && _date.Month == minimumDate.Month && day < minimumDate.Day;
                }

                if (!dayUnavailable && _maximumDate.HasValue)
                {
                    var maximumDate = _maximumDate.Value;
                    dayUnavailable = maximumDate.Year == _date.Year && _date.Month == maximumDate.Month && day > maximumDate.Day;
                }

                DaysContent.Children.Add(new ColumnDayTemplate(day, DaySelected, dayUnavailable, day == _date.Day), column++, row);

                if (column == 7)
                {
                    column = 0;
                    row++;
                }
            }
        }

        private void DaySelected(int day)
        {
            if (day != _date.Day)
            {
                var selectedDayemplate = (ColumnDayTemplate)DaysContent.Children.ElementAt(6 + _date.Day);
                selectedDayemplate.DeselectDay();
                _date = new DateTime(_date.Year, _date.Month, day);
            }
        }

        private void ValidateMinimumAndMaximumDate()
        {
            ValidadeMinimumDate();
            ValidadeMaximumDate();
        }

        private void ValidadeMinimumDate()
        {
            if (_minimumDate.HasValue)
            {
                var minimumDate = _minimumDate.Value;
                if (_date.Year <= minimumDate.Year)
                {
                    ButtonPreviousYear.Opacity = 0;
                    ButtonPreviousYear.IsEnabled = false;

                    if (_date.Month <= minimumDate.Month)
                    {
                        ButtonPreviousMonth.Opacity = 0;
                        ButtonPreviousMonth.IsEnabled = false;
                        if (_date.Month < minimumDate.Month)
                            _date = new DateTime(_date.Year, minimumDate.Month, minimumDate.Day);
                    }
                    else
                    {
                        ButtonPreviousMonth.Opacity = 1;
                        ButtonPreviousMonth.IsEnabled = true;
                    }
                }
                else
                {
                    ButtonPreviousYear.Opacity = 1;
                    ButtonPreviousYear.IsEnabled = true;
                    ButtonPreviousMonth.Opacity = 1;
                    ButtonPreviousMonth.IsEnabled = true;
                }
            }
        }
        private void ValidadeMaximumDate()
        {
            if (_maximumDate.HasValue)
            {
                var maximumDate = _maximumDate.Value;
                if (_date.Year >= maximumDate.Year)
                {
                    ButtonNextYear.Opacity = 0;
                    ButtonNextYear.IsEnabled = false;

                    if (_date.Month >= maximumDate.Month)
                    {
                        ButtonNextMonth.Opacity = 0;
                        ButtonNextMonth.IsEnabled = false;
                        if (_date.Month > maximumDate.Month)
                            _date = new DateTime(_date.Year, maximumDate.Month, maximumDate.Day);
                    }
                    else
                    {
                        ButtonNextMonth.Opacity = 1;
                        ButtonNextMonth.IsEnabled = true;
                    }
                }
                else
                {
                    ButtonNextYear.Opacity = 1;
                    ButtonNextYear.IsEnabled = true;
                    ButtonNextMonth.Opacity = 1;
                    ButtonNextMonth.IsEnabled = true;
                }
            }
        }
    }
}