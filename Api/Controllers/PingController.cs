using System;
using Microsoft.AspNetCore.Mvc;

namespace JiraQueries.Api.Controllers {
    [Route("ping")]
    public class PingController : Controller {
        [HttpGet]
        public string Get() => DateTime.Now.ToString("o");
    }
}
