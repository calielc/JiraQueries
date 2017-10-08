namespace JiraQueries.Bussiness.Models {
    public sealed class BoolViewModel {
        public BoolViewModel(bool? value) {
            Value = value;
        }

        public bool? Value { get; }

        public string Text {
            get {
                switch (Value) {
                    case true:
                        return "Yes";
                    case false:
                        return "No";
                    default:
                        return "Undefined";
                }
            }
        }

        public static implicit operator bool(BoolViewModel value) => value != null && value.Value == true;

        public static implicit operator BoolViewModel(bool? value) => new BoolViewModel(value);
    }
}