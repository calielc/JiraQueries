using System;
using System.Collections.Generic;
using System.Linq;

namespace JiraQueries.Bussiness.Services {
    public sealed class CollectionStatistics {
        public CollectionStatistics(IReadOnlyCollection<double> collection) {
            Count = collection.Count;
            switch (Count) {
                case 0:
                    Min = Max = Average = Sum = null;
                    break;
                case 1:
                    Min = Max = Average = Sum = collection.Single();
                    break;
                default:
                    var min = Min = collection.Min();
                    var max = Max = collection.Max();
                    Average = Count >= 10
                        ? collection.Where(value => value > min && value < max).Average()
                        : collection.Average();
                    Sum = collection.Sum();
                    break;
            }
        }

        public int Count { get; }

        public double? Min { get; }
        public double? Max { get; }
        public double? Average { get; }
        public double? Sum { get; }

        public static CollectionStatistics FromEnumerable(IEnumerable<double> enumerable)
            => enumerable == null ? default : new CollectionStatistics(enumerable.ToArray());

        public static CollectionStatistics FromEnumerable<T>(IEnumerable<T> enumerable, Func<T, double> selector)
            => enumerable == null ? default : new CollectionStatistics(enumerable.Select(selector).ToArray());
    }
}