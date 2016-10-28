using System;
using System.Web.Http;
using RobotsAtWar.Server;
using RobotsAtWar.Server.Readers;

#pragma warning disable 1591

namespace RobotsAtWar.WebApi.Controllers
{
    public class AuthController : ApiController
    {
        [HttpGet]
        public bool Login(Guid robotId)
        {
            var reader = new RobotReader();
            Robot robot = reader.GetRobotInfo(robotId);

            return robot != null;
        }
    }
}

#pragma warning restore 1591
