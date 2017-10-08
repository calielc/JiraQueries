using System;

namespace JiraQueries.Bussiness {
    public static class DateTimeExtensions {
        public static DateTime SetDay(this DateTime self, int day) => new DateTime(self.Year, self.Month, day);
    }
}