using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace RobotsAtWar.Server.Host.Controllers
{
    public class GetOpenRoomsController : ApiController
    {
        /// <summary>
        /// Returns a list of open public game rooms.
        /// </summary>
        /// <param name="myGuid">User GUID</param>
        /// <returns>List of open game rooms</returns>
        [Route("GetOpenRooms/{myGuid}")]
        public List<string> Get(Guid myGuid)
        {
            List<string> list = new List<string>();
            if (Database.UserExists(myGuid))
            {
                list.AddRange(BattleFieldSingleton.BattleFieldByGuid.Select(entry => entry.Key + Database.GetUserFreeText(entry.Key)));
                return list;
            }
            return null;
        }

    }
}