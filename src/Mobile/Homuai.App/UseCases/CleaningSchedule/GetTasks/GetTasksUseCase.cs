using Homuai.App.Model;
using Homuai.App.Services;
using Homuai.App.Services.Communication.CleaningSchedule;
using Homuai.Communication.Request;
using Homuai.Communication.Response;
using Refit;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.CleaningSchedule.GetTasks
{
    public class GetTasksUseCase : UseCaseBase, IGetTasksUseCase
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;
        private readonly ICleaningScheduleService _restService;

        public GetTasksUseCase(Lazy<UserPreferences> userPreferences) : base("cleaning-schedule")
        {
            this.userPreferences = userPreferences;
            _restService = RestService.For<ICleaningScheduleService>(BaseAddress());
        }

        public async Task<ScheduleCleaningHouseModel> Execute(DateTime date)
        {
            var token = await _userPreferences.GetToken();
            var response = await _restService.GetTasks(new RequestDateJson { Date = date }, token, GetLanguage());

            ResponseValidate(response);

            await _userPreferences.ChangeToken(GetTokenOnHeaderRequest(response.Headers));

            return Mapper(response.Content);
        }

        private ScheduleCleaningHouseModel Mapper(ResponseTasksJson response)
        {
            return new ScheduleCleaningHouseModel
            {
                Action = response.Action,
                Message = response.Message,
                Schedule = new ScheduleTasksCleaningHouseModel
                {
                    Date = response.Schedule.Date,
                    AmountOfTasks = response.Schedule.AmountOfTasks,
                    Name = response.Schedule.Name,
                    ProfileColorDarkMode = response.Schedule.ProfileColorDarkMode,
                    ProfileColorLightMode = response.Schedule.ProfileColorLightMode,
                    Tasks = new ObservableCollection<TaskModel>(response.Schedule.Tasks.Select(c => new TaskModel
                    {
                        IdTaskToRegisterRoomCleaning = c.IdTaskToRegisterRoomCleaning,
                        CanRegisterRoomCleanedToday = c.CanCompletedToday,
                        CanEdit = c.CanEdit,
                        CanRate = c.CanRate,
                        Room = c.Room,
                        Assign = new ObservableCollection<UserSimplifiedModel>(c.Assign.Select(w => new UserSimplifiedModel
                        {
                            Id = w.Id,
                            Name = w.Name,
                            ProfileColorLightMode = w.ProfileColorLightMode,
                            ProfileColorDarkMode = w.ProfileColorDarkMode
                        }))
                    })),
                    AvaliableUsersToAssign = new ObservableCollection<UserSimplifiedModel>(response.Schedule.AvaliableUsersToAssign.Select(c => new UserSimplifiedModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        ProfileColorDarkMode = c.ProfileColorDarkMode,
                        ProfileColorLightMode = c.ProfileColorLightMode
                    }))
                },
                CreateSchedule = new CreateScheduleCleaningHouseModel
                {
                    Rooms = response.CreateSchedule.Rooms.Select(c => new RoomModel
                    {
                        Id = c.Id,
                        Room = c.Name
                    }).ToList(),
                    Friends = new ObservableCollection<FriendCreateFirstScheduleModel>(response.CreateSchedule.Friends.Select(c => new FriendCreateFirstScheduleModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        ProfileColorLightMode = c.ProfileColorLightMode,
                        ProfileColorDarkMode = c.ProfileColorDarkMode
                    }))
                }
            };
        }
    }
}
