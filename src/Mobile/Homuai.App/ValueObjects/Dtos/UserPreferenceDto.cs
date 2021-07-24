namespace Homuai.App.ValueObjects.Dtos
{
    public class UserPreferenceDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfileColorLightMode { get; set; }
        public string ProfileColorDarkMode { get; set; }
        public bool IsAdministrator { get; set; }
        public bool IsPartOfOneHome { get; set; }
        public string Token { get; set; }
        public double Width { get; set; }
    }
}
