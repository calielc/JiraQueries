using System;

namespace JiraQueries.JiraRestApi.Jql {
    public sealed class JqlValueDateTime : JqlValue<DateTime> {
        public JqlValueDateTime(DateTime value, string format) {
            Format = format;
            Value = value;
        }

        public DateTime Value { get; }
        public string Format { get; }

        public override string ToString() => Value.ToString("o");

        public override string Resolve() => Value.ToString(Format);

        public static implicit operator JqlValueDateTime(DateTime value) => new JqlValueDateTime(value, "yyyy-MM-dd");
    }
}