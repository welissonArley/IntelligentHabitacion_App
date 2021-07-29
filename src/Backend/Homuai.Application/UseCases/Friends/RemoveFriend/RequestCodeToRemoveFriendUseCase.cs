using Homuai.Application.Services.LoggedUser;
using Homuai.Domain.Dto;
using Homuai.Domain.Entity;
using Homuai.Domain.Repository;
using Homuai.Domain.Repository.Code;
using Homuai.Domain.Services.SendEmail;
using Homuai.Domain.ValueObjects;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.Friends.RemoveFriend
{
    public class RequestCodeToRemoveFriendUseCase : IRequestCodeToRemoveFriendUseCase
    {
        private readonly HomuaiUseCase _homuaiUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly ICodeWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISendCodeRemoveFriendEmail _emailHelper;

        public RequestCodeToRemoveFriendUseCase(ILoggedUser loggedUser, ICodeWriteOnlyRepository repository,
            IUnitOfWork unitOfWork, ISendCodeRemoveFriendEmail emailHelper, HomuaiUseCase homuaiUseCase)
        {
            _loggedUser = loggedUser;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _homuaiUseCase = homuaiUseCase;
            _emailHelper = emailHelper;
        }

        public async Task<ResponseOutput> Execute()
        {
            var loggedUser = await _loggedUser.User();

            var codeRandom = new CodeGenerator().Random6Chars();

            await _repository.Add(new Code
            {
                Type = Domain.Enums.CodeType.RemoveFriend,
                Value = codeRandom,
                UserId = loggedUser.Id
            });

            var response = await _homuaiUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            await _unitOfWork.Commit();

            await _emailHelper.Send(new SendCodeToPerformSomeActionDto
            {
               Code = codeRandom,
               Email = loggedUser.Email,
               UserName = loggedUser.Name
            });
            
            return response;
        }
    }
}
