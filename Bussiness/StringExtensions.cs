using System.Globalization;
using System.Text;

namespace JiraQueries.Bussiness {
    public static class StringExtensions {
        public static string RemoveDiacritics(this string self) {
            if (self is null) {
                return null;
            }

            var normalizedString = self.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString) {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark) {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}