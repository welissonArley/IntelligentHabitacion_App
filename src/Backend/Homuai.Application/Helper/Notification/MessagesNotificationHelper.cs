using Homuai.Domain;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Homuai.Application.Helper.Notification
{
    public class MessagesNotificationHelper
    {
        private readonly List<string> _languages;

        public MessagesNotificationHelper()
        {
            _languages = new List<string> { "en", "pt" };
        }

        public (Dictionary<string, string> title, Dictionary<string, string> messages) Messages(NotificationHelperType notification, string[] data = null)
        {
            var titles = new Dictionary<string, string>();
            var messages = new Dictionary<string, string>();

            switch (notification)
            {
                case NotificationHelperType.DeliveryReceived:
                    {
                        titles = GetValuePairs("NOTIFICATION_TITLE_DELIVERY_RECEIVED");
                        messages = GetValuePairs("NOTIFICATION_MESSAGE_DELIVERY_RECEIVED");
                    }
                    break;
                case NotificationHelperType.CleaningScheduleCreated:
                    {
                        titles = GetValuePairs("NOTIFICATION_TITLE_CLEANING_SCHEDULE_CREATED");
                        messages = GetValuePairs("NOTIFICATION_MESSAGE_CLEANING_SCHEDULE_CREATED");
                    }
                    break;
                case NotificationHelperType.CleaningScheduleUpdated:
                    {
                        titles = GetValuePairs("NOTIFICATION_TITLE_CLEANING_SCHEDULE_UPDATED");
                        messages = GetValuePairs("NOTIFICATION_MESSAGE_CLEANING_SCHEDULE_UPDATED");
                    }
                    break;
                case NotificationHelperType.RatedTask:
                    {
                        titles = GetValuePairs("NOTIFICATION_TITLE_CLEANING_TASK_RATED");
                        messages = GetValuePairs("NOTIFICATION_MESSAGE_CLEANING_TASK_RATED", data);
                    }
                    break;
                case NotificationHelperType.RemovedFromHome:
                    {
                        titles = GetValuePairs("NOTIFICATION_TITLE_REMOVED_FROM_HOME");
                        messages = GetValuePairs("NOTIFICATION_MESSAGE_REMOVED_FROM_HOME");
                    }
                    break;
                case NotificationHelperType.CleaningScheduleReminder:
                    {
                        titles = GetValuePairs("NOTIFICATION_TITLE_CLEANING_SCHEDULE_REMINDER");
                        messages = GetValuePairs("NOTIFICATION_MESSAGE_CLEANING_SCHEDULE_REMINDER");
                    }
                    break;
                case NotificationHelperType.RoomCleaned:
                    {
                        titles = GetValuePairs("NOTIFICATION_TITLE_CLEAN_ROOM_CLEANED");
                        messages = GetValuePairs("NOTIFICATION_MESSAGE_ROOM_CLEANED", data);
                    }
                    break;
                case NotificationHelperType.ReminderTotalDaysWithoutCleanRoom:
                    {
                        titles = GetValuePairs("NOTIFICATION_TITLE_CLEANING_SCHEDULE_REMINDER");
                        messages = GetValuePairs("NOTIFICATION_MESSAGE_TOTAL_DAYS_WITHOUT_CLEANING_ROOM", data);
                    }
                    break;
                case NotificationHelperType.SevenDaysProductExpiration:
                    {
                        titles = GetValuePairs("NOTIFICATION_TITLE_7_DAYS_PRODUCT_EXPIRATION");
                        messages = GetValuePairs("NOTIFICATION_MESSAGE_7_DAYS_PRODUCT_EXPIRATION", data);
                    }
                    break;
                case NotificationHelperType.ThreeDaysProductExpiration:
                    {
                        titles = GetValuePairs("NOTIFICATION_TITLE_3_DAYS_PRODUCT_EXPIRATION");
                        messages = GetValuePairs("NOTIFICATION_MESSAGE_3_DAYS_PRODUCT_EXPIRATION", data);
                    }
                    break;
                case NotificationHelperType.TomorrowDayProductExpiration:
                    {
                        titles = GetValuePairs("NOTIFICATION_TITLE_TOMORROW_PRODUCT_EXPIRATION");
                        messages = GetValuePairs("NOTIFICATION_MESSAGE_TOMORROW_PRODUCT_EXPIRATION", data);
                    }
                    break;
                case NotificationHelperType.TodayProductExpiration:
                    {
                        titles = GetValuePairs("NOTIFICATION_TITLE_TODAY_PRODUCT_EXPIRATION");
                        messages = GetValuePairs("NOTIFICATION_MESSAGE_TODAY_PRODUCT_EXPIRATION", data);
                    }
                    break;
                case NotificationHelperType.YesterdayProductExpiration:
                    {
                        titles = GetValuePairs("NOTIFICATION_TITLE_YESTERDAY_PRODUCT_EXPIRATION");
                        messages = GetValuePairs("NOTIFICATION_MESSAGE_YESTERDAY_PRODUCT_EXPIRATION", data);
                    }
                    break;
                case NotificationHelperType.TwoDaysPassedProductExpiration:
                    {
                        titles = GetValuePairs("NOTIFICATION_TITLE_TWO_DAYS_PASSED_PRODUCT_EXPIRATION");
                        messages = GetValuePairs("NOTIFICATION_MESSAGE_TWO_DAYS_PASSED_PRODUCT_EXPIRATION", data);
                    }
                    break;
                case NotificationHelperType.DeletedProductExpiration:
                    {
                        titles = GetValuePairs("NOTIFICATION_TITLE_DELETED_PRODUCT_EXPIRATION");
                        messages = GetValuePairs("NOTIFICATION_MESSAGE_DELETED_PRODUCT_EXPIRATION", data);
                    }
                    break;
            }

            return (titles, messages);
        }

        private Dictionary<string, string> GetValuePairs(string key, string[] data = null)
        {
            return _languages.Select(x => new KeyValuePair<string, string>(x, GetString(key, new CultureInfo(x))))
                            .ToDictionary(x => x.Key, x => data == null ? x.Value : string.Format(x.Value, data));
        }
        private string GetString(string key, CultureInfo culture)
        {
            return ResourceText.ResourceManager.GetString(key, culture);
        }
    }
}
