using System.Globalization;

namespace JiraQueries.JiraRestApi.Jql {
    public sealed class JqlValueInt : JqlValue<int> {
        private static readonly CultureInfo Culture = CultureInfo.GetCultureInfo("en-US");

        private JqlValueInt(int value) {
            Value = value;
        }

        public int Value { get; }

        public override string ToString() => Value.ToString();

        public override string Resolve() => Value.ToString(Culture);

        public static implicit operator JqlValueInt(int value) => new JqlValueInt(value);
    }
}