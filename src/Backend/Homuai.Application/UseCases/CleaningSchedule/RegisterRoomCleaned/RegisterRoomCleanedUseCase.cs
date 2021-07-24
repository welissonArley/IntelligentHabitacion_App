using HashidsNet;
using Homuai.Application.Services.LoggedUser;
using Homuai.Communication.Request;
using Homuai.Domain.Repository;
using Homuai.Domain.Repository.CleaningSchedule;
using Homuai.Domain.Repository.User;
using Homuai.Domain.Services;
using Homuai.Exception.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.CleaningSchedule.RegisterRoomCleaned
{
    public class RegisterRoomCleanedUseCase : IRegisterRoomCleanedUseCase
    {
        private readonly IPushNotificationService _pushNotificationService;
        private readonly HomuaiUseCase _homuaiUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly ICleaningScheduleWriteOnlyRepository _repository;
        private readonly ICleaningScheduleReadOnlyRepository _repositoryCleaningScheduleReadOnly;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IHashids _hashids;

        public RegisterRoomCleanedUseCase(ICleaningScheduleWriteOnlyRepository repository, ILoggedUser loggedUser,
            HomuaiUseCase homuaiUseCase, IUnitOfWork unitOfWork,
            ICleaningScheduleReadOnlyRepository repositoryCleaningScheduleReadOnly, IHashids hashids,
            IPushNotificationService pushNotificationService, IUserReadOnlyRepository userReadOnlyRepository)
        {
            _loggedUser = loggedUser;
            _repository = repository;
            _homuaiUseCase = homuaiUseCase;
            _unitOfWork = unitOfWork;
            _repositoryCleaningScheduleReadOnly = repositoryCleaningScheduleReadOnly;
            _pushNotificationService = pushNotificationService;
            _userReadOnlyRepository = userReadOnlyRepository;
            _hashids = hashids;
        }

        public async Task<ResponseOutput> Execute(RequestRegisterRoomCleaned request)
        {
            ValidateRequest(request);

            var loggedUser = await _loggedUser.User();
            var rooms = new List<string>();

            foreach(var id in request.TaskIds)
            {
                var taskId = _hashids.DecodeLong(id).First();
                var task = await _repositoryCleaningScheduleReadOnly.GetTaskById(taskId);
                await TaskValidate(loggedUser, task, request.Date);

                await _repository.RegisterRoomCleaned(taskId, request.Date);

                rooms.Add(task.Room);
            }

            var response = await _homuaiUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            await _unitOfWork.Commit();

            var friends = (await _userReadOnlyRepository.GetByHome(loggedUser.HomeAssociation.HomeId)).Where(c => c.Id != loggedUser.Id).Select(c => c.PushNotificationId).ToList();

            foreach (var room in rooms)
                await SendNotification(loggedUser.Name, room, friends);

            return response;
        }

        private async Task TaskValidate(Domain.Entity.User loggedUser, Domain.Entity.CleaningSchedule task, DateTime date)
        {
            if (task == null)
                throw new InvalidTaskException();

            if (task.HomeId != loggedUser.HomeAssociation.HomeId || task.UserId != loggedUser.Id)
                throw new YouCannotPerformThisActionException();

            var taskCleanedOnDate = await _repositoryCleaningScheduleReadOnly.TaskCleanedOnDate(task.Id, date);
            if (taskCleanedOnDate)
                throw new UserAlreadyRegisterRoomCleanedException();
        }
        private void ValidateRequest(RequestRegisterRoomCleaned request)
        {
            var today = DateTime.UtcNow.Date;
            if ((request.Date - today).TotalDays > 4 || request.Date.Year != today.Year || request.Date.Month != today.Month)
                throw new UserCannotRegisterRoomThisDateCleanedException();

            if (!request.TaskIds.Any())
                throw new InvalidTaskException();
        }

        private async Task SendNotification(string userName, string room, List<string> pushNotificationIds)
        {
            var titles = new Dictionary<string, string>
            {
                { "en", "Clean room 💦" },
                { "pt", "Cômodo limpo 💦" }
            };
            var messages = new Dictionary<string, string>
            {
                { "en", string.Format("{0} cleaned the {1}, uhuu. You can now go to the App and rate the task ✔️", userName, room) },
                { "pt", string.Format("{0} limpou a(o) {1}, uhuu. Você já pode ir no App e avaliar a tarefa ✔️", userName, room) }
            };

            await _pushNotificationService.Send(titles, messages, pushNotificationIds);
        }
    }
}
