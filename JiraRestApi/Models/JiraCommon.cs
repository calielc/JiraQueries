namespace JiraQueries.JiraRestApi.Models {
    public class JiraCommon {
        public string Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public static implicit operator string(JiraCommon self) => self == null
            ? default
            : self.DisplayName ?? self.Name ?? self.Value ?? self.Key ?? self.Id;
    }
}