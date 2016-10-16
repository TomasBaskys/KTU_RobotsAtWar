using System;
using System.Web.Http;
using System.Web.Http.Description;
using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server.Host.Controllers
{
    /// <summary>
    /// Vo čia!!!
    /// </summary>
    /// 
    public class RestController : ApiController
    {
        // POST api/<controller>

        /// <summary>
        /// Returns a DefenceOutcome of resting action
        /// </summary>
        /// <param name="roomGuid">Room Guid where user are playing</param>
        /// <param name="myGuid">User's personal Guid</param>
        /// <param name="time">The amount of time user want to rest</param>
        ///<returns></returns>
        [Route("RestController/{roomGuid}/{myGuid}/{time}")]
        [ResponseType(typeof(RestOutcome))]
        public RestOutcome Post(Guid roomGuid, Guid myGuid, int time)
        {
            if (BattleFieldSingleton.GetBattleFieldByGuid(roomGuid).AreUsersReady())
            {
                //var time = Int32.Parse(timeString);
                return BattleFieldSingleton.GetBattleFieldByGuid(roomGuid).Rest(myGuid, time);
            }
            return RestOutcome.BattleNotStarted;
        }
    }
}