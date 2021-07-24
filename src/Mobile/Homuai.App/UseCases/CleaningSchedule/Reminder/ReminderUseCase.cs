using Homuai.App.Services;
using Homuai.App.Services.Communication.CleaningSchedule;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.CleaningSchedule.Reminder
{
    public class ReminderUseCase : UseCaseBase, IReminderUseCase
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;
        private readonly ICleaningScheduleService _restService;

        public ReminderUseCase(Lazy<UserPreferences> userPreferences) : base("cleaning-schedule")
        {
            this.userPreferences = userPreferences;
            _restService = RestService.For<ICleaningScheduleService>(BaseAddress());
        }

        public async Task Execute(IList<string> usersIds)
        {
            if (!usersIds.Any())
                return;

            var token = await _userPreferences.GetToken();
            var response = await _restService.ReminderUsers(usersIds, token, GetLanguage());

            ResponseValidate(response);

            await _userPreferences.ChangeToken(GetTokenOnHeaderRequest(response.Headers));
        }
    }
}
