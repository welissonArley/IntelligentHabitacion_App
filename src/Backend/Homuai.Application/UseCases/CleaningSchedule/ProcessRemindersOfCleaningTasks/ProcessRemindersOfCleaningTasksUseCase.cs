using Homuai.Application.Helper.Notification;
using Homuai.Domain.Repository.CleaningSchedule;
using Homuai.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.CleaningSchedule.ProcessRemindersOfCleaningTasks
{
    public class ProcessRemindersOfCleaningTasksUseCase : IProcessRemindersOfCleaningTasksUseCase
    {
        private readonly IPushNotificationService _pushNotificationService;
        private readonly ICleaningScheduleReadOnlyRepository _repository;
        private readonly MessagesNotificationHelper _messagesNotificationHelper;

        public ProcessRemindersOfCleaningTasksUseCase(IPushNotificationService pushNotificationService,
            ICleaningScheduleReadOnlyRepository repository)
        {
            _pushNotificationService = pushNotificationService;
            _repository = repository;
            _messagesNotificationHelper = new MessagesNotificationHelper();
        }

        public async Task Execute()
        {
            var query = await _repository.GetTasksWithMoreThan8daysWithoutClompleted();

            var group = query.GroupBy(c => c.UserId);

            var users = query.Select(c => c.User).Distinct();

            if (query.Any())
            {
                foreach (var result in group)
                {
                    var schedules = result.ToList();
                    await ProcessSchedules(users.First(c => c.Id == result.Key), schedules);
                }
            }
        }

        private async Task ProcessSchedules(Domain.Entity.User user, List<Domain.Entity.CleaningSchedule> schedules)
        {
            var today = DateTime.UtcNow.Date;

            foreach (var task in schedules)
            {
                var totalDays = Convert.ToInt32((today - (task.CleaningTasksCompleteds.Any() ? task.CleaningTasksCompleteds.OrderByDescending(c => c.CreateDate).First().CreateDate : task.ScheduleStartAt)).TotalDays);

                (var titles, var messages) = _messagesNotificationHelper.Messages(NotificationHelperType.ReminderTotalDaysWithoutCleanRoom, new string[2] { totalDays.ToString(), task.Room.ToUpper() });

                await SendNotification(user, titles, messages);
            }
        }

        private async Task SendNotification(Domain.Entity.User user, Dictionary<string, string> titles, Dictionary<string, string> messages)
        {
            await _pushNotificationService.Send(titles, messages, new List<string> { user.PushNotificationId }, DateTime.Today.RandomTimeMorning());
        }
    }
}
