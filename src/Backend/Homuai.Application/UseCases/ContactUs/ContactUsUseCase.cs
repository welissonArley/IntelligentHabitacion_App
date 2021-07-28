using Homuai.Application.Services.LoggedUser;
using Homuai.Communication.Request;
using Homuai.Domain.Repository;
using Homuai.Domain.Services.SendEmail;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.ContactUs
{
    public class ContactUsUseCase : IContactUsUseCase
    {
        private readonly HomuaiUseCase _homuaiUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISendContactUsEmail _emailHelper;

        public ContactUsUseCase(ILoggedUser loggedUser, ISendContactUsEmail emailHelper,
            HomuaiUseCase homuaiUseCase, IUnitOfWork unitOfWork)
        {
            _loggedUser = loggedUser;
            _homuaiUseCase = homuaiUseCase;
            _unitOfWork = unitOfWork;
            _emailHelper = emailHelper;
        }

        public async Task<ResponseOutput> Execute(RequestContactUsJson request)
        {
            var loggedUser = await _loggedUser.User();
            
            if (!string.IsNullOrWhiteSpace(request.Message))
            {
                await _emailHelper.Send(new Domain.Dto.ContactUsDto
                {
                    Message = request.Message,
                    Email = loggedUser.Email,
                    UserName = loggedUser.Name
                });
            }

            var response = await _homuaiUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            await _unitOfWork.Commit();

            return response;
        }
    }
}
