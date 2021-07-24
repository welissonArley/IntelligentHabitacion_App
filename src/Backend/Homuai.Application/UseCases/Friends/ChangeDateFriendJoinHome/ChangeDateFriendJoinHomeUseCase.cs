using AutoMapper;
using Homuai.Application.Services.LoggedUser;
using Homuai.Communication.Request;
using Homuai.Communication.Response;
using Homuai.Domain;
using Homuai.Domain.Repository;
using Homuai.Domain.Repository.User;
using Homuai.Domain.ValueObjects;
using Homuai.Exception.Exceptions;
using System;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.Friends.ChangeDateFriendJoinHome
{
    public class ChangeDateFriendJoinHomeUseCase : IChangeDateFriendJoinHomeUseCase
    {
        private readonly IMapper _mapper;
        private readonly HomuaiUseCase _homuaiUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserUpdateOnlyRepository _repository;

        public ChangeDateFriendJoinHomeUseCase(IUserUpdateOnlyRepository repository, IMapper mapper, ILoggedUser loggedUser,
            IUnitOfWork unitOfWork, HomuaiUseCase homuaiUseCase)
        {
            _repository = repository;
            _mapper = mapper;
            _homuaiUseCase = homuaiUseCase;
            _unitOfWork = unitOfWork;
            _loggedUser = loggedUser;
        }

        public async Task<ResponseOutput> Execute(long myFriendId, RequestDateJson request)
        {
            var loggedUser = await _loggedUser.User();
            var friend = await _repository.GetById_Update(myFriendId);

            Validate(friend, loggedUser, request.Date);

            friend.HomeAssociation.JoinedOn = request.Date.Date;

            _repository.Update(friend);

            var converter = new DateStringConverter();

            var resultJson = _mapper.Map<ResponseFriendJson>(friend);
            resultJson.DescriptionDateJoined = string.Format(ResourceText.DESCRIPTION_DATE_JOINED_THE_HOUSE, converter.DateToStringYearMonthAndDay(loggedUser.HomeAssociation.JoinedOn));

            var response = await _homuaiUseCase.CreateResponse(loggedUser.Email, loggedUser.Id, resultJson);

            await _unitOfWork.Commit();

            return response;
        }

        private void Validate(Domain.Entity.User friend, Domain.Entity.User loggedUser, DateTime newDate)
        {
            if (friend == null)
                throw new FriendNotFoundException();

            if (friend.HomeAssociation.Home.AdministratorId != loggedUser.Id)
                throw new YouCannotPerformThisActionException();

            if (DateTime.Compare(newDate.Date, friend.HomeAssociation.JoinedOn.Date) > 0)
                throw new InvalidDateException(friend.HomeAssociation.JoinedOn);
        }
    }
}
