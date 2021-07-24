using Homuai.Domain.Services;
using Homuai.Domain.ValueObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Homuai.Infrastructure.Services
{
    public class OneSignalService : HttpClient, IPushNotificationService
    {
        private readonly string _key;
        private readonly string _appId;
        private readonly string _url;

        public OneSignalService(OneSignalConfig config)
        {
            _key = config.Key;
            _appId = config.AppId;
            _url = config.Url;
        }

        public async Task Send(Dictionary<string, string> titleForEachLanguage, Dictionary<string, string> messageForEachLanguage, List<string> usersIds, Dictionary<string, string> data)
        {
            var bodyMensage = JsonConvert.SerializeObject(new
            {
                app_id = _appId,
                include_player_ids = usersIds,
                contents = messageForEachLanguage,
                headings = titleForEachLanguage,
                data
            });

            await SendRequest(bodyMensage);
        }

        public async Task Send(Dictionary<string, string> titleForEachLanguage, Dictionary<string, string> messageForEachLanguage, List<string> usersIds, DateTime? time = null)
        {
            string bodyMessage = JsonConvert.SerializeObject(new
            {
                app_id = _appId,
                include_player_ids = usersIds,
                contents = messageForEachLanguage,
                headings = titleForEachLanguage
            });

            if (time.HasValue)
            {
                bodyMessage = JsonConvert.SerializeObject(new
                {
                    app_id = _appId,
                    include_player_ids = usersIds,
                    contents = messageForEachLanguage,
                    headings = titleForEachLanguage,
                    delayed_option = "timezone",
                    delivery_time_of_day = time.Value.ToString("HH:mm")
                });
            }

            await SendRequest(bodyMessage);
        }

        private async Task SendRequest(string body)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _url);

            request.Headers.Add("Authorization", "Basic " + _key);

            request.Content = new StringContent(body, Encoding.UTF8, "application/json");

            await SendAsync(request);
        }
    }
}
