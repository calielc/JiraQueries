using System;
using System.Globalization;

namespace JiraQueries.Bussiness.Export {
    public class MappingItem<TItemList, TField> : IMappingItem<TItemList> {
        private readonly Func<TItemList, TField> _getValue;

        public MappingItem(string field, Func<TItemList, TField> getValue) {
            Field = field;
            _getValue = getValue;
        }

        public string Field { get; }

        public Type DataType => typeof(TField);

        public string Format { get; set; }

        public string Resolve(TItemList value, CultureInfo culture) {
            var result = _getValue(value);
            if (result == null) {
                return string.Empty;
            }

            if (result is IFormattable formatable) {
                return formatable.ToString(Format, culture);

            }
            return result.ToString();
        }
    }
}