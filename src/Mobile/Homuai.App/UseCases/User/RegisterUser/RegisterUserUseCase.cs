using Homuai.App.Model;
using Homuai.App.Services;
using Homuai.App.Services.Communication.User;
using Homuai.App.ValueObjects.Dtos;
using Homuai.Communication.Request;
using Refit;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Homuai.App.UseCases.User.RegisterUser
{
    public class RegisterUserUseCase : UseCaseBase, IRegisterUserUseCase
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;
        private readonly IUserRestService _restService;

        public RegisterUserUseCase(Lazy<UserPreferences> userPreferences) : base("User")
        {
            this.userPreferences = userPreferences;
            _restService = RestService.For<IUserRestService>(BaseAddress());
        }

        public async Task Execute(RegisterUserModel userInformations)
        {
            var response = await _restService.CreateUser(Mapper(userInformations), GetLanguage());

            ResponseValidate(response);

            await _userPreferences.SaveInitialUserInfos(new UserPreferenceDto
            {
                IsAdministrator = false,
                IsPartOfOneHome = false,
                Id = response.Content.Id,
                Token = GetTokenOnHeaderRequest(response.Headers),
                ProfileColorDarkMode = response.Content.ProfileColorDarkMode,
                ProfileColorLightMode = response.Content.ProfileColorLightMode,
                Name = userInformations.Name,
                Password = userInformations.Password,
                Email = userInformations.Email,
                Width = Application.Current.MainPage.Width
            });
        }

        private RequestRegisterUserJson Mapper(RegisterUserModel userInformations)
        {
            var user = new RequestRegisterUserJson
            {
                Name = userInformations.Name,
                Email = userInformations.Email,
                Password = userInformations.Password,
                PushNotificationId = Services.Communication.Notifications.MyOneSignalId
            };

            user.Phonenumbers.Add(userInformations.PhoneNumber1);

            if (!string.IsNullOrWhiteSpace(userInformations.PhoneNumber2))
                user.Phonenumbers.Add(userInformations.PhoneNumber2);

            user.EmergencyContacts.Add(new RequestEmergencyContactJson
            {
                Name = userInformations.EmergencyContact1.Name,
                Relationship = userInformations.EmergencyContact1.Relationship,
                Phonenumber = userInformations.EmergencyContact1.PhoneNumber
            });

            if (!string.IsNullOrWhiteSpace(userInformations.EmergencyContact2.Name))
            {
                user.EmergencyContacts.Add(new RequestEmergencyContactJson
                {
                    Name = userInformations.EmergencyContact2.Name,
                    Relationship = userInformations.EmergencyContact2.Relationship,
                    Phonenumber = userInformations.EmergencyContact2.PhoneNumber
                });
            }

            return user;
        }
    }
}
