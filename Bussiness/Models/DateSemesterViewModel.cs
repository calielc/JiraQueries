using System;

namespace JiraQueries.Bussiness.Models
{
    public struct DateSemesterViewModel {
        public DateSemesterViewModel(DateTime dateTime) {
            Value = 1 + (dateTime.Month - 1) / 6;
        }

        public string Text => $"S{Value}";
        public int Value { get; }

        public static implicit operator DateSemesterViewModel(DateTime value) => new DateSemesterViewModel(value);
    }
}