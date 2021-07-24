using Homuai.Application.Services.LoggedUser;
using Homuai.Communication.Response;
using Homuai.Domain.Repository;
using Homuai.Domain.Repository.CleaningSchedule;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.CleaningSchedule.DetailsAllRate
{
    public class DetailsAllRateUseCase : IDetailsAllRateUseCase
    {
        private readonly HomuaiUseCase _homuaiUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly ICleaningScheduleReadOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DetailsAllRateUseCase(ICleaningScheduleReadOnlyRepository repository, ILoggedUser loggedUser,
            HomuaiUseCase homuaiUseCase, IUnitOfWork unitOfWork)
        {
            _loggedUser = loggedUser;
            _repository = repository;
            _homuaiUseCase = homuaiUseCase;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseOutput> Execute(long completedTaskId)
        {
            var loggedUser = await _loggedUser.User();

            var cleaningTasksCompleted = await _repository.GetTaskCompletedById(completedTaskId);

            var responseJson = Mapper(cleaningTasksCompleted);
            var response = await _homuaiUseCase.CreateResponse(loggedUser.Email, loggedUser.Id, responseJson);

            await _unitOfWork.Commit();

            return response;
        }

        private List<ResponseRateTaskJson> Mapper(Domain.Entity.CleaningTasksCompleted cleaningTasksCompleted)
        {
            return cleaningTasksCompleted.Ratings.Select(c => new ResponseRateTaskJson
            {
                Date = cleaningTasksCompleted.CreateDate,
                Feedback = c.FeedBack,
                Name = cleaningTasksCompleted.CleaningSchedule.User.Name,
                Room = cleaningTasksCompleted.CleaningSchedule.Room,
                Rating = c.Rating
            })
            .ToList();
        }
    }
}
