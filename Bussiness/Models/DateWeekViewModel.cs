using System;
using System.Globalization;

namespace JiraQueries.Bussiness.Models {
    public struct DateWeekViewModel {
        private static readonly DateTimeFormatInfo CurrentInfo = DateTimeFormatInfo.CurrentInfo;

        public DateWeekViewModel(DateTime dateTime) {
            DayValue = dateTime.DayOfWeek;
            DayText = dateTime.DayOfWeek.ToString();

            YearValue = CurrentInfo.Calendar.GetWeekOfYear(dateTime, CurrentInfo.CalendarWeekRule, CurrentInfo.FirstDayOfWeek);
        }

        public DayOfWeek DayValue { get; }
        public string DayText { get; }

        public int YearValue { get; }
        public string YearText => $"W{YearValue}";

        public static implicit operator DateWeekViewModel(DateTime value) => new DateWeekViewModel(value);
    }
}