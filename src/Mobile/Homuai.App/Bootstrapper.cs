using Homuai.App.Services;
using Homuai.App.UseCases.CleaningSchedule.Calendar;
using Homuai.App.UseCases.CleaningSchedule.CreateFirstSchedule;
using Homuai.App.UseCases.CleaningSchedule.DetailsAllRate;
using Homuai.App.UseCases.CleaningSchedule.EditTaskAssign;
using Homuai.App.UseCases.CleaningSchedule.GetTasks;
using Homuai.App.UseCases.CleaningSchedule.HistoryOfTheDay;
using Homuai.App.UseCases.CleaningSchedule.RateTask;
using Homuai.App.UseCases.CleaningSchedule.RegisterRoomCleaned;
using Homuai.App.UseCases.CleaningSchedule.Reminder;
using Homuai.App.UseCases.ContactUs;
using Homuai.App.UseCases.Friends.ChangeDateFriendJoinHome;
using Homuai.App.UseCases.Friends.GetMyFriends;
using Homuai.App.UseCases.Friends.NotifyOrderReceived;
using Homuai.App.UseCases.Friends.RemoveFriend;
using Homuai.App.UseCases.Home.HomeInformations;
using Homuai.App.UseCases.Home.RegisterHome;
using Homuai.App.UseCases.Home.RegisterHome.Brazil;
using Homuai.App.UseCases.Home.UpdateHomeInformations;
using Homuai.App.UseCases.Login.DoLogin;
using Homuai.App.UseCases.Login.ForgotPassword;
using Homuai.App.UseCases.MyFoods.ChangeQuantityOfOneProduct;
using Homuai.App.UseCases.MyFoods.DeleteMyFood;
using Homuai.App.UseCases.MyFoods.GetMyFoods;
using Homuai.App.UseCases.MyFoods.RegisterMyFood;
using Homuai.App.UseCases.MyFoods.UpdateMyFood;
using Homuai.App.UseCases.User.ChangePassword;
using Homuai.App.UseCases.User.EmailAlreadyBeenRegistered;
using Homuai.App.UseCases.User.RegisterUser;
using Homuai.App.UseCases.User.UpdateUserInformations;
using Homuai.App.UseCases.User.UserInformations;
using XLabs.Ioc;

namespace Homuai.App
{
    public static class Bootstrapper
    {
        public static IDependencyContainer AddDependeces(this IDependencyContainer container)
        {
            return container.Register(new UserPreferences())
                .Register<IEmailAlreadyBeenRegisteredUseCase, EmailAlreadyBeenRegisteredUseCase>()
                .Register<IRegisterUserUseCase, RegisterUserUseCase>()
                .Register<IUserInformationsUseCase, UserInformationsUseCase>()
                .Register<IUpdateUserInformationsUseCase, UpdateUserInformationsUseCase>()
                .Register<IChangePasswordUseCase, ChangePasswordUseCase>()
                .Register<ILoginUseCase, LoginUseCase>()
                .Register<IRequestCodeResetPasswordUseCase, RequestCodeResetPasswordUseCase>()
                .Register<IResetPasswordUseCase, ResetPasswordUseCase>()
                .Register<IRequestCEPUseCase, RequestCEPUseCase>()
                .Register<IRegisterHomeUseCase, RegisterHomeUseCase>()
                .Register<IGetMyFoodsUseCase, GetMyFoodsUseCase>()
                .Register<IRegisterMyFoodUseCase, RegisterMyFoodUseCase>()
                .Register<IChangeQuantityOfOneProductUseCase, ChangeQuantityOfOneProductUseCase>()
                .Register<IUpdateMyFoodUseCase, UpdateMyFoodUseCase>()
                .Register<IDeleteMyFoodUseCase, DeleteMyFoodUseCase>()
                .Register<IHomeInformationsUseCase, HomeInformationsUseCase>()
                .Register<IUpdateHomeInformationsUseCase, UpdateHomeInformationsUseCase>()
                .Register<IGetMyFriendsUseCase, GetMyFriendsUseCase>()
                .Register<INotifyOrderReceivedUseCase, NotifyOrderReceivedUseCase>()
                .Register<IChangeDateFriendJoinHomeUseCase, ChangeDateFriendJoinHomeUseCase>()
                .Register<IRemoveFriendUseCase, RemoveFriendUseCase>()
                .Register<IRequestCodeToRemoveFriendUseCase, RequestCodeToRemoveFriendUseCase>()
                .Register<IGetTasksUseCase, GetTasksUseCase>()
                .Register<ICreateFirstScheduleUseCase, CreateFirstScheduleUseCase>()
                .Register<IRegisterRoomCleanedUseCase, RegisterRoomCleanedUseCase>()
                .Register<IReminderUseCase, ReminderUseCase>()
                .Register<ICalendarUseCase, CalendarUseCase>()
                .Register<IHistoryOfTheDayUseCase, HistoryOfTheDayUseCase>()
                .Register<IEditTaskAssignUseCase, EditTaskAssignUseCase>()
                .Register<IRateTaskUseCase, RateTaskUseCase>()
                .Register<IDetailsAllRateUseCase, DetailsAllRateUseCase>()
                .Register<IContactUsUseCase, ContactUsUseCase>();
        }
    }
}
