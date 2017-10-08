using System.Linq;

namespace JiraQueries.Bussiness.Models {
    public sealed class LabelsViewModel {
        private LabelsViewModel(string[] labels) {
            NaoPlanejado = labels.Contains("naoPlanejado");
            Paralelo = labels.Contains("paralelo");
            Backlog = labels.Contains("backlog");
            PdC = labels.Contains("PIM-PdC");
        }

        public BoolViewModel Backlog { get; }
        public BoolViewModel NaoPlanejado { get; }
        public BoolViewModel Paralelo { get; }
        public BoolViewModel PdC { get; set; }

        public static implicit operator LabelsViewModel(string[] labels)
            => labels == null || labels.Length == 0 ? null : new LabelsViewModel(labels);
    }
}