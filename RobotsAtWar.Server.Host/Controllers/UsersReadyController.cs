using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RobotsAtWar.Server.Host.Controllers
{
    public class UsersReadyController : ApiController
    {
        /// <summary>
        /// Returns whether both users are ready
        /// </summary>
        /// <param name="roomGuid">Room Guid where user are playing</param>
        /// <returns></returns>
        [Route("UsersReady/{roomGuid}")]
        public bool Post(Guid roomGuid)
        {
            if (roomGuid == Guid.Empty)
                roomGuid = Guid.NewGuid();
            return BattleFieldSingleton.GetBattleFieldByGuid(roomGuid).AreUsersReady();
        }
    }
}