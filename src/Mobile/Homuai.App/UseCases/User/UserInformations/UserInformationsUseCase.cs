using Homuai.App.Model;
using Homuai.App.Services;
using Homuai.App.Services.Communication.User;
using Homuai.Communication.Response;
using Refit;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.User.UserInformations
{
    public class UserInformationsUseCase : UseCaseBase, IUserInformationsUseCase
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;
        private readonly IUserRestService _restService;

        public UserInformationsUseCase(Lazy<UserPreferences> userPreferences) : base("User")
        {
            this.userPreferences = userPreferences;
            _restService = RestService.For<IUserRestService>(BaseAddress());
        }

        public async Task<UserInformationsModel> Execute()
        {
            var response = await _restService.GetUserInformations(await _userPreferences.GetToken(), GetLanguage());

            ResponseValidate(response);

            await _userPreferences.ChangeToken(GetTokenOnHeaderRequest(response.Headers));

            return Mapper(response.Content);
        }

        private UserInformationsModel Mapper(ResponseUserInformationsJson response)
        {
            var model = new UserInformationsModel
            {
                Name = response.Name,
                Email = response.Email,
                PhoneNumber1 = response.Phonenumbers.First().Number
            };

            if (response.Phonenumbers.Count > 1)
                model.PhoneNumber2 = response.Phonenumbers[1].Number;

            var emergencyContact = response.EmergencyContacts.First();
            model.EmergencyContact1 = new EmergencyContactModel
            {
                Name = emergencyContact.Name,
                Relationship = emergencyContact.Relationship,
                PhoneNumber = emergencyContact.Phonenumber
            };

            if (response.EmergencyContacts.Count > 1)
            {
                emergencyContact = response.EmergencyContacts[1];
                model.EmergencyContact2 = new EmergencyContactModel
                {
                    Name = emergencyContact.Name,
                    Relationship = emergencyContact.Relationship,
                    PhoneNumber = emergencyContact.Phonenumber
                };
            }

            return model;
        }
    }
}
