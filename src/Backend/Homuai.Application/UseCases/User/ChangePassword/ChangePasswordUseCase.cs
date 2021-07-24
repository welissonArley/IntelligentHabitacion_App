using Homuai.Application.Services.Cryptography;
using Homuai.Application.Services.LoggedUser;
using Homuai.Communication.Request;
using Homuai.Domain.Repository;
using Homuai.Domain.Repository.User;
using Homuai.Exception.ExceptionsBase;
using System.Linq;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.User.ChangePassword
{
    public class ChangePasswordUseCase : IChangePasswordUseCase
    {
        private readonly HomuaiUseCase _homuaiUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserUpdateOnlyRepository _repository;
        private readonly PasswordEncripter _cryptography;

        public ChangePasswordUseCase(ILoggedUser loggedUser,
            IUserUpdateOnlyRepository repository, IUnitOfWork unitOfWork, PasswordEncripter cryptography,
            HomuaiUseCase homuaiUseCase)
        {
            _loggedUser = loggedUser;
            _homuaiUseCase = homuaiUseCase;
            _unitOfWork = unitOfWork;
            _repository = repository;
            _cryptography = cryptography;
        }

        public async Task<ResponseOutput> Execute(RequestChangePasswordJson changePasswordJson)
        {
            var loggedUser = await _loggedUser.User();

            Validate(changePasswordJson, loggedUser);

            var userToUpdate = await _repository.GetById_Update(loggedUser.Id);
            userToUpdate.Password = _cryptography.Encrypt(changePasswordJson.NewPassword);

            _repository.Update(userToUpdate);
            var response = await _homuaiUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            await _unitOfWork.Commit();

            return response;
        }

        private void Validate(RequestChangePasswordJson changePasswordJson, Domain.Entity.User userDataNow)
        {
            var validation = new ChangePasswordValidation(_cryptography, userDataNow).Validate(changePasswordJson);

            if (!validation.IsValid)
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}
