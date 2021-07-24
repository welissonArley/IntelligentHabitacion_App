namespace Homuai.App.Services.Communication
{
    public static class Notifications
    {
        public static string OneSignalKey { get { return ""; } }
        public static string MyOneSignalId { private set; get; }

        public static void SetMyIdOneSignal(string myId)
        {
            MyOneSignalId = myId;
        }
    }
}
