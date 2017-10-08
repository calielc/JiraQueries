using System;

namespace JiraQueries.Bussiness.Models {
    public sealed class TimeSpanViewModel {
        private TimeSpanViewModel(TimeSpan timeSpan) {
            Value = timeSpan;
        }

        public TimeSpan Value { get; }

        public double TotalDays => Value.TotalDays;

        public double TotalHours => Value.TotalHours;

        public double TotalSeconds => Value.TotalSeconds;

        public override string ToString() => Value.ToString();

        public static implicit operator bool(TimeSpanViewModel self) => self != null;

        public static implicit operator TimeSpanViewModel(long? seconds) => seconds is null
            ? default
            : new TimeSpanViewModel(TimeSpan.FromSeconds(seconds.Value));

        public static implicit operator TimeSpanViewModel(TimeSpan? timeSpan) => timeSpan is null
            ? default
            : new TimeSpanViewModel(timeSpan.Value);
    }
}