using System;
using Xamarin.Essentials;

namespace Homuai.App.Services
{
    public class PhoneCall
    {
        public static void MakeCall(string number)
        {
            Launcher.OpenAsync(new Uri($"tel:{number.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "")}"));
        }
    }
}
