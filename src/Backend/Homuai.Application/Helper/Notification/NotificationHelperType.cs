namespace Homuai.Application.Helper.Notification
{
    public enum NotificationHelperType
    {
        DeliveryReceived = 0,
        CleaningScheduleCreated = 1,
        CleaningScheduleUpdated = 2,
        RatedTask = 3,
        RemovedFromHome = 4,
        CleaningScheduleReminder = 5,
        RoomCleaned = 6,
        ReminderTotalDaysWithoutCleanRoom = 7,
        SevenDaysProductExpiration = 8,
        ThreeDaysProductExpiration = 9,
        TomorrowDayProductExpiration = 10,
        TodayProductExpiration = 11,
        YesterdayProductExpiration = 12,
        TwoDaysPassedProductExpiration = 13,
        DeletedProductExpiration = 14
    }
}
