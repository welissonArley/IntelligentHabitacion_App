using Homuai.App.Model;
using Homuai.App.Services;
using Homuai.App.Services.Communication.CleaningSchedule;
using Homuai.Communication.Request;
using Homuai.Communication.Response;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.CleaningSchedule.CreateFirstSchedule
{
    public class CreateFirstScheduleUseCase : UseCaseBase, ICreateFirstScheduleUseCase
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;
        private readonly ICleaningScheduleService _restService;

        public CreateFirstScheduleUseCase(Lazy<UserPreferences> userPreferences) : base("cleaning-schedule")
        {
            this.userPreferences = userPreferences;
            _restService = RestService.For<ICleaningScheduleService>(BaseAddress());
        }

        public async Task<ScheduleTasksCleaningHouseModel> Execute(IList<FriendCreateFirstScheduleModel> usersTasks)
        {
            var request = Mapper(usersTasks);

            var response = await _restService.CreateFirstCleaningSchedule(request, await _userPreferences.GetToken(), GetLanguage());

            ResponseValidate(response);

            await _userPreferences.ChangeToken(GetTokenOnHeaderRequest(response.Headers));

            return Mapper(response.Content);
        }

        private IList<RequestUpdateCleaningScheduleJson> Mapper(IList<FriendCreateFirstScheduleModel> usersTasks)
        {
            return usersTasks.Select(c => new RequestUpdateCleaningScheduleJson
            {
                UserId = c.Id,
                Rooms = c.Tasks.Select(w => w.Room).ToList()
            }).ToList();
        }
        private ScheduleTasksCleaningHouseModel Mapper(ResponseScheduleTasksCleaningHouseJson response)
        {
            return new ScheduleTasksCleaningHouseModel
            {
                Date = response.Date,
                AmountOfTasks = response.AmountOfTasks,
                Name = response.Name,
                ProfileColorDarkMode = response.ProfileColorDarkMode,
                ProfileColorLightMode = response.ProfileColorLightMode,
                Tasks = new ObservableCollection<TaskModel>(response.Tasks.Select(c => new TaskModel
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
                }))
            };
        }
    }
}
