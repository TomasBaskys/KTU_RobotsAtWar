using System;
using System.Web.Http;

namespace RobotsAtWar.Server.Host.Controllers
{
    public class IsAliveController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Players who want to check if battle is still going on should make a request to this controller
        /// </summary>
        /// <param name="roomGuid">The GUID of the room that is checked</param>
        /// <returns>bool True, if battle is over, false, if both warriors are still alive</returns>
        [Route("IsAlive/{roomGuid}")]
        public bool Get(Guid roomGuid)
        {
            if (BattleFieldSingleton.GetBattleFieldByGuid(roomGuid).AreUsersReady())
            {
                return BattleFieldSingleton.GetBattleFieldByGuid(roomGuid).IsBattleOver();
            }
            return false;
        }
    }
}