using Homuai.Domain.Entity;
using Homuai.Domain.Enums;
using Homuai.Domain.Repository;
using Homuai.Domain.Repository.Code;
using Homuai.Domain.Repository.User;
using Homuai.Domain.Services.SendEmail;
using Homuai.Domain.ValueObjects;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.Login.ForgotPassword
{
    public class RequestCodeResetPasswordUseCase : IRequestCodeResetPasswordUseCase
    {
        private readonly IUserReadOnlyRepository _userRepository;
        private readonly ICodeWriteOnlyRepository _repository;
        private readonly ISendResetPasswordEmail _emailHelper;
        private readonly IUnitOfWork _unitOfWork;

        public RequestCodeResetPasswordUseCase(IUserReadOnlyRepository userRepository, ICodeWriteOnlyRepository repository,
            ISendResetPasswordEmail emailHelper, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _repository = repository;
            _emailHelper = emailHelper;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(string email)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user != null)
            {
                var codeRandom = new CodeGenerator().Random6Chars();

                await _repository.Add(new Code
                {
                    Active = true,
                    Type = CodeType.ResetPassword,
                    Value = codeRandom,
                    UserId = user.Id
                });

                await _emailHelper.Send(new Domain.Dto.ResetPasswordDto
                {
                    UserName = user.Name,
                    Email = user.Email,
                    Code = codeRandom
                });

                await _unitOfWork.Commit();
            }
        }
    }
}
