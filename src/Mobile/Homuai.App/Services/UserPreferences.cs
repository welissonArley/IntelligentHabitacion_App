using Homuai.App.ValueObjects.Dtos;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Homuai.App.Services
{
    public class UserPreferences
    {
        private readonly string _keyEmail = "IHeml";
        private readonly string _keyPassword = "IHpawd";
        private readonly string _keyToken = "IHtkn";
        private readonly string _keyId = "IHidy";

        public string Name
        {
            get => Preferences.Get("NAME", null);
            private set => Preferences.Set("NAME", value);
        }
        public bool IsPartOfOneHome
        {
            get => Preferences.Get("ISPARTOFONEHOME", false);
            private set => Preferences.Set("ISPARTOFONEHOME", value);
        }
        public bool IsAdministrator
        {
            get => Preferences.Get("ISADMINISTRATOR", false);
            private set
            {
                Preferences.Set("ISADMINISTRATOR", value);
                IsPartOfOneHome = true;
            }
        }
        public double Width
        {
            get => Preferences.Get("WIDTH", 0.0);
            private set => Preferences.Set("WIDTH", value);
        }
        public bool HasOrder
        {
            get => Preferences.Get("HASORDER", false);
            private set => Preferences.Set("HASORDER", value);
        }

        #region functions
        public async Task SaveInitialUserInfos(UserPreferenceDto userPreference)
        {
            Name = userPreference.Name;
            IsAdministrator = userPreference.IsAdministrator;
            IsPartOfOneHome = userPreference.IsPartOfOneHome;
            Width = userPreference.Width;

            Preferences.Set("PROFILECOLOR_LightMode", userPreference.ProfileColorLightMode);
            Preferences.Set("PROFILECOLOR_DarkMode", userPreference.ProfileColorDarkMode);

            await SecureStorage.SetAsync(_keyId, userPreference.Id);
            await SecureStorage.SetAsync(_keyEmail, userPreference.Email);
            await ChangePassword(userPreference.Password);
            await ChangeToken(userPreference.Token);
        }
        public async Task SaveUserInformations(string name, string email)
        {
            Name = name;
            await SecureStorage.SetAsync(_keyEmail, email);
        }
        public async Task ChangeToken(string token)
        {
            await SecureStorage.SetAsync(_keyToken, token);
        }
        public async Task<string> GetToken()
        {
            return await SecureStorage.GetAsync(_keyToken);
        }
        public async Task<string> GetMyId()
        {
            return await SecureStorage.GetAsync(_keyId);
        }
        public async Task ChangePassword(string password)
        {
            await SecureStorage.SetAsync(_keyPassword, password);
        }
        public async Task<(string Email, string Password)> GetInfoToLogin()
        {
            var email = await SecureStorage.GetAsync(_keyEmail);
            var password = await SecureStorage.GetAsync(_keyPassword);

            return (email, password);
        }
        public void UserIsAdministrator(bool isAdmin)
        {
            IsAdministrator = isAdmin;
        }
        public void UserHasOrder(bool hasOrder)
        {
            HasOrder = hasOrder;
        }
        public void UserIsPartOfOneHome(bool isPartOfOneHome)
        {
            IsPartOfOneHome = isPartOfOneHome;
        }
        public string ProfileColor()
        {
            if (Application.Current.RequestedTheme == OSAppTheme.Dark)
                return Preferences.Get("PROFILECOLOR_DarkMode", null);

            return Preferences.Get("PROFILECOLOR_LightMode", null);
        }

        public async Task<bool> AlreadySignedIn()
        {
            var result = await GetInfoToLogin();

            return !string.IsNullOrEmpty(result.Email) && !string.IsNullOrEmpty(result.Password);
        }
        public async Task<bool> HasAccessToken()
        {
            return !string.IsNullOrWhiteSpace(await SecureStorage.GetAsync(_keyToken));
        }
        public void Logout()
        {
            Preferences.Clear();
            SecureStorage.Remove(_keyToken);
            SecureStorage.Remove(_keyId);
        }
        #endregion
    }
}
