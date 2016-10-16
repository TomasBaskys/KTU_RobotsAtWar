using System.Collections.Generic;
using System.Web.Http;

namespace RobotsAtWar.Server.Host.Controllers
{
    public class RoomsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return BattleFieldSingleton.GetAllPublicRooms();
        }
    }
}