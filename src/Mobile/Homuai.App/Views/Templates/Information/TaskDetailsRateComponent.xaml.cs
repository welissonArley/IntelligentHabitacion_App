using Homuai.App.Model;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Templates.Information
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskDetailsRateComponent : ContentView
    {
        public ICommand TappedDetailsRateTaskCommand
        {
            get => (ICommand)GetValue(TappedDetailsRateTaskCommandProperty);
            set => SetValue(TappedDetailsRateTaskCommandProperty, value);
        }
        public ICommand TappedRateTaskCommand
        {
            get => (ICommand)GetValue(TappedRateTaskCommandProperty);
            set => SetValue(TappedRateTaskCommandProperty, value);
        }

        public DetailsTaskCleanedOnDayModel TaskDetails
        {
            get => (DetailsTaskCleanedOnDayModel)GetValue(TaskDetailsProperty);
            set => SetValue(TaskDetailsProperty, value);
        }

        public static readonly BindableProperty TaskDetailsProperty = BindableProperty.Create(
                                                        propertyName: "TaskDetails",
                                                        returnType: typeof(DetailsTaskCleanedOnDayModel),
                                                        declaringType: typeof(TaskDetailsRateComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: TaskDetailsChanged);

        public static readonly BindableProperty TappedRateTaskCommandProperty = BindableProperty.Create(propertyName: "TappedRateTask",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(TaskDetailsRateComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

        public static readonly BindableProperty TappedDetailsRateTaskCommandProperty = BindableProperty.Create(propertyName: "TappedDetailsRateTask",
                                                        returnType: typeof(ICommand),
                                                        declaringType: typeof(TaskDetailsRateComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

        private static void TaskDetailsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            newValue = oldValue != null && newValue is null ? oldValue : newValue;
            if (newValue != null)
            {
                var model = (DetailsTaskCleanedOnDayModel)newValue;
                var component = (TaskDetailsRateComponent)bindable;

                component.UserLabel.Text = model.User;
                if (model.CanRate)
                {
                    component.SeeDetailsButton.IsVisible = false;
                    component.ContentShowRateDetails.IsVisible = false;
                    component.ButtonToRate.IsVisible = true;
                }
                else
                {
                    component.SeeDetailsButton.IsVisible = true;
                    component.ContentShowRateDetails.IsVisible = true;
                    component.ButtonToRate.IsVisible = false;

                    if (model.AverageRate < 0)
                    {
                        component.LabelWithoutRate.IsVisible = true;
                        component.RatingStars.IsVisible = false;
                        component.SeeDetailsButton.IsVisible = false;
                    }
                    else
                    {
                        component.LabelWithoutRate.IsVisible = false;
                        component.RatingStars.IsVisible = true;
                        component.RatingStars.Rating = model.AverageRate;
                    }
                }
            }
        }

        public TaskDetailsRateComponent()
        {
            InitializeComponent();
        }

        private void ButtonRateTask_Clicked(object sender, System.EventArgs e)
        {
            TappedRateTaskCommand.Execute(TaskDetails);
        }

        private void ButtonSeeDetails_Clicked(object sender, System.EventArgs e)
        {
            TappedDetailsRateTaskCommand.Execute(TaskDetails);
        }
    }
}