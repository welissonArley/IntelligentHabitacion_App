using Homuai.App.Services;
using Homuai.App.Services.Communication.Friend;
using Refit;
using System;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.Friends.RemoveFriend
{
    public class RequestCodeToRemoveFriendUseCase : UseCaseBase, IRequestCodeToRemoveFriendUseCase
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;
        private readonly IFriendService _restService;

        public RequestCodeToRemoveFriendUseCase(Lazy<UserPreferences> userPreferences) : base("Friend")
        {
            this.userPreferences = userPreferences;
            _restService = RestService.For<IFriendService>(BaseAddress());
        }

        public async Task Execute()
        {
            var response = await _restService.RequestCodeToRemoveFriend(await _userPreferences.GetToken(), GetLanguage());

            ResponseValidate(response);

            await _userPreferences.ChangeToken(GetTokenOnHeaderRequest(response.Headers));
        }
    }
}
