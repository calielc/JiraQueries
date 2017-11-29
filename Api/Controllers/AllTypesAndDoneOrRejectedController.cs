using System.Collections.Generic;
using System.Threading.Tasks;
using JiraQueries.Api.Util;
using JiraQueries.Bussiness.Jira;
using JiraQueries.Bussiness.Models;
using JiraQueries.Bussiness.Services;
using Microsoft.AspNetCore.Mvc;

namespace JiraQueries.Api.Controllers {
    public sealed class AllTypesAndDoneOrRejectedController : Controller {
        [HttpGet, Route("{project}/[controller].xml")]
        public async Task<ContentResult> Xml(string project) {
            var service = CreateService(project);

            var xmlExporter = await service.Xml("AllTypesAndDoneOrRejected.xsd");
            return xmlExporter.AsContentResult();
        }

        [HttpGet, Route("{project}/[controller].xsd")]
        public ContentResult Xsd(string project) {
            var service = CreateService(project);

            var xmlExporter = service.Xsd();
            return xmlExporter.AsContentResult();
        }

        [HttpGet, Route("{project}/[controller].csv")]
        public async Task<ContentResult> Csv(string project) {
            var service = CreateService(project);

            var csvExporter = await service.Csv(",");
            return csvExporter.AsContentResult();
        }

        [HttpGet, Route("{project}/[controller].raw")]
        public async Task<IEnumerable<IssueViewModel>> Raw(string project) {
            var service = CreateService(project);

            return await service.Raw();
        }

        private static AllTypesAndDoneOrRejectedService CreateService(string project)
            => new AllTypesAndDoneOrRejectedService(JiraAccessPoint.Instance) {
                Project = project
            };
    }
}