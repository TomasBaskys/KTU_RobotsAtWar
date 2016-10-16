using System;
using System.Web.Http;

namespace RobotsAtWar.Server.Host.Controllers
{
    public class WinnerController : ApiController
    {
        // POST api/<controller>
        /// <summary>
        /// Returns a message about the end of the battle 
        /// </summary>
        /// <param name="roomGuid">Room Guid where user are playing</param>
        /// <param name="myGuid">User's personal Guid</param>
        /// <returns></returns>
        [Route("Winner/{roomGuid}/{myGuid}")]
        public string Get(Guid roomGuid, Guid myGuid)
        {
            if (BattleFieldSingleton.GetBattleFieldByGuid(roomGuid).AreUsersReady())
            {
                return BattleFieldSingleton.GetBattleFieldByGuid(roomGuid).CheckForWinner(myGuid);
            }
            return "Other user has not joined yet";
        }
    }
}