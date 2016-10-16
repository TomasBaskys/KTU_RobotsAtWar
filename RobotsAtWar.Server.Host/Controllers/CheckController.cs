using System;
using System.Web.Http;
using System.Web.Http.Description;

namespace RobotsAtWar.Server.Host.Controllers
{
    public class CheckController : ApiController
    {
        /// <summary>
        /// Issues a check command which returns the enemy WarriorState object.
        /// </summary>
        /// <param name="roomGuid">Battle Room GUID</param>
        /// <param name="myGuid">User GUID</param>
        /// <returns>WarriorState object</returns>
        [Route("Check/{roomGuid}/{myGuid}")]
        [ResponseType(typeof(WarriorState))]
        public WarriorState Post(Guid roomGuid, Guid myGuid)
        {
            if (BattleFieldSingleton.GetBattleFieldByGuid(roomGuid).AreUsersReady())
                return BattleFieldSingleton.GetBattleFieldByGuid(roomGuid).Check(myGuid);
            return null;
        }
    }
}