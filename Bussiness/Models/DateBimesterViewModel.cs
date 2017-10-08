using System;

namespace JiraQueries.Bussiness.Models
{
    public struct DateBimesterViewModel {
        public DateBimesterViewModel(DateTime dateTime) {
            Value = 1 + (dateTime.Month - 1) / 2;
        }

        public string Text => $"B{Value}";
        public int Value { get; }

        public static implicit operator DateBimesterViewModel(DateTime value) => new DateBimesterViewModel(value);
    }
}