using System.Web.Http;

namespace RobotsAtWar.WebApi.Controllers
{
    public class ActionsController : ApiController
    {
        [HttpGet]
        public string[] Attack()
        {
            return new[] {"value1", "value2"};
        }
    }
}
