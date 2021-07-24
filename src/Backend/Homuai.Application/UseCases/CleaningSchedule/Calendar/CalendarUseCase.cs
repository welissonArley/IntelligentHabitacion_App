using Homuai.Application.Services.LoggedUser;
using Homuai.Communication.Request;
using Homuai.Communication.Response;
using Homuai.Domain.Dto;
using Homuai.Domain.Repository;
using Homuai.Domain.Repository.CleaningSchedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.CleaningSchedule.Calendar
{
    public class CalendarUseCase : ICalendarUseCase
    {
        private readonly HomuaiUseCase _homuaiUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly ICleaningScheduleReadOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CalendarUseCase(ICleaningScheduleReadOnlyRepository repository, ILoggedUser loggedUser,
            HomuaiUseCase homuaiUseCase, IUnitOfWork unitOfWork)
        {
            _loggedUser = loggedUser;
            _repository = repository;
            _homuaiUseCase = homuaiUseCase;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseOutput> Execute(RequestCalendarCleaningScheduleJson request)
        {
            var loggedUser = await _loggedUser.User();

            var calendar = await _repository.GetCalendarTasksForMonth(request.Month, loggedUser.HomeAssociation.HomeId, request.Room, loggedUser.Id);

            var responseJson = Mapper(request.Month, calendar);
            var response = await _homuaiUseCase.CreateResponse(loggedUser.Email, loggedUser.Id, responseJson);

            await _unitOfWork.Commit();

            return response;
        }

        private ResponseCalendarCleaningScheduleJson Mapper(DateTime date, IList<CleaningScheduleCalendarDayInfoDto> dto)
        {
            return new ResponseCalendarCleaningScheduleJson
            {
                Date = date,
                CleanedDays = dto.Select(c => new ResponseCleaningScheduleCalendarDayInfoJson
                {
                    Day = c.Day,
                    AmountCleanedRecords = c.AmountCleanedRecords,
                    AmountcleanedRecordsToRate = c.AmountcleanedRecordsToRate
                })
                .ToList()
            };
        }
    }
}
