using System;
using System.Globalization;

namespace JiraQueries.Bussiness.Export {
    public interface IMappingItem {
        string Field { get; }

        Type DataType { get; }
    }

    public interface IMappingItem<in TItem> : IMappingItem {
        string Resolve(TItem value, CultureInfo culture);
    }
}