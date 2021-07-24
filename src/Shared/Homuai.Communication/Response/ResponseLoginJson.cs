namespace Homuai.Communication.Response
{
    public class ResponseLoginJson
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsPartOfOneHome { get; set; }
        public bool IsAdministrator { get; set; }
        public string ProfileColorLightMode { get; set; }
        public string ProfileColorDarkMode { get; set; }
    }
}
