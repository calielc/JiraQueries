using System;

namespace JiraQueries.Bussiness.Models {
    public sealed class DateTimeViewModel {
        private DateTimeViewModel(DateTime dateTime) {
            Value = dateTime;
        }

        public DateTime Value { get; }

        public DateTime Date => Value.Date;

        public int Day => Value.Day;
        public DateWeekViewModel Week => Value;
        public DateFortnightViewModel Fortnight => Value;
        public DateMonthViewModel Month => Value;
        public DateQuarterViewModel Quarter => Value;
        public DateBimesterViewModel Bimester => Value;
        public DateSemesterViewModel Semester => Value;
        public int Year => Value.Year;

        public override string ToString() => Value.ToString("G");

        public static implicit operator bool(DateTimeViewModel self) => self != null;

        public static implicit operator DateTimeViewModel(DateTime? self) => self == null
            ? default
            : new DateTimeViewModel(self.Value);
    }
}