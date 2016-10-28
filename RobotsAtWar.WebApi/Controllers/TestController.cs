using System;
using System.Web.Http;

#pragma warning disable 1591

namespace RobotsAtWar.WebApi.Controllers
{
    public class TestController : ApiController
    {
        [HttpGet]
        public string[] Values(Guid value)
        {
            return new[] {"value1", value.ToString()};
        }
    }
}

#pragma warning restore 1591
