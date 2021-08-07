using AutoMapper;
using Homuai.Application.Services.LoggedUser;
using Homuai.Communication.Response;
using Homuai.Domain.Repository;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.Home.HomeInformation
{
    public class HomeInformationUseCase : IHomeInformationUseCase
    {
        private readonly IMapper _mapper;
        private readonly HomuaiUseCase _homuaiUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;

        public HomeInformationUseCase(ILoggedUser loggedUser, IMapper mapper,
            IUnitOfWork unitOfWork, HomuaiUseCase homuaiUseCase)
        {
            _loggedUser = loggedUser;
            _homuaiUseCase = homuaiUseCase;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseOutput> Execute()
        {
            var user = await _loggedUser.User();

            var json = _mapper.Map<ResponseHomeInformationsJson>(user.HomeAssociation.Home);

            var response = await _homuaiUseCase.CreateResponse(user.Email, user.Id, json);

            await _unitOfWork.Commit();

            return response;
        }
    }
}
