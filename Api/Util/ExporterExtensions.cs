using System.Linq;
using System.Net.Http;
using System.Text;
using JiraQueries.Bussiness.Export;
using Microsoft.AspNetCore.Mvc;

namespace JiraQueries.Api.Util {
    public static class ExporterExtensions {
        public static ContentResult AsContentResult(this IXmlExporter self)
            => new ContentResult {
                Content = self.Build(),
                ContentType = "text/xml, utf-8",
            };

        public static ContentResult AsContentResult(this ICsvExporter self)
            => new ContentResult {
                Content = self.Build(),
                ContentType = "text/csv, utf-8",
            };

        public static AttachmentResponse AsAttachment(this ICsvExporter self, string fileName)
            => new AttachmentResponse {
                MediaType = "text/csv",
                FileName = fileName,
                Content = BuildUtfContent(self)
            };

        public static AttachmentResponse AsAttachment(this IXmlExporter self, string fileName)
            => new AttachmentResponse {
                MediaType = "text/xml",
                FileName = fileName,
                Content = BuildUtfContent(self)
            };

        private static ByteArrayContent BuildUtfContent(IExporter exporter) {
            var content = exporter.Build();
            var utf8Array = Encoding.UTF8.GetBytes(content);

            var bytes = Encoding.UTF8.GetPreamble().Concat(utf8Array).ToArray();

            return new ByteArrayContent(bytes);
        }
    }
}