using Homuai.Domain.Entity;
using Homuai.Domain.Enums;
using Homuai.Domain.Repository;
using Homuai.Domain.Repository.Code;
using Homuai.Domain.Repository.User;
using Homuai.Domain.Services;
using Homuai.Domain.ValueObjects;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.Login.ForgotPassword
{
    public class RequestCodeResetPasswordUseCase : IRequestCodeResetPasswordUseCase
    {
        private readonly IUserReadOnlyRepository _userRepository;
        private readonly ICodeWriteOnlyRepository _repository;
        private readonly ISendEmail _emailHelper;
        private readonly IUnitOfWork _unitOfWork;

        public RequestCodeResetPasswordUseCase(IUserReadOnlyRepository userRepository, ICodeWriteOnlyRepository repository,
            ISendEmail emailHelper, IUnitOfWork unitOfWork)
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

                await _emailHelper.Send(new EmailContent
                {
                    SendToEmail = user.Email,
                    Subject = "Recuperar Senha",
                    HtmlText = BodyHtmlText(user.Name, codeRandom),
                    PlainText = BodyPlainText(user.Name, codeRandom)
                });

                await _unitOfWork.Commit();
            }
        }

        private string BodyPlainText(string userName, string code)
        {
            var plainText = $"Olá {userName}, Precisa resetar sua senha para acessar sua conta, certo? Use o código abaixo para prosseguir com a ação:\n\n\n";
            plainText = $"{plainText}{code}\n\n\n";
            plainText = $"{plainText}Mas lembre-se, não deixe pra depois pois este código será valido por apenas 1 hora combinado?\n\n\n";
            plainText = $"{plainText}Obrigado,\nHomuai Admin Team.";

            return plainText;
        }
        private string BodyHtmlText(string userName, string code)
        {
            var htmlText = $@"<div style=""margin-top: 50px;"">
			    <span style=""font-family: 'Raleway';font-size: 14px;"">Olá {userName},</span>
			    <span style=""font-family: 'Raleway';font-size: 14px;display: block;margin-top: 14px;"">Precisa resgatar sua senha para acessar sua conta certo? Use o código abaixo para prosseguir com a ação:</span>
			
			    <div style=""margin-top: 50px;"">
				    <span style=""color: #FEBF3B;font-family: 'Raleway';font-size: 30px;font-weight: 800;"">{code}</span>
			    </div>
			
			    <div style=""margin-top: 50px;"">
				    <span style=""font-family: 'Raleway';font-size: 14px;"">Mas lembre-se, não deixe pra depois, pois este código será valido por apenas 1 hora, combinado?</span>
			    </div>
		    </div>";

                htmlText = $@"{htmlText}<div style=""margin-top: 100px;"">
			    <span style=""font-family: 'Raleway';font-size: 14px;"">Obrigado,</span>
			    <span style=""font-family: 'Raleway';font-size: 14px;display: block;margin-top: 14px;"">Homuai Admin Team.</span>
		    </div>";

            return htmlText;
        }
    }
}
