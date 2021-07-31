using HashidsNet;
using Homuai.Application.Helper.Notification;
using Homuai.Application.Services.LoggedUser;
using Homuai.Communication.Request;
using Homuai.Domain.Repository;
using Homuai.Domain.Repository.CleaningSchedule;
using Homuai.Domain.Repository.User;
using Homuai.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.CleaningSchedule.EditTaskAssign
{
    public class EditTaskAssignUseCase : IEditTaskAssignUseCase
    {
        private readonly IPushNotificationService _pushNotificationService;
        private readonly HomuaiUseCase _homuaiUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHashids _hashids;
        private readonly ICleaningScheduleReadOnlyRepository _cleaningScheduleReadOnlyRepository;
        private readonly ICleaningScheduleWriteOnlyRepository _cleaningScheduleWriteOnlyRepository;

        public EditTaskAssignUseCase(ILoggedUser loggedUser, HomuaiUseCase homuaiUseCase,
            IUnitOfWork unitOfWork, IHashids hashids, IUserReadOnlyRepository userReadOnlyRepository,
            ICleaningScheduleReadOnlyRepository cleaningScheduleReadOnlyRepository,
            IPushNotificationService pushNotificationService,
            ICleaningScheduleWriteOnlyRepository cleaningScheduleWriteOnlyRepository)
        {
            _hashids = hashids;
            _loggedUser = loggedUser;
            _homuaiUseCase = homuaiUseCase;
            _unitOfWork = unitOfWork;
            _userReadOnlyRepository = userReadOnlyRepository;
            _cleaningScheduleReadOnlyRepository = cleaningScheduleReadOnlyRepository;
            _pushNotificationService = pushNotificationService;
            _cleaningScheduleWriteOnlyRepository = cleaningScheduleWriteOnlyRepository;
        }

        public async Task<ResponseOutput> Execute(RequestEditAssignCleaningScheduleJson request)
        {
            var loggedUser = await _loggedUser.User();
            
            var scheduleRoom = await _cleaningScheduleReadOnlyRepository.GetScheduleRoomForCurrentMonth(loggedUser.HomeAssociation.HomeId, request.Room);

            var usersIds = request.UserIds.Select(c => _hashids.DecodeLong(c).First());

            var scheduleToRemoveOrFinish = scheduleRoom.Where(c => usersIds.All(w => w != c.UserId));

            await FinishOrRemoveSchedule(scheduleToRemoveOrFinish);

            var scheduleToAdd = usersIds.Where(c => scheduleRoom.All(w => w.UserId != c));

            await Add(scheduleToAdd, request.Room, loggedUser.HomeAssociation.HomeId);

            var response = await _homuaiUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            await _unitOfWork.Commit();

            var friends = await _userReadOnlyRepository.GetByHome(loggedUser.HomeAssociation.HomeId);
            await SendNotification(friends.Where(c => c.Id != loggedUser.Id && usersIds.Any(w => w == c.Id)).Select(c => c.PushNotificationId).ToList());

            return response;
        }

        private async Task FinishOrRemoveSchedule(IEnumerable<Domain.Entity.CleaningSchedule> list)
        {
            foreach(var schedule in list)
            {
                if (schedule.CleaningTasksCompleteds.Any())
                    await _cleaningScheduleWriteOnlyRepository.FinishTask(schedule.Id);
                else
                    _cleaningScheduleWriteOnlyRepository.Remove(schedule);
            }
        }
        private async Task Add(IEnumerable<long> list, string room, long homeId)
        {
            await _cleaningScheduleWriteOnlyRepository.Add(list.Select(c => new Domain.Entity.CleaningSchedule
            {
                HomeId = homeId,
                ScheduleStartAt = DateTime.UtcNow,
                UserId = c,
                Room = room
            }));
        }

        private async Task SendNotification(List<string> pushNotificationIds)
        {
            (var titles, var messages) = new MessagesNotificationHelper().Messages(NotificationHelperType.CleaningScheduleUpdated);

            await _pushNotificationService.Send(titles, messages, pushNotificationIds);
        }
    }
}
