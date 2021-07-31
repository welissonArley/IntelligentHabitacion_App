using Homuai.Application.Helper.Notification;
using Homuai.Application.Services.LoggedUser;
using Homuai.Domain.Enums;
using Homuai.Domain.Repository;
using Homuai.Domain.Repository.User;
using Homuai.Domain.Services;
using Homuai.Exception.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.Friends.NotifyOrderReceived
{
    public class NotifyOrderReceivedUseCase : INotifyOrderReceivedUseCase
    {
        private readonly HomuaiUseCase _homuaiUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPushNotificationService _pushNotificationService;
        private readonly IUserReadOnlyRepository _repository;

        public NotifyOrderReceivedUseCase(ILoggedUser loggedUser, IPushNotificationService pushNotificationService,
            IUnitOfWork unitOfWork, HomuaiUseCase homuaiUseCase,
            IUserReadOnlyRepository repository)
        {
            _loggedUser = loggedUser;
            _homuaiUseCase = homuaiUseCase;
            _unitOfWork = unitOfWork;
            _pushNotificationService = pushNotificationService;
            _repository = repository;
        }

        public async Task<ResponseOutput> Execute(long friendId)
        {
            var loggedUser = await _loggedUser.User();
            var friend = await _repository.GetById(friendId);

            Validate(friend, loggedUser);

            await SendNotification(friend.PushNotificationId);

            var response = await _homuaiUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            await _unitOfWork.Commit();

            return response;
        }

        private void Validate(Domain.Entity.User friend, Domain.Entity.User loggedUser)
        {
            if (friend == null)
                throw new FriendNotFoundException();

            if (friend.HomeAssociation.Home.AdministratorId != loggedUser.Id)
                throw new YouCannotPerformThisActionException();
        }

        private async Task SendNotification(string pushNotificationId)
        {
            (var titles, var messages) = new MessagesNotificationHelper().Messages(NotificationHelperType.DeliveryReceived);

            var data = new Dictionary<string, string> { { EnumNotifications.OrderReceived, "1" } };

            await _pushNotificationService.Send(titles, messages, new List<string> { pushNotificationId }, data);
        }
    }
}
