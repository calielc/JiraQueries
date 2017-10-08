using System.Collections.Generic;
using System.Linq;
using JiraQueries.JiraRestApi.Models;

namespace JiraQueries.Bussiness.Models {
    public sealed class ArrayViewModel {
        private ArrayViewModel(IReadOnlyCollection<JiraCommon> array) {
            Any = array.Any();
            Empty = !Any;
            Count = array.Count;
            Items = array.Select(x => (string) x).ToArray();
        }

        public BoolViewModel Any { get; }

        public BoolViewModel Empty { get; }

        public int Count { get; }

        public string[] Items { get; }

        public static implicit operator bool(ArrayViewModel self) => self != null && self.Any;

        public static implicit operator ArrayViewModel(JiraCommon[] array) =>
            array is null ? default : new ArrayViewModel(array);
    }
}