using System;
using System.Web.Http;
using System.Web.Http.Description;

namespace RobotsAtWar.Server.Host.Controllers
{
    /// <summary>
    /// Controller returns a WarriorState object which represents current state and life points of the selected warrior.
    /// </summary>
    public class MyStatsController : ApiController
    {
        /// <summary>
        /// Method returns a WarriorState object which represents current state and life points of the selected warrior.
        /// </summary>
        /// <param name="roomGuid">
        /// GUID of the battlefield room
        /// </param>
        /// <param name="myGuid">
        /// GUID of the warrior
        /// </param>
        /// <returns>
        /// WarriorState object
        /// </returns>
        [Route("MyStats/{roomGuid}/{myGuid}")]
        [ResponseType(typeof(WarriorState))]
        public WarriorState Get(Guid roomGuid, Guid myGuid)
        {
            if (BattleFieldSingleton.GetBattleFieldByGuid(roomGuid).AreUsersReady())
            {
                return BattleFieldSingleton.GetBattleFieldByGuid(roomGuid).MyStats(myGuid);
            }
            return null;
        }
    }
}