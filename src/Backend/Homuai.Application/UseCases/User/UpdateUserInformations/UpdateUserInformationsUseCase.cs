using AutoMapper;
using Homuai.Application.Services.LoggedUser;
using Homuai.Application.UseCases.User.EmailAlreadyBeenRegistered;
using Homuai.Communication.Request;
using Homuai.Domain.Entity;
using Homuai.Domain.Repository;
using Homuai.Domain.Repository.User;
using Homuai.Exception;
using Homuai.Exception.ExceptionsBase;
using System.Linq;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.User.UpdateUserInformations
{
    public class UpdateUserInformationsUseCase : IUpdateUserInformationsUseCase
    {
        private readonly HomuaiUseCase _homuaiUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailAlreadyBeenRegisteredUseCase _registeredUseCase;
        private readonly IMapper _mapper;
        private readonly IUserUpdateOnlyRepository _repository;

        public UpdateUserInformationsUseCase(ILoggedUser loggedUser, IMapper mapper,
            IUserUpdateOnlyRepository repository, IUnitOfWork unitOfWork,
            IEmailAlreadyBeenRegisteredUseCase registeredUseCase, HomuaiUseCase homuaiUseCase)
        {
            _loggedUser = loggedUser;
            _homuaiUseCase = homuaiUseCase;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
            _registeredUseCase = registeredUseCase;
        }

        public async Task<ResponseOutput> Execute(RequestUpdateUserJson updateUserJson)
        {
            var loggedUser = await _loggedUser.User();

            await Validate(updateUserJson, loggedUser);

            var userToUpdate = await _repository.GetById_Update(loggedUser.Id);

            userToUpdate.Name = updateUserJson.Name;
            userToUpdate.Email = updateUserJson.Email;
            userToUpdate.Phonenumbers.Clear();
            userToUpdate.EmergencyContacts.Clear();

            userToUpdate.Phonenumbers = updateUserJson.Phonenumbers.Select(c => _mapper.Map<Phonenumber>(c)).ToList();
            userToUpdate.EmergencyContacts = updateUserJson.EmergencyContacts.Select(c => _mapper.Map<EmergencyContact>(c)).ToList();

            _repository.Update(userToUpdate);
            var response = await _homuaiUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            await _unitOfWork.Commit();

            return response;
        }

        private async Task Validate(RequestUpdateUserJson updateUserJson, Domain.Entity.User userDataNow)
        {
            var validation = new UpdateUserInformationsValidation().Validate(updateUserJson);

            if (!userDataNow.Email.Equals(updateUserJson.Email) && (await _registeredUseCase.Execute(updateUserJson.Email)).Value)
                validation.Errors.Add(new FluentValidation.Results.ValidationFailure("", ResourceTextException.EMAIL_ALREADYBEENREGISTERED));

            if (!validation.IsValid)
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}
