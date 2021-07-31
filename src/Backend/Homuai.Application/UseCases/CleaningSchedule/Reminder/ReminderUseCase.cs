using HashidsNet;
using Homuai.Application.Helper.Notification;
using Homuai.Application.Services.LoggedUser;
using Homuai.Domain.Repository;
using Homuai.Domain.Repository.User;
using Homuai.Domain.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.CleaningSchedule.Reminder
{
    public class ReminderUseCase : IReminderUseCase
    {
        private readonly IPushNotificationService _pushNotificationService;
        private readonly HomuaiUseCase _homuaiUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHashids _hashids;

        public ReminderUseCase(ILoggedUser loggedUser, HomuaiUseCase homuaiUseCase,
            IUnitOfWork unitOfWork, IHashids hashids, IPushNotificationService pushNotificationService,
            IUserReadOnlyRepository userReadOnlyRepository)
        {
            _hashids = hashids;
            _loggedUser = loggedUser;
            _homuaiUseCase = homuaiUseCase;
            _unitOfWork = unitOfWork;
            _pushNotificationService = pushNotificationService;
            _userReadOnlyRepository = userReadOnlyRepository;
        }

        public async Task<ResponseOutput> Execute(IList<string> usersId)
        {
            var loggedUser = await _loggedUser.User();
            var friends = await _userReadOnlyRepository.GetByHome(loggedUser.HomeAssociation.HomeId);

            var usersIds = usersId.Distinct()
                   .Select(c => _hashids.DecodeLong(c).First())
                   .Where(c => c != loggedUser.Id);

            await SendNotification(friends.Where(c => usersIds.Contains(c.Id)).Select(c => c.PushNotificationId).ToList());

            var response = await _homuaiUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            await _unitOfWork.Commit();

            return response;
        }

        private async Task SendNotification(List<string> pushNotificationIds)
        {
            (var titles, var messages) = new MessagesNotificationHelper().Messages(NotificationHelperType.CleaningScheduleReminder);

            await _pushNotificationService.Send(titles, messages, pushNotificationIds);
        }
    }
}
