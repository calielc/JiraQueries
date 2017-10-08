using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace JiraQueries.JiraRestApi {
    public sealed class ResolverCollection<T> : IUrlResolver, IEnumerable<T> where T : IUrlResolver {
        public ResolverCollection(IEnumerable<T> values, string separator) {
            Values = values;
            Separator = separator;
        }

        public string Separator { get; }

        public IEnumerable<T> Values { get; }

        public string Resolve() {
            var values = Values.Select(item => item.Resolve());
            return string.Join(Separator, values);
        }

        public IEnumerator<T> GetEnumerator() => Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) Values).GetEnumerator();
    }

    public static class ResolverCollectionExtensions {
        public static ResolverCollection<T> Merge<T>(this IEnumerable<T> self, string separator) where T : IUrlResolver
            => new ResolverCollection<T>(self, separator);

        public static string Resolve<T>(this IEnumerable<T> self, string separator) where T : IUrlResolver
            => new ResolverCollection<T>(self, separator).Resolve();
    }
}