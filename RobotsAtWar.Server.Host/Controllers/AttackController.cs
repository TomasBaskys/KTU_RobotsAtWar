using System;
using System.Web.Http;
using System.Web.Http.Description;
using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server.Host.Controllers
{
    public class AttackController : ApiController
    {
        /// <summary>
        /// Issues an attack command to the user controlled robot.
        /// </summary>
        /// <param name="roomGuid">Battle room GUID</param>
        /// <param name="myGuid">User GUID</param>
        /// <param name="strength">The strength which is applied to your attack.</param>
        /// <returns>Enumeration indicating the result of your attack.</returns>
        [Route("Attack/{roomGuid}/{myGuid}/{strength}")]
        [ResponseType(typeof(AttackOutcome))]
        public AttackOutcome Post(Guid roomGuid, Guid myGuid, string strength)
        {
            if (BattleFieldSingleton.GetBattleFieldByGuid(roomGuid).AreUsersReady())
                return BattleFieldSingleton.GetBattleFieldByGuid(roomGuid)
                    .Attack(myGuid, (Strength) Enum.Parse(typeof (Strength), strength));
            return AttackOutcome.BattleNotStarted;
        }
    }
}
