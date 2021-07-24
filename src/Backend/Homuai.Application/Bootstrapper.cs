using Homuai.Application.Services.Cryptography;
using Homuai.Application.Services.LoggedUser;
using Homuai.Application.Services.Token;
using Homuai.Application.UseCases;
using Homuai.Application.UseCases.CleaningSchedule.Calendar;
using Homuai.Application.UseCases.CleaningSchedule.CreateFirstSchedule;
using Homuai.Application.UseCases.CleaningSchedule.DetailsAllRate;
using Homuai.Application.UseCases.CleaningSchedule.EditTaskAssign;
using Homuai.Application.UseCases.CleaningSchedule.GetTasks;
using Homuai.Application.UseCases.CleaningSchedule.HistoryOfTheDay;
using Homuai.Application.UseCases.CleaningSchedule.ProcessRemindersOfCleaningTasks;
using Homuai.Application.UseCases.CleaningSchedule.RateTask;
using Homuai.Application.UseCases.CleaningSchedule.RegisterRoomCleaned;
using Homuai.Application.UseCases.CleaningSchedule.Reminder;
using Homuai.Application.UseCases.ContactUs;
using Homuai.Application.UseCases.Friends.AddFriends;
using Homuai.Application.UseCases.Friends.ChangeDateFriendJoinHome;
using Homuai.Application.UseCases.Friends.GetMyFriends;
using Homuai.Application.UseCases.Friends.NotifyOrderReceived;
using Homuai.Application.UseCases.Friends.RemoveFriend;
using Homuai.Application.UseCases.Home.HomeInformations;
using Homuai.Application.UseCases.Home.RegisterHome;
using Homuai.Application.UseCases.Home.UpdateHomeInformations;
using Homuai.Application.UseCases.Login.DoLogin;
using Homuai.Application.UseCases.Login.ForgotPassword;
using Homuai.Application.UseCases.MyFoods.ChangeQuantityOfOneProduct;
using Homuai.Application.UseCases.MyFoods.DeleteMyFood;
using Homuai.Application.UseCases.MyFoods.GetMyFoods;
using Homuai.Application.UseCases.MyFoods.ProcessFoodsNextToDueDate;
using Homuai.Application.UseCases.MyFoods.RegisterMyFood;
using Homuai.Application.UseCases.MyFoods.UpdateMyFood;
using Homuai.Application.UseCases.User.ChangePassword;
using Homuai.Application.UseCases.User.EmailAlreadyBeenRegistered;
using Homuai.Application.UseCases.User.RegisterUser;
using Homuai.Application.UseCases.User.UpdateUserInformations;
using Homuai.Application.UseCases.User.UserInformations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clearfield.Application
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddScoped(options => new TokenController(configuration.GetValue<double>("Settings:Jwt:ExpiresMinutes"),
                    configuration.GetValue<string>("Settings:Jwt:SigningKey")))

                .AddScoped<HomuaiUseCase>()

                .AddScoped(options => new PasswordEncripter(configuration.GetValue<string>("Settings:KeyAdditionalCryptography")))

                .AddScoped<ILoggedUser, LoggedUser>()
                .AddScoped<IProcessRemindersOfCleaningTasksUseCase, ProcessRemindersOfCleaningTasksUseCase>()
                .AddScoped<IProcessFoodsNextToDueDateUseCase, ProcessFoodsNextToDueDateUseCase>()
                .AddScoped<IEmailAlreadyBeenRegisteredUseCase, EmailAlreadyBeenRegisteredUseCase>()
                .AddScoped<IUpdateUserInformationsUseCase, UpdateUserInformationsUseCase>()
                .AddScoped<IRegisterUserUseCase, RegisterUserUseCase>()
                .AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>()
                .AddScoped<IUserInformationsUseCase, UserInformationsUseCase>()
                .AddScoped<IChangeQuantityOfOneProductUseCase, ChangeQuantityOfOneProductUseCase>()
                .AddScoped<IDeleteMyFoodUseCase, DeleteMyFoodUseCase>()
                .AddScoped<IGetMyFoodsUseCase, GetMyFoodsUseCase>()
                .AddScoped<IRegisterMyFoodUseCase, RegisterMyFoodUseCase>()
                .AddScoped<IUpdateMyFoodUseCase, UpdateMyFoodUseCase>()
                .AddScoped<ILoginUseCase, LoginUseCase>()
                .AddScoped<IRequestCodeResetPasswordUseCase, RequestCodeResetPasswordUseCase>()
                .AddScoped<IResetPasswordUseCase, ResetPasswordUseCase>()
                .AddScoped<IHomeInformationsUseCase, HomeInformationsUseCase>()
                .AddScoped<IRegisterHomeUseCase, RegisterHomeUseCase>()
                .AddScoped<IUpdateHomeInformationsUseCase, UpdateHomeInformationsUseCase>()
                .AddScoped<IAddFriendUseCase, AddFriendUseCase>()
                .AddScoped<IChangeDateFriendJoinHomeUseCase, ChangeDateFriendJoinHomeUseCase>()
                .AddScoped<IGetMyFriendsUseCase, GetMyFriendsUseCase>()
                .AddScoped<INotifyOrderReceivedUseCase, NotifyOrderReceivedUseCase>()
                .AddScoped<IRequestCodeToRemoveFriendUseCase, RequestCodeToRemoveFriendUseCase>()
                .AddScoped<IRemoveFriendUseCase, RemoveFriendUseCase>()
                .AddScoped<IContactUsUseCase, ContactUsUseCase>()
                .AddScoped<ICreateFirstScheduleUseCase, CreateFirstScheduleUseCase>()
                .AddScoped<IDetailsAllRateUseCase, DetailsAllRateUseCase>()
                .AddScoped<IEditTaskAssignUseCase, EditTaskAssignUseCase>()
                .AddScoped<IGetTasksUseCase, GetTasksUseCase>()
                .AddScoped<IHistoryOfTheDayUseCase, HistoryOfTheDayUseCase>()
                .AddScoped<IRateTaskUseCase, RateTaskUseCase>()
                .AddScoped<IRegisterRoomCleanedUseCase, RegisterRoomCleanedUseCase>()
                .AddScoped<IReminderUseCase, ReminderUseCase>()
                .AddScoped<ICalendarUseCase, CalendarUseCase>();
        }
    }
}
