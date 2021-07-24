using Homuai.App.Model;
using Homuai.App.ValueObjects.Enum;
using Homuai.App.Views.Modal;
using Rg.Plugins.Popup.Extensions;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Templates.Information
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskCleaningScheduleAssignTemplate : ContentView
    {
        public ICommand OnSelectTaskDetails
        {
            get => (ICommand)GetValue(OnSelectTaskDetailsProperty);
            set => SetValue(OnSelectTaskDetailsProperty, value);
        }
        public ICommand OnConfirmRoomCleanedToday
        {
            get => (ICommand)GetValue(OnConfirmRoomCleanedTodayProperty);
            set => SetValue(OnConfirmRoomCleanedTodayProperty, value);
        }
        public ICommand OnEditAssigsToTask
        {
            get => (ICommand)GetValue(OnEditAssigsToTaskProperty);
            set => SetValue(OnEditAssigsToTaskProperty, value);
        }
        public TaskModel Task
        {
            get => (TaskModel)GetValue(TaskProperty);
            set => SetValue(TaskProperty, value);
        }
        public static readonly BindableProperty TaskProperty = BindableProperty.Create(
                                                        propertyName: "Task",
                                                        returnType: typeof(TaskModel),
                                                        declaringType: typeof(TaskCleaningScheduleAssignTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: TaskChanged);

        public static readonly BindableProperty OnConfirmRoomCleanedTodayProperty = BindableProperty.Create(
                                                        propertyName: "OnConfirmRoomCleanedToday",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(TaskCleaningScheduleAssignTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: null);

        public static readonly BindableProperty OnSelectTaskDetailsProperty = BindableProperty.Create(
                                                        propertyName: "OnSelectTaskDetails",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(TaskCleaningScheduleAssignTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: null);

        public static readonly BindableProperty OnEditAssigsToTaskProperty = BindableProperty.Create(
                                                        propertyName: "OnEditAssigsToTask",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(TaskCleaningScheduleAssignTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: null);

        private static void TaskChanged(BindableObject bindable, object oldValue, object newValue)
        {
            newValue = oldValue != null && newValue is null ? oldValue : newValue;
            if (newValue != null)
            {
                var taskModel = (TaskModel)newValue;
                var component = (TaskCleaningScheduleAssignTemplate)bindable;
                component.ContentAssign.Children.Clear();

                for (var index = taskModel.Assign.Count; index > 0; index--)
                {
                    var assign = taskModel.Assign.ElementAt(index - 1);
                    component.ContentAssign.Children.Insert(0, CreateEllipseAssign(assign.ProfileColor, assign.Name));
                }

                if (!taskModel.Assign.Any())
                    component.ContentAssign.Children.Insert(0, CreateNoAssignContent());

                component.Room.Text = taskModel.Room;
                component.OptionEdit.IsVisible = taskModel.CanEdit;
                component.ThereIsTaskToRateContent.IsVisible = taskModel.CanRate;
                component.CompletedTodayContent.IsVisible = taskModel.CanRegisterRoomCleanedToday;
                if (taskModel.CanRegisterRoomCleanedToday)
                {
                    component.CompletedTodayContent.CheckChangedCommand = new Command(async () =>
                    {
                        await component.RoomCleanedToday(taskModel.Room);
                    });
                }
            }
        }

        private static Grid CreateEllipseAssign(string color, string name)
        {
            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.End,
                RowDefinitions =
                {
                    new RowDefinition
                    {
                        Height = 36
                    }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition
                    {
                        Width = 36
                    }
                }
            };

            grid.Children.Add(new Ellipse
            {
                Fill = new SolidColorBrush(Color.FromHex(color)),
                HeightRequest = 36,
                WidthRequest = 36
            }, 0, 0);
            grid.Children.Add(new Label
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Text = new Services.ShortNameConverter().Converter(name),
                TextColor = Application.Current.RequestedTheme == OSAppTheme.Dark ? (Color)Application.Current.Resources["DarkModePrimaryColor"] : Color.White,
                FontSize = 14,
                Style = (Style)Application.Current.Resources["LabelBold"]
            }, 0, 0);

            return grid;
        }
        private static Label CreateNoAssignContent()
        {
            return new Label
            {
                Text = ResourceText.TITLE_NO_RESPONSIBLE,
                FontSize = 18,
                Style = (Style)Application.Current.Resources["LabelBold"],
                TextColor = (Color)Application.Current.Resources["GrayDefault"]
            };
        }

        public TaskCleaningScheduleAssignTemplate()
        {
            InitializeComponent();
        }

        public async Task RoomCleanedToday(string room)
        {
            ICommand callbackCancel = new Command(() => CompletedTodayContent.IsChecked = false);
            ICommand callbackConfirm = new Command(() => OnConfirmRoomCleanedToday.Execute(Task));

            await Navigation.PushPopupAsync(new ConfirmAction(string.Format(ResourceText.TITLE_ROOM_CLEANED, room), ResourceText.DESCRIPTION_ROOM_CLEANED, ModalConfirmActionType.Green, callbackConfirm, callbackCancel));
        }

        private void SelectTask_Tapped(object sender, System.EventArgs e)
        {
            OnSelectTaskDetails.Execute(Task);
        }

        private void EditAssign_Tapped(object sender, System.EventArgs e)
        {
            OnEditAssigsToTask.Execute(Task);
        }
    }
}