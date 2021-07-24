using Homuai.App.Services;
using Homuai.App.Services.Communication.Friend;
using Refit;
using System;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.Friends.NotifyOrderReceived
{
    public class NotifyOrderReceivedUseCase : UseCaseBase, INotifyOrderReceivedUseCase
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;
        private readonly IFriendService _restService;

        public NotifyOrderReceivedUseCase(Lazy<UserPreferences> userPreferences) : base("Friend")
        {
            this.userPreferences = userPreferences;
            _restService = RestService.For<IFriendService>(BaseAddress());
        }

        public async Task Execute(string friendId)
        {
            var response = await _restService.NotifyFriendOrderHasArrived(friendId, await _userPreferences.GetToken(), GetLanguage());

            ResponseValidate(response);

            await _userPreferences.ChangeToken(GetTokenOnHeaderRequest(response.Headers));
        }
    }
}
