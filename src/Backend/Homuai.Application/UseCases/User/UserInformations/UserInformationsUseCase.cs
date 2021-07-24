using AutoMapper;
using Homuai.Application.Services.LoggedUser;
using Homuai.Communication.Response;
using Homuai.Domain.Repository;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.User.UserInformations
{
    public class UserInformationsUseCase : IUserInformationsUseCase
    {
        private readonly IMapper _mapper;
        private readonly HomuaiUseCase _homeuaiUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;

        public UserInformationsUseCase(ILoggedUser loggedUser, IMapper mapper,
            IUnitOfWork unitOfWork, HomuaiUseCase homuaiUseCase)
        {
            _loggedUser = loggedUser;
            _homeuaiUseCase = homuaiUseCase;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseOutput> Execute()
        {
            var user = await _loggedUser.User();

            var json = _mapper.Map<ResponseUserInformationsJson>(user);

            var response = await _homeuaiUseCase.CreateResponse(user.Email, user.Id, json);

            await _unitOfWork.Commit();

            return response;
        }
    }
}
