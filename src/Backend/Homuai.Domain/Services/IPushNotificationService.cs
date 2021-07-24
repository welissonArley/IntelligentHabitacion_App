using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homuai.Domain.Services
{
    public interface IPushNotificationService
    {
        Task Send(Dictionary<string, string> titleForEachLanguage, Dictionary<string, string> messageForEachLanguage, List<string> usersIds, DateTime? time = null);
        Task Send(Dictionary<string, string> titleForEachLanguage, Dictionary<string, string> messageForEachLanguage, List<string> usersIds, Dictionary<string, string> data);
    }
}
