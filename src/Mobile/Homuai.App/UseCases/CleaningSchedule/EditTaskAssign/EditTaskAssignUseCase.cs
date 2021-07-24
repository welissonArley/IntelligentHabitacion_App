using Homuai.App.Services;
using Homuai.App.Services.Communication.CleaningSchedule;
using Homuai.Communication.Request;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.CleaningSchedule.EditTaskAssign
{
    public class EditTaskAssignUseCase : UseCaseBase, IEditTaskAssignUseCase
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;
        private readonly ICleaningScheduleService _restService;

        public EditTaskAssignUseCase(Lazy<UserPreferences> userPreferences) : base("cleaning-schedule")
        {
            this.userPreferences = userPreferences;
            _restService = RestService.For<ICleaningScheduleService>(BaseAddress());
        }

        public async Task Execute(List<string> userIds, string room)
        {
            var request = Mapper(userIds, room);

            var response = await _restService.EditTaskAssign(request, await _userPreferences.GetToken(), GetLanguage());

            ResponseValidate(response);

            await _userPreferences.ChangeToken(GetTokenOnHeaderRequest(response.Headers));
        }

        private RequestEditAssignCleaningScheduleJson Mapper(List<string> userIds, string room)
        {
            return new RequestEditAssignCleaningScheduleJson
            {
                Room = room,
                UserIds = userIds
            };
        }
    }
}
