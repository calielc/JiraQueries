using System;

namespace JiraQueries.Bussiness.Models
{
    public struct DateQuarterViewModel {
        public DateQuarterViewModel(DateTime dateTime) {
            Value = 1 + (dateTime.Month - 1) / 3;
        }

        public string Text => $"Q{Value}";
        public int Value { get; }

        public static implicit operator DateQuarterViewModel(DateTime value) => new DateQuarterViewModel(value);
    }
}