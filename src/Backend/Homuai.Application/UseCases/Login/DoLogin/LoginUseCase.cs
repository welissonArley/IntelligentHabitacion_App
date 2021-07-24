using AutoMapper;
using Homuai.Application.Services.Cryptography;
using Homuai.Communication.Request;
using Homuai.Communication.Response;
using Homuai.Domain.Repository;
using Homuai.Domain.Repository.User;
using Homuai.Exception.ExceptionsBase;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.Login.DoLogin
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly HomuaiUseCase _homuaiUseCase;
        private readonly PasswordEncripter _cryptography;
        private readonly IUserReadOnlyRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public LoginUseCase(IUserReadOnlyRepository repository, PasswordEncripter cryptography,
            HomuaiUseCase homuaiUseCase, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _cryptography = cryptography;
            _repository = repository;
            _homuaiUseCase = homuaiUseCase;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseOutput> Execute(RequestLoginJson loginJson)
        {
            var user = await _repository.GetByEmailPassword(loginJson.User, _cryptography.Encrypt(loginJson.Password));

            if (user == null)
                throw new InvalidLoginException();

            var json = _mapper.Map<ResponseLoginJson>(user);
            var response = await _homuaiUseCase.CreateResponse(user.Email, user.Id, json);
            await _unitOfWork.Commit();

            return response;
        }
    }
}
