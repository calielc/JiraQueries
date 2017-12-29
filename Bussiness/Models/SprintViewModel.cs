using System;
using System.Text.RegularExpressions;

namespace JiraQueries.Bussiness.Models {
    public sealed class SprintViewModel {
        private static readonly Regex RegEx = new Regex(
            @"name=([\w .-]*)\,[\w\d=]*\,startDate=([\d\w-:.\<\>]*)\,endDate=([\d\w-:.\<\>]*)\,",
            RegexOptions.IgnoreCase);

        public SprintViewModel(string greenHopperValue) {
            var result = RegEx.Matches(greenHopperValue);
            if (result.Count != 1) {
                return;
            }

            var groups = result[0].Groups;
            if (groups.Count != 4) {
                return;
            }

            Name = AdjustName(groups[1].Value);
            Release = AdjustRelease(Name);

            if (DateTime.TryParse(groups[2].Value, out var startAt)) {
                StartAt = startAt;
            }
            if (DateTime.TryParse(groups[3].Value, out var endAt)) {
                EndAt = endAt;
            }
        }

        public string Release { get; }

        public string Name { get; }

        public DateTimeViewModel StartAt { get; }

        public DateTimeViewModel EndAt { get; }

        private static string AdjustName(string value) {
            switch (value) {
                case null:
                    return string.Empty;

                case "PIM 6.1":
                case "PIM DIST - Sprint 1":
                case "PIM GERA - Sprint 1":
                    return "PIM 06.1";

                case "PIM 6.2":
                case "PIM DIST - Sprint 2":
                case "PIM GERA - Sprint 2":
                    return "PIM 06.2";

                case "PIM 6.3":
                case "PIM DIST - Sprint 3":
                case "PIM GERA - Sprint 3":
                    return "PIM 06.3";

                case "PIM 6.4":
                case "PIM DIST - Sprint 4":
                case "PIM GERA - Sprint 4":
                    return "PIM 06.4";

                case "PIM 7.1":
                case "PIM DIST - Sprint 5":
                case "PIM GERA - Sprint 5":
                case "Release 7 - GERA":
                    return "PIM 07.1";
                case "PIM 7.2":
                    return "PIM 07.2";
                case "PIM 7.3":
                    return "PIM 07.3";
                case "PIM 7.4":
                    return "PIM 07.4";

                case "PIM 8.1":
                    return "PIM 08.1";
                case "PIM 8.2":
                    return "PIM 08.2";
                case "PIM 8.3":
                    return "PIM 08.3";
                case "PIM 8.4":
                    return "PIM 08.4";
                case "PIM 8.5":
                    return "PIM 08.5";

                case "PIM 9.1 - Bugs":
                    return "PIM 09.1";
                case "PIM 9.2":
                    return "PIM 09.2";
                case "PIM 9.3":
                    return "PIM 09.3";
                case "PIM 9.4":
                    return "PIM 09.4";
                case "PIM 9.5 - Bugs":
                    return "PIM 09.5";

                case "PIM 10.1 - Bugs":
                    return "PIM 10.1";

                default:
                    return value.Trim();
            }
        }

        private static string AdjustRelease(string name) {
            if (name is null) {
                return null;
            }

            if (name.StartsWith("PIM 06.")) {
                return "Release 06";
            }
            if (name.StartsWith("PIM 07.")) {
                return "Release 07";
            }
            if (name.StartsWith("PIM 08.")) {
                return "Release 08";
            }
            if (name.StartsWith("PIM 09.")) {
                return "Release 09";
            }
            if (name.StartsWith("PIM 10.")) {
                return "Release 10";
            }
            if (name.StartsWith("PIM 11.")) {
                return "Release 11";
            }
            if (name.StartsWith("PIM 12.")) {
                return "Release 12";
            }

            return null;
        }

        public static implicit operator SprintViewModel(string greenHopperValue)
            => string.IsNullOrEmpty(greenHopperValue) ? default : new SprintViewModel(greenHopperValue);
    }
}