using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;

namespace RobotsAtWar.Server.Host.Controllers
{
    public class LeaderboardController : ApiController
    {
        /// <summary>
        /// Player who want to receive their current rank should make a request to this controller
        /// </summary>
        /// <param name="guid">The GUID of user whos rank is checked</param>
        /// <returns>integer with rank of user</returns>
        [Route("Rank/{guid}")]
        public int Get(string guid)
        {
            return Leaderboard.GetLeaderboardPosition(guid);
        }

    }
}