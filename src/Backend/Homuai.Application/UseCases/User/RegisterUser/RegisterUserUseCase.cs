using AutoMapper;
using Homuai.Application.Services.Cryptography;
using Homuai.Communication.Request;
using Homuai.Communication.Response;
using Homuai.Domain.Repository;
using Homuai.Domain.Repository.User;
using Homuai.Domain.ValueObjects;
using Homuai.Exception.ExceptionsBase;
using System.Linq;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.User.RegisterUser
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserReadOnlyRepository _repositoryUserReadOnly;
        private readonly IUserWriteOnlyRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly HomuaiUseCase _homuaiUseCase;
        private readonly PasswordEncripter _cryptography;

        public RegisterUserUseCase(IMapper mapper, IUnitOfWork unitOfWork,
            HomuaiUseCase homuaiUseCase, IUserWriteOnlyRepository repository,
            IUserReadOnlyRepository repositoryUserReadOnly, PasswordEncripter cryptography)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _homuaiUseCase = homuaiUseCase;
            _repository = repository;
            _repositoryUserReadOnly = repositoryUserReadOnly;
            _cryptography = cryptography;
        }

        public async Task<ResponseOutput> Execute(RequestRegisterUserJson registerUserJson)
        {
            await ValidateRequest(registerUserJson);

            var colors = new Color().RandomColor();

            var userModel = _mapper.Map<Domain.Entity.User>(registerUserJson);
            userModel.Password = _cryptography.Encrypt(userModel.Password);
            userModel.ProfileColorLightMode = colors.colorLightMode;
            userModel.ProfileColorDarkMode = colors.colorDarkMode;

            await _repository.Add(userModel);
            await _unitOfWork.Commit();

            var json = _mapper.Map<ResponseUserRegisteredJson>(userModel);

            var response = await _homuaiUseCase.CreateResponse(userModel.Email, userModel.Id, json);

            await _unitOfWork.Commit();

            return response;
        }

        private async Task ValidateRequest(RequestRegisterUserJson registerUserJson)
        {
            var validation = await new RegisterUserValidation(_repositoryUserReadOnly).ValidateAsync(registerUserJson);

            if (!validation.IsValid)
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}
