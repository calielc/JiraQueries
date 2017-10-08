using System.Linq;

namespace JiraQueries.JiraRestApi.Jql {
    public interface IJql : IUrlResolver { }

    public static class Jql {
        public static IJql Or(params IJql[] conditionals) => new JqlOr(conditionals);

        public static IJql And(params IJql[] conditionals) => new JqlAnd(conditionals);
    }

    public static class JqlExtensions {
        public static IJql Or(this IJql left, IJql right)
            => new JqlOr(new[] { left, right });

        public static IJql And(this IJql left, IJql right)
            => new JqlAnd(new[] { left, right });

        public static IJql Less<T>(this JqlField<T> field, T value) where T : IJqlValue
            => new JqlComparison(field, value, "<");

        public static IJql LessOrEqual<T>(this JqlField<T> field, T value) where T : IJqlValue
            => new JqlComparison(field, value, "<=");

        public static IJql Equal<T>(this JqlField<T> field, T value) where T : IJqlValue
            => new JqlComparison(field, value, "=");

        public static IJql GreatherOrEqual<T>(this JqlField<T> field, T value) where T : IJqlValue
            => new JqlComparison(field, value, ">=");

        public static IJql Greather<T>(this JqlField<T> field, T value) where T : IJqlValue
            => new JqlComparison(field, value, ">");

        public static IJql NotEqual<T>(this JqlField<T> field, T value) where T : IJqlValue
            => new JqlComparison(field, value, "<>");

        public static IJql In<T>(this JqlField<T> field, params T[] values) where T : IJqlValue
            => new JqlIn(field, values?.Cast<IJqlValue>().ToArray());
    }
}