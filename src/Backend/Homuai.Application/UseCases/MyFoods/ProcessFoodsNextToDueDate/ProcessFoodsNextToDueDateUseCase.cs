using Homuai.Application.Helper.Notification;
using Homuai.Domain.Entity;
using Homuai.Domain.Repository.MyFoods;
using Homuai.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.MyFoods.ProcessFoodsNextToDueDate
{
    public class ProcessFoodsNextToDueDateUseCase : IProcessFoodsNextToDueDateUseCase
    {
        private readonly IMyFoodsWriteOnlyRepository _myFoodWriteRepository;
        private readonly IMyFoodsReadOnlyRepository _myFoodReadOnlyRepository;
        private readonly IPushNotificationService _pushNotificationService;
        private readonly MessagesNotificationHelper _messagesNotificationHelper;

        public ProcessFoodsNextToDueDateUseCase(IMyFoodsWriteOnlyRepository myFoodWriteRepository,
            IMyFoodsReadOnlyRepository myFoodReadOnlyRepository,
            IPushNotificationService pushNotificationService)
        {
            _myFoodWriteRepository = myFoodWriteRepository;
            _myFoodReadOnlyRepository = myFoodReadOnlyRepository;
            _pushNotificationService = pushNotificationService;
            _messagesNotificationHelper = new MessagesNotificationHelper();
        }

        public async Task Execute()
        {
            var query = await _myFoodReadOnlyRepository.GetExpiredOrCloseToDueDate();

            var group = query.GroupBy(c => c.UserId);

            var users = query.Select(c => c.User).Distinct();

            if (query.Any())
            {
                foreach (var result in group)
                {
                    var foodList = result.ToList();
                    await ProcessFoodList(users.First(c => c.Id == result.Key), foodList);
                }
            }
        }

        private async Task ProcessFoodList(Domain.Entity.User user, List<MyFood> listFoods)
        {
            var today = DateTime.UtcNow.Date;
            Dictionary<string, string> titles;
            Dictionary<string, string> messages;

            foreach (var food in listFoods)
            {
                var totalDays = (food.DueDate.Value - today).TotalDays;
                var foodName = new string[1] { food.Name };

                switch (totalDays)
                {
                    case 7:
                        {
                            (titles, messages) = _messagesNotificationHelper.Messages(NotificationHelperType.SevenDaysProductExpiration, foodName);
                        }
                        break;
                    case 3:
                        {
                            (titles, messages) = _messagesNotificationHelper.Messages(NotificationHelperType.ThreeDaysProductExpiration, foodName);
                        }
                        break;
                    case 1:
                        {
                            (titles, messages) = _messagesNotificationHelper.Messages(NotificationHelperType.TomorrowDayProductExpiration, foodName);
                        }
                        break;
                    case 0:
                        {
                            (titles, messages) = _messagesNotificationHelper.Messages(NotificationHelperType.TodayProductExpiration, foodName);
                        }
                        break;
                    case -1:
                        {
                            (titles, messages) = _messagesNotificationHelper.Messages(NotificationHelperType.YesterdayProductExpiration, foodName);
                        }
                        break;
                    case -2:
                        {
                            (titles, messages) = _messagesNotificationHelper.Messages(NotificationHelperType.TwoDaysPassedProductExpiration, foodName);
                        }
                        break;
                    default:
                        {
                            (titles, messages) = _messagesNotificationHelper.Messages(NotificationHelperType.DeletedProductExpiration, foodName);

                            _myFoodWriteRepository.Delete(food);
                        }
                        break;
                }
                
                await SendNotification(user, titles, messages);
            }
        }

        private async Task SendNotification(Domain.Entity.User user, Dictionary<string, string> titles, Dictionary<string, string> messages)
        {
            await _pushNotificationService.Send(titles, messages, new List<string> { user.PushNotificationId }, DateTime.Today.RandomTimeMorning());
        }
    }
}
