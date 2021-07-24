using HashidsNet;
using Homuai.Application.Services.LoggedUser;
using Homuai.Communication.Request;
using Homuai.Communication.Response;
using Homuai.Domain.Dto;
using Homuai.Domain.Repository;
using Homuai.Domain.Repository.CleaningSchedule;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.CleaningSchedule.HistoryOfTheDay
{
    public class HistoryOfTheDayUseCase : IHistoryOfTheDayUseCase
    {
        private readonly HomuaiUseCase _homuaiUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly ICleaningScheduleReadOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHashids _hashids;

        public HistoryOfTheDayUseCase(ICleaningScheduleReadOnlyRepository repository, ILoggedUser loggedUser,
            HomuaiUseCase homuaiUseCase, IUnitOfWork unitOfWork,
            IHashids hashids)
        {
            _loggedUser = loggedUser;
            _repository = repository;
            _homuaiUseCase = homuaiUseCase;
            _unitOfWork = unitOfWork;
            _hashids = hashids;
        }

        public async Task<ResponseOutput> Execute(RequestHistoryOfTheDayJson request)
        {
            var loggedUser = await _loggedUser.User();
            
            var history = await _repository.GetHistoryOfTheDay(request.Date, loggedUser.HomeAssociation.HomeId, request.Room, loggedUser.Id);

            var responseJson = Mapper(history);
            var response = await _homuaiUseCase.CreateResponse(loggedUser.Email, loggedUser.Id, responseJson);

            await _unitOfWork.Commit();

            return response;
        }

        private IList<ResponseHistoryRoomOfTheDayJson> Mapper(IList<CleaningScheduleHistoryRoomOfTheDayDto> dto)
        {
            return dto.Select(c => new ResponseHistoryRoomOfTheDayJson
            {
                Room = c.Room,
                History = c.History.Select(w => new ResponseHistoryCleanDayJson
                {
                    User = w.User,
                    AverageRate = w.AverageRate,
                    CanRate = w.CanRate,
                    Id = _hashids.EncodeLong(w.Id),
                    CleanedAt = w.CleanedAt
                }).ToList()
            })
            .ToList();
        }
    }
}
