using System;

namespace JiraQueries.Bussiness.Models {
    public struct DateMonthViewModel {
        public DateMonthViewModel(DateTime dateTime) {
            Value = dateTime.Month;
            LongText = dateTime.ToString("MMMM");
            ShortText = dateTime.ToString("MMM");
        }

        public string ShortText { get; }
        public string LongText { get; }

        public int Value { get; }

        public static implicit operator DateMonthViewModel(DateTime value) => new DateMonthViewModel(value);
    }
    public struct DateFortnightViewModel {
        public DateFortnightViewModel(DateTime dateTime) {
            Value = dateTime.Day < 16 ? 1 : 2;
        }

        public string Text => $"F{Value}";
        public int Value { get; }

        public static implicit operator DateFortnightViewModel(DateTime value) => new DateFortnightViewModel(value);
    }
}