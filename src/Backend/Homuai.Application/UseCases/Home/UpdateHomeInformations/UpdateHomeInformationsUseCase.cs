using AutoMapper;
using Homuai.Application.Services.LoggedUser;
using Homuai.Communication.Request;
using Homuai.Domain.Repository;
using Homuai.Domain.Repository.Home;
using Homuai.Exception.ExceptionsBase;
using System.Linq;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.Home.UpdateHomeInformations
{
    public class UpdateHomeInformationsUseCase : IUpdateHomeInformationsUseCase
    {
        private readonly HomuaiUseCase _homuaiUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHomeUpdateOnlyRepository _repository;

        public UpdateHomeInformationsUseCase(ILoggedUser loggedUser, IMapper mapper, IUnitOfWork unitOfWork,
            HomuaiUseCase homuaiUseCase, IHomeUpdateOnlyRepository repository)
        {
            _loggedUser = loggedUser;
            _homuaiUseCase = homuaiUseCase;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ResponseOutput> Execute(RequestUpdateHomeJson updateHomeJson)
        {
            Validate(updateHomeJson);

            var loggedUser = await _loggedUser.User();
            
            var homeModel = await _repository.GetById_Update(loggedUser.HomeAssociation.HomeId);
            _mapper.Map(updateHomeJson, homeModel);

            _repository.Update(homeModel);
            
            var response = await _homuaiUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            await _unitOfWork.Commit();

            return response;
        }

        private void Validate(RequestUpdateHomeJson updateHomeJson)
        {
            var validation = new UpdateHomeInformationValidation().Validate(updateHomeJson);

            if (!validation.IsValid)
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}
