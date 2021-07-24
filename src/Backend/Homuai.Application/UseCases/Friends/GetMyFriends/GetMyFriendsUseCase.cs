using AutoMapper;
using Homuai.Application.Services.LoggedUser;
using Homuai.Communication.Response;
using Homuai.Domain;
using Homuai.Domain.Repository;
using Homuai.Domain.Repository.User;
using Homuai.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.Friends.GetMyFriends
{
    public class GetMyFriendsUseCase : IGetMyFriendsUseCase
    {
        private readonly IMapper _mapper;
        private readonly HomuaiUseCase _homuaiUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserReadOnlyRepository _repository;

        public GetMyFriendsUseCase(IUserReadOnlyRepository repository, IMapper mapper, ILoggedUser loggedUser,
            IUnitOfWork unitOfWork, HomuaiUseCase homuaiUseCase)
        {
            _repository = repository;
            _mapper = mapper;
            _homuaiUseCase = homuaiUseCase;
            _unitOfWork = unitOfWork;
            _loggedUser = loggedUser;
        }

        public async Task<ResponseOutput> Execute()
        {
            var loggedUser = await _loggedUser.User();

            var friends = await _repository.GetByHome(loggedUser.HomeAssociation.HomeId);

            var resultJson = _mapper.Map<List<ResponseFriendJson>>(friends.Where(c => c.Id != loggedUser.Id));

            var converter = new DateStringConverter();
            foreach (var friendJson in resultJson)
                friendJson.DescriptionDateJoined = string.Format(ResourceText.DESCRIPTION_DATE_JOINED_THE_HOUSE, converter.DateToStringYearMonthAndDay(DateTime.Compare(loggedUser.HomeAssociation.JoinedOn, friendJson.JoinedOn) == 1 ? loggedUser.HomeAssociation.JoinedOn : friendJson.JoinedOn));

            var response = await _homuaiUseCase.CreateResponse(loggedUser.Email, loggedUser.Id, resultJson);

            await _unitOfWork.Commit();

            return response;
        }
    }
}
