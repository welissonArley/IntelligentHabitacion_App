using Homuai.Application.Services.Cryptography;
using Homuai.Communication.Request;
using Homuai.Domain.Repository;
using Homuai.Domain.Repository.Code;
using Homuai.Domain.Repository.User;
using Homuai.Exception.ExceptionsBase;
using System.Linq;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.Login.ForgotPassword
{
    public class ResetPasswordUseCase : IResetPasswordUseCase
    {
        private readonly PasswordEncripter _cryptography;
        private readonly IUserUpdateOnlyRepository _repository;
        private readonly ICodeReadOnlyRepository _codeRepository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly ICodeWriteOnlyRepository _codeWriteOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ResetPasswordUseCase(PasswordEncripter cryptography, IUserUpdateOnlyRepository repository,
            ICodeReadOnlyRepository codeRepository, IUserReadOnlyRepository userReadOnlyRepository,
            ICodeWriteOnlyRepository codeWriteOnlyRepository, IUnitOfWork unitOfWork)
        {
            _cryptography = cryptography;
            _repository = repository;
            _codeRepository = codeRepository;
            _userReadOnlyRepository = userReadOnlyRepository;
            _codeWriteOnlyRepository = codeWriteOnlyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(RequestResetYourPasswordJson resetYourPasswordJson)
        {
            await Validate(resetYourPasswordJson);

            var user = await _repository.GetByEmail_Update(resetYourPasswordJson.Email);
            user.Password = _cryptography.Encrypt(resetYourPasswordJson.Password);

            _repository.Update(user);
            _codeWriteOnlyRepository.DeleteAllFromTheUser(user.Id);

            await _unitOfWork.Commit();
        }

        private async Task Validate(RequestResetYourPasswordJson resetYourPasswordJson)
        {
            var validation = await new ForgotPasswordValidation(_codeRepository, _userReadOnlyRepository).ValidateAsync(resetYourPasswordJson);

            if (!validation.IsValid)
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}
