using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace JiraQueries.Api.Util {
    public sealed class AttachmentResponse {
        public string MediaType { get; set; }

        public string FileName { get; set; }

        public HttpContent Content { get; set; }

        public static implicit operator HttpResponseMessage(AttachmentResponse self) {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = self.Content;
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(self.MediaType);
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") {
                FileName = self.FileName
            };

            return result;
        }
    }
}