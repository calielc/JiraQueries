using JiraQueries.JiraRestApi.Models;

namespace JiraQueries.Bussiness.Models {
    public sealed class CommonViewModel {
        private CommonViewModel(JiraCommon common) {
            Key = common.Key ?? common.Value;
            Name = common.DisplayName ?? common.Name ?? common.Value ?? common.Key;
        }

        public string Key { get; }

        public string Name { get; }

        public static implicit operator CommonViewModel(JiraCommon self) =>
            self is null ? null : new CommonViewModel(self);
    }
}