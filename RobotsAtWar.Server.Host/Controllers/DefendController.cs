using System;
using System.Web.Http;
using System.Web.Http.Description;
using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server.Host.Controllers
{
    public class DefendController : ApiController
    {
        /// <summary>
        /// Issues a defend command to the user controlled robot.
        /// </summary>
        /// <param name="roomGuid">Battle Room GUID</param>
        /// <param name="myGuid">User GUID</param>
        /// <param name="time">Length of the defense action</param>
        /// <returns>DefenceOutcome enumeration indicating the result.</returns>
        [Route("Defend/{roomGuid}/{myGuid}/{time}")]
        [ResponseType(typeof(DefenceOutcome))]
        public DefenceOutcome Post(Guid roomGuid, Guid myGuid, int time)
        {
            if (BattleFieldSingleton.GetBattleFieldByGuid(roomGuid).AreUsersReady())
            {
                return BattleFieldSingleton.GetBattleFieldByGuid(roomGuid).Defend(myGuid, time);
            }
            return DefenceOutcome.BattleNotStarted;
        }
    }
}