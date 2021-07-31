using Homuai.Application.Helper.Notification;
using Homuai.Application.Services.Cryptography;
using Homuai.Application.Services.LoggedUser;
using Homuai.Communication.Request;
using Homuai.Domain.Enums;
using Homuai.Domain.Repository;
using Homuai.Domain.Repository.CleaningSchedule;
using Homuai.Domain.Repository.Code;
using Homuai.Domain.Repository.MyFoods;
using Homuai.Domain.Repository.User;
using Homuai.Domain.Services;
using Homuai.Exception.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.Friends.RemoveFriend
{
    public class RemoveFriendUseCase : IRemoveFriendUseCase
    {
        private readonly HomuaiUseCase _homuaiUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPushNotificationService _pushNotificationService;
        private readonly IUserUpdateOnlyRepository _repository;
        private readonly ICodeWriteOnlyRepository _codeRepository;
        private readonly PasswordEncripter _cryptography;
        private readonly ICodeReadOnlyRepository _codeReadOnlyRepository;
        private readonly IMyFoodsWriteOnlyRepository _myFoodsRepository;
        private readonly ICleaningScheduleWriteOnlyRepository _scheduleRepository;

        public RemoveFriendUseCase(ILoggedUser loggedUser, IPushNotificationService pushNotificationService,
            IUnitOfWork unitOfWork, ICodeWriteOnlyRepository codeRepository, PasswordEncripter cryptography,
            HomuaiUseCase homuaiUseCase, IUserUpdateOnlyRepository repository,
            ICodeReadOnlyRepository codeReadOnlyRepository, IMyFoodsWriteOnlyRepository myFoodsRepository,
            ICleaningScheduleWriteOnlyRepository scheduleRepository)
        {
            _loggedUser = loggedUser;
            _homuaiUseCase = homuaiUseCase;
            _unitOfWork = unitOfWork;
            _pushNotificationService = pushNotificationService;
            _repository = repository;
            _codeRepository = codeRepository;
            _cryptography = cryptography;
            _codeReadOnlyRepository = codeReadOnlyRepository;
            _myFoodsRepository = myFoodsRepository;
            _scheduleRepository = scheduleRepository;
        }

        public async Task<ResponseOutput> Execute(long friendId, RequestAdminActionJson request)
        {
            var loggedUser = await _loggedUser.User();

            var friend = await _repository.GetById_Update(friendId);
            var pushNotificationId = friend.PushNotificationId;

            await ValidateActionOnFriend(loggedUser, friend, request);

            friend.HomeAssociationId = null;
            friend.HomeAssociation.ExitOn = DateTime.UtcNow;

            _repository.Update(friend);

            _codeRepository.DeleteAllFromTheUser(loggedUser.Id);

            _myFoodsRepository.DeleteAllFromTheUser(friend.Id);
            await _scheduleRepository.FinishAllFromTheUser(friend.Id, loggedUser.HomeAssociation.HomeId);

            var response = await _homuaiUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            await _unitOfWork.Commit();

            await SendNotification(pushNotificationId);

            return response;
        }

        private async Task SendNotification(string pushNotificationId)
        {
            (var titles, var messages) = new MessagesNotificationHelper().Messages(NotificationHelperType.RemovedFromHome);
            var data = new Dictionary<string, string> { { EnumNotifications.RemovedFromHome, "1" } };

            await _pushNotificationService.Send(titles, messages, new List<string> { pushNotificationId }, data);
        }

        private async Task ValidateActionOnFriend(Domain.Entity.User loggedUser, Domain.Entity.User friend, RequestAdminActionJson request)
        {
            if (friend == null)
                throw new FriendNotFoundException();

            if (friend.HomeAssociation == null || friend.HomeAssociation.HomeId != loggedUser.HomeAssociation.HomeId)
                throw new YouCannotPerformThisActionException();

            if (!loggedUser.Password.Equals(_cryptography.Encrypt(request.Password)))
                throw new CodeOrPasswordInvalidException();

            var userCode = await _codeReadOnlyRepository.GetByCode(request.Code);

            if (userCode == null || userCode.Type != CodeType.RemoveFriend)
                throw new CodeOrPasswordInvalidException();

            if (userCode.CreateDate.AddMinutes(10) < DateTime.UtcNow)
                throw new ExpiredCodeException();
        }
    }
}
