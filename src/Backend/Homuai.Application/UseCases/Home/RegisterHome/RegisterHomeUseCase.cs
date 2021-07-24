using AutoMapper;
using Homuai.Application.Services.LoggedUser;
using Homuai.Communication.Request;
using Homuai.Domain.Repository;
using Homuai.Domain.Repository.Home;
using Homuai.Exception.Exceptions;
using Homuai.Exception.ExceptionsBase;
using System.Linq;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.Home.RegisterHome
{
    public class RegisterHomeUseCase : IRegisterHomeUseCase
    {
        private readonly HomuaiUseCase _homuaiUseCase;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        private readonly IHomeWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterHomeUseCase(IHomeWriteOnlyRepository repository, IUnitOfWork unitOfWork,
            ILoggedUser loggedUser, IMapper mapper, HomuaiUseCase homuaiUseCase)
        {
            _mapper = mapper;
            _loggedUser = loggedUser;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _homuaiUseCase = homuaiUseCase;
        }

        public async Task<ResponseOutput> Execute(RequestRegisterHomeJson registerHomeJson)
        {
            var loggedUser = await _loggedUser.User();
            Validate(loggedUser, registerHomeJson);

            var homeModel = _mapper.Map<Domain.Entity.Home>(registerHomeJson);
            homeModel.AdministratorId = loggedUser.Id;

            await _repository.Add(loggedUser, homeModel);

            var response = await _homuaiUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            await _unitOfWork.Commit();

            return response;
        }

        private void Validate(Domain.Entity.User loggedUser, RequestRegisterHomeJson registerHomeJson)
        {
            if (loggedUser.IsPartOfHome())
                throw new UserIsPartOfAHomeException();

            var validation = new RegisterHomeValidation().Validate(registerHomeJson);

            if (!validation.IsValid)
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}
