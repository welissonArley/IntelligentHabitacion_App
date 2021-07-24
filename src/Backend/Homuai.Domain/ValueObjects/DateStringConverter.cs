using System;

namespace Homuai.Domain.ValueObjects
{
    public class DateStringConverter
    {
        public string DateToStringYearMonthAndDay(DateTime date)
        {
            var now = DateTime.UtcNow;

            TimeSpan span = now > date ? now - date : date - now;
            var differenceDate = (new DateTime(1, 1, 1) + span);

            string response = DateToStringYear(differenceDate).Trim();
            response = $"{response}{(response.Length == 0 ? "" : ",")} {DateToStringMonth(differenceDate)}".Trim();
            response = $"{response}{(response.Length == 0 ? "" : $" {ResourceText.TITLE_AND}")} {DateToStringDay(differenceDate)}".Trim();
            response = $"{(response.Length > 0 ? response : $"1 {ResourceText.TITLE_DAY}")}".Trim();
            return response;
        }

        private static string DateToStringYear(DateTime date)
        {
            var years = date.Year - 1;

            if (years <= 0)
                return "";

            return $" {years} {(years == 1 ? $"{ResourceText.TITLE_YEAR}" : $"{ResourceText.TITLE_YEARS}")}".Trim();
        }

        private static string DateToStringMonth(DateTime date)
        {
            var months = date.Month - 1;
            if (months <= 0)
                return "";

            return $" {months} {(months == 1 ? $"{ResourceText.TITLE_MONTH}" : $"{ResourceText.TITLE_MONTHS}")}";
        }

        private static string DateToStringDay(DateTime date)
        {
            var days = date.Day - 1;
            if (days <= 0)
                return "";

            return $" {days} {(days == 1 ? $"{ResourceText.TITLE_DAY}" : $"{ResourceText.TITLE_DAYS}")}";
        }
    }
}
