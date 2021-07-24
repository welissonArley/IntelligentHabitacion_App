using Homuai.Application.Services.LoggedUser;
using Homuai.Communication.Request;
using Homuai.Communication.Response;
using Homuai.Domain.Repository;
using Homuai.Domain.Repository.CleaningSchedule;
using Homuai.Domain.Services;
using Homuai.Exception.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.CleaningSchedule.RateTask
{
    public class RateTaskUseCase : IRateTaskUseCase
    {
        private readonly IPushNotificationService _pushNotificationService;
        private readonly HomuaiUseCase _homuaiUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICleaningScheduleWriteOnlyRepository _repositoryWriteOnly;
        private readonly ICleaningScheduleReadOnlyRepository _repositoryReadOnly;

        public RateTaskUseCase(IPushNotificationService pushNotificationService, ILoggedUser loggedUser,
            HomuaiUseCase homuaiUseCase, IUnitOfWork unitOfWork,
            ICleaningScheduleWriteOnlyRepository repositoryWriteOnly,
            ICleaningScheduleReadOnlyRepository repositoryReadOnly)
        {
            _pushNotificationService = pushNotificationService;
            _loggedUser = loggedUser;
            _homuaiUseCase = homuaiUseCase;
            _unitOfWork = unitOfWork;
            _repositoryWriteOnly = repositoryWriteOnly;
            _repositoryReadOnly = repositoryReadOnly;
        }

        public async Task<ResponseOutput> Execute(long taskCompletedId, RequestRateTaskJson request)
        {
            var loggedUser = await _loggedUser.User();

            var task = await _repositoryReadOnly.GetTaskCompletedById(taskCompletedId);

            await Validate(task, loggedUser, request);

            var averageRating = await _repositoryWriteOnly.AddRateTask_ReturnAverageRating(
                new Domain.Entity.CleaningRating
                {
                    CleaningTaskCompletedId = taskCompletedId,
                    Rating = request.Rating,
                    FeedBack = request.FeedBack
                }, loggedUser.Id);

            var response = await _homuaiUseCase.CreateResponse(loggedUser.Email, loggedUser.Id, new ResponseAverageRatingJson { AverageRating = averageRating });

            await _unitOfWork.Commit();

            await SendNotification(task.CleaningSchedule.Room, task.CleaningSchedule.User.PushNotificationId);

            return response;
        }

        private async Task Validate(Domain.Entity.CleaningTasksCompleted task, Domain.Entity.User userLogged, RequestRateTaskJson request)
        {
            if (task == null)
                throw new InvalidTaskException();

            if (!(request.Rating >= 0 && request.Rating <= 5))
                throw new InvalidRatingException();

            if (task.CleaningSchedule.UserId == userLogged.Id)
                throw new UserRatingOwnTaskException();

            if (task.CleaningSchedule.User.HomeAssociation.HomeId != userLogged.HomeAssociation.HomeId)
                throw new YouCannotPerformThisActionException();

            if (!(task.CleaningSchedule.ScheduleStartAt.Year == DateTime.UtcNow.Year && task.CleaningSchedule.ScheduleStartAt.Month == DateTime.UtcNow.Month))
                throw new InvalidDateToRateException();

            var userAlreadyRatedTheTask = await _repositoryReadOnly.UserAlreadyRatedTheTask(userLogged.Id, task.Id);
            if (userAlreadyRatedTheTask)
                throw new UserAlreadyRateTaskException();
        }

        private async Task SendNotification(string room, string pushNotificationId)
        {
            var titles = new Dictionary<string, string>
            {
                { "en", "Cleaning task rated 🌟" },
                { "pt", "Tarefa de limpeza avaliada 🌟" }
            };
            var messages = new Dictionary<string, string>
            {
                { "en", string.Format("Your cleaning task ({0}) has been rated :) Enter the app and check ✔️", room) },
                { "pt", string.Format("Sua tarefa de limpeza ({0}) foi avaliada :) Entre no app e confira ✔️", room) }
            };

            await _pushNotificationService.Send(titles, messages, new List<string> { pushNotificationId });
        }
    }
}
