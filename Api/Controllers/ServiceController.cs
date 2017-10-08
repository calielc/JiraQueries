using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiraQueries.Api.Util;
using JiraQueries.Bussiness.Jira;
using JiraQueries.Bussiness.Models;
using JiraQueries.Bussiness.Services;
using Microsoft.AspNetCore.Mvc;

namespace JiraQueries.Api.Controllers {
    public abstract class ServiceController<T> : Controller {
        private readonly string _controllerName;

        protected ServiceController() {
            _controllerName = GetType().Name.Replace("Controller", "");
        }

        [HttpGet, Route("{project}/[controller].xml")]
        public async Task<ContentResult> Xml(string project) {
            var service = CreateService(project);

            var xsdUrl = $"{_controllerName}.xsd";

            var xmlExporter = await service.Xml(xsdUrl);
            return xmlExporter.AsContentResult();
        }

        [HttpGet, Route("{project}/[controller].xsd")]
        public ContentResult Xsd(string project) {
            var service = CreateService(project);

            var xmlExporter = service.Xsd();
            return xmlExporter.AsContentResult();
        }

        [HttpGet, Route("{project}/[controller].csv")]
        public async Task<ContentResult> Csv(string project, string separator = ",") {
            var service = CreateService(project);

            var csvExporter = await service.Csv(separator);
            return csvExporter.AsContentResult();
        }

        [HttpGet, Route("{project}/[controller].raw")]
        public async Task<IEnumerable<T>> Raw(string project) {
            var service = CreateService(project);

            var issues = await service.Json();
            return issues;
        }

        protected abstract IService<T> CreateService(string project);
    }

    [Obsolete]
    public sealed class CampaignController : ServiceController<IssueViewModel> {
        protected override IService<IssueViewModel> CreateService(string project)
            => new CampaignService(JiraAccessPoint.Instance) {
                Project = project
            };
    }

    [Obsolete]
    public sealed class TimelineController : ServiceController<IssueViewModel> {
        protected override IService<IssueViewModel> CreateService(string project)
            => new TimelineService(JiraAccessPoint.Instance) {
                Project = project
            };
    }

    public sealed class AllTypesAndDoneOrRejectedController : ServiceController<IssueViewModel> {
        protected override IService<IssueViewModel> CreateService(string project)
            => new AllTypesAndDoneOrRejectedService(JiraAccessPoint.Instance) {
                Project = project
            };
    }
}