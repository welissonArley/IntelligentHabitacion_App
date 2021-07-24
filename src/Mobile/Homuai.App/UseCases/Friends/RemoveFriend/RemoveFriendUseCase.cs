using Homuai.App.Services;
using Homuai.App.Services.Communication.Friend;
using Refit;
using System;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.Friends.RemoveFriend
{
    public class RemoveFriendUseCase : UseCaseBase, IRemoveFriendUseCase
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;
        private readonly IFriendService _restService;

        public RemoveFriendUseCase(Lazy<UserPreferences> userPreferences) : base("Friend")
        {
            this.userPreferences = userPreferences;
            _restService = RestService.For<IFriendService>(BaseAddress());
        }

        public async Task Execute(string friendId, string code, string password)
        {
            var response = await _restService.RemoveFriend(friendId, new Communication.Request.RequestAdminActionJson
            {
                Code = code,
                Password = password
            }, await _userPreferences.GetToken(), GetLanguage());

            ResponseValidate(response);

            await _userPreferences.ChangeToken(GetTokenOnHeaderRequest(response.Headers));
        }
    }
}
