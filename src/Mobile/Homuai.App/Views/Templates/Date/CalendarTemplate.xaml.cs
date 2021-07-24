using Homuai.App.Model;
using Homuai.App.Views.Modal;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Templates.Date
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarTemplate : ContentView
    {
        public ICommand OnDaySelectedCommand
        {
            get => (ICommand)GetValue(OnDaySelectedCommandProperty);
            set => SetValue(OnDaySelectedCommandProperty, value);
        }
        public ICommand OnChangeDateCommand
        {
            get => (ICommand)GetValue(OnChangeDateCommandProperty);
            set => SetValue(OnChangeDateCommandProperty, value);
        }
        public CleaningScheduleCalendarModel Model
        {
            get => (CleaningScheduleCalendarModel)GetValue(ModelProperty);
            set => SetValue(ModelProperty, value);
        }
        private StackLayout _stackLayoutSelectedDayActually { get; set; }
        private int _selectedDayActually { get; set; }
        private bool _selectedDayHasRateAvaliable { get; set; }
        private bool _selectedDayHasAttention { get; set; }

        public static readonly BindableProperty ModelProperty = BindableProperty.Create(
                                                        propertyName: "Model",
                                                        returnType: typeof(CleaningScheduleCalendarModel),
                                                        declaringType: typeof(CalendarTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: ModelChanged);

        public static readonly BindableProperty OnChangeDateCommandProperty = BindableProperty.Create(propertyName: "OnChangeDate",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(CalendarTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

        public static readonly BindableProperty OnDaySelectedCommandProperty = BindableProperty.Create(propertyName: "OnDaySelected",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(CalendarTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

        private static void ModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                var model = (CleaningScheduleCalendarModel)newValue;
                var component = ((CalendarTemplate)bindable);
                FillCalendar(component, model);
            }
        }

        public CalendarTemplate()
        {
            InitializeComponent();
        }

        private static void FillCalendar(CalendarTemplate component, CleaningScheduleCalendarModel model)
        {
            component.LabelMonth.Text = $"{model.Date.ToString("MMMM").Substring(0, 1).ToUpper()}{model.Date.ToString("MMMM").Substring(1)}, {model.Date.Year}";

            var row = 2;
            var dateReceived = new DateTime(model.Date.Year, model.Date.Month, 1);
            var column = (int)dateReceived.DayOfWeek;

            while (component.DaysContent.Children.Count > 8)
                component.DaysContent.Children.RemoveAt(8);

            var datePreviousNextMonth = dateReceived.AddMonths(-1);
            var numberOfDays = DateTime.DaysInMonth(datePreviousNextMonth.Year, datePreviousNextMonth.Month);
            datePreviousNextMonth = new DateTime(datePreviousNextMonth.Year, datePreviousNextMonth.Month, numberOfDays);

            for (var dayPreviousMonth = column - 1; dayPreviousMonth >= 0; dayPreviousMonth--)
            {
                var stackLayout = ColumnDayTemplatePreviousNextMonth(component, datePreviousNextMonth, datePreviousNextMonth.Day);
                component.DaysContent.Children.Add(stackLayout, dayPreviousMonth, row);
                datePreviousNextMonth = datePreviousNextMonth.AddDays(-1);
            }

            for (var day = 1; day <= DateTime.DaysInMonth(model.Date.Year, model.Date.Month); day++)
            {
                var dayDetails = model.CleanedDays.FirstOrDefault(c => c.Day == day);
                var rateAvaliable = dayDetails != null && dayDetails.AmountcleanedRecordsToRate > 0;
                var showAttention = dayDetails != null && dayDetails.AmountCleanedRecords > 0;

                component.DaysContent.Children.Add(ColumnDayTemplate(component, model.Date, day, day == model.Date.Day, rateAvaliable, showAttention),
                    column++, row);

                if (column == 7)
                {
                    column = 0;
                    row++;
                }
            }

            datePreviousNextMonth = dateReceived.AddMonths(1);
            while (row <= 7)
            {
                for (var columnToFill = column; columnToFill < 7; columnToFill++)
                {
                    var stackLayout = ColumnDayTemplatePreviousNextMonth(component, datePreviousNextMonth, datePreviousNextMonth.Day);
                    component.DaysContent.Children.Add(stackLayout, columnToFill, row);
                    datePreviousNextMonth = datePreviousNextMonth.AddDays(1);
                }
                row++;
                column = 0;
            }
        }

        private static StackLayout ColumnDayTemplatePreviousNextMonth(CalendarTemplate component, DateTime date, int day)
        {
            var dayLabel = DayLabel(day);
            dayLabel.Style = (Style)Application.Current.Resources["LabelThin"];
            dayLabel.TextColor = (Color)Application.Current.Resources["GrayDefault"];

            var stackLayout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    dayLabel
                }
            };
            stackLayout.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    component.OnChangeDateCommand.Execute(new DateTime(date.Year, date.Month, day));
                })
            });

            return stackLayout;
        }
        private static StackLayout ColumnDayTemplate(CalendarTemplate component, DateTime date, int day, bool selected, bool rateAvaliable, bool showAttentionIcon)
        {
            var stackLayout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    DayLabel(day)
                }
            };
            stackLayout.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    component._stackLayoutSelectedDayActually.Children.Clear();
                    component._stackLayoutSelectedDayActually.Children.Add(DayLabel(component._selectedDayActually));
                    if (component._selectedDayHasAttention)
                        component._stackLayoutSelectedDayActually.Children.Add(ShowAttention(component._selectedDayHasRateAvaliable));

                    stackLayout.Children.Clear();
                    stackLayout.Children.Add(Selected(day, rateAvaliable, showAttentionIcon));

                    component._stackLayoutSelectedDayActually = stackLayout;
                    component._selectedDayActually = day;
                    component._selectedDayHasRateAvaliable = rateAvaliable;
                    component._selectedDayHasAttention = showAttentionIcon;

                    component.OnDaySelectedCommand.Execute(new DateTime(date.Year, date.Month, day));
                })
            });

            if (selected)
            {
                stackLayout.Children.Clear();
                stackLayout.Children.Add(Selected(day, rateAvaliable, showAttentionIcon));
                component._stackLayoutSelectedDayActually = stackLayout;
                component._selectedDayActually = day;
                component._selectedDayHasRateAvaliable = rateAvaliable;
                component._selectedDayHasAttention = showAttentionIcon;
            }
            else if (showAttentionIcon)
                stackLayout.Children.Add(ShowAttention(rateAvaliable));

            return stackLayout;
        }

        private static Ellipse ShowAttention(bool rateAvaliable)
        {
            return new Ellipse
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HeightRequest = 10,
                WidthRequest = 10,
                Fill = new SolidColorBrush((Color)Application.Current.Resources[rateAvaliable ? "YellowDefault" : "GreenDefault"])
            };
        }
        private static Grid Selected(int day, bool rateAvaliable, bool hasAttention)
        {
            Grid grid = new Grid
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                RowDefinitions =
                {
                    new RowDefinition { Height = 30 }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = 30 }
                }
            };
            grid.Children.Add(new Ellipse
            {
                HeightRequest = 31,
                WidthRequest = 31,
                Stroke = hasAttention ? new SolidColorBrush((Color)Application.Current.Resources[rateAvaliable ? "YellowDefault" : "GreenDefault"]) : new SolidColorBrush(Application.Current.RequestedTheme == OSAppTheme.Dark ? Color.White : Color.Black),
                StrokeThickness = 2
            }, 0, 0);
            grid.Children.Add(new Label
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Text = $"{(day < 10 ? "0" : "")}{day}",
                FontSize = 16,
                Style = (Style)Application.Current.Resources["LabelBold"],
                TextColor = hasAttention ? (Color)Application.Current.Resources[rateAvaliable ? "YellowDefault" : "GreenDefault"] : Application.Current.RequestedTheme == OSAppTheme.Dark ? Color.White : Color.Black
            }, 0, 0);

            return grid;
        }
        private static Label DayLabel(int day)
        {
            return new Label
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Text = $"{(day < 10 ? "0" : "")}{day}",
                FontSize = 16,
                Style = (Style)Application.Current.Resources["LabelBold"]
            };
        }

        private void PreviousMonth_Clicked(object sender, EventArgs e)
        {
            var newDate = Model.Date.AddMonths(-1);
            var day = _selectedDayActually;
            if (_selectedDayActually > DateTime.DaysInMonth(newDate.Year, newDate.Month))
                day = DateTime.DaysInMonth(newDate.Year, newDate.Month);

            OnChangeDateCommand.Execute(new DateTime(newDate.Year, newDate.Month, day));
        }
        private void NextMonth_Clicked(object sender, EventArgs e)
        {
            var newDate = Model.Date.AddMonths(1);
            var day = _selectedDayActually;
            if (_selectedDayActually > DateTime.DaysInMonth(newDate.Year, newDate.Month))
                day = DateTime.DaysInMonth(newDate.Year, newDate.Month);

            OnChangeDateCommand.Execute(new DateTime(newDate.Year, newDate.Month, day));
        }
        private async void ChangeMonthYear_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new CalendarMonth(Model.Date, new Command((date) =>
            {
                var newDate = (DateTime)date;
                var day = _selectedDayActually;
                if (_selectedDayActually > DateTime.DaysInMonth(newDate.Year, newDate.Month))
                    day = DateTime.DaysInMonth(newDate.Year, newDate.Month);

                OnChangeDateCommand.Execute(new DateTime(newDate.Year, newDate.Month, day));
            })));
        }
    }
}