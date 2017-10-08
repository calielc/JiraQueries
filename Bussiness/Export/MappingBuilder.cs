using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace JiraQueries.Bussiness.Export {
    public sealed class MappingBuilder<TItem> : IReadOnlyCollection<IMappingItem<TItem>> {
        private readonly List<IMappingItem<TItem>> _items = new List<IMappingItem<TItem>>();

        public MappingBuilder() { }

        public MappingBuilder(IEnumerable<IMappingItem<TItem>> items)
            => _items = new List<IMappingItem<TItem>>(items);

        public int Count => _items.Count;

        public MappingBuilder<TItem> Add<TField>(string field, Func<TItem, TField> getValue)
            => AddOrReplace(new MappingItem<TItem, TField>(field, getValue));

        public MappingBuilder<TItem> Add<TField>(string field, Func<TItem, TField> getValue, string format)
            => AddOrReplace(new MappingItem<TItem, TField>(field, getValue) {
                Format = format
            });

        public IEnumerator<IMappingItem<TItem>> GetEnumerator() => _items.GetEnumerator();

        private MappingBuilder<TItem> AddOrReplace(IMappingItem<TItem> newItem) {
            var oldItem = Enumerable.SingleOrDefault(_items, item => item.Field == newItem.Field);
            if (oldItem == null) {
                _items.Add(newItem);
                return this;
            }

            var index = _items.IndexOf(oldItem);
            _items[index] = newItem;

            return this;
        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) _items).GetEnumerator();
    }
}