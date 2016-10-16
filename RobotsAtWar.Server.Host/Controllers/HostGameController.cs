using System;
using System.Web.Http;
using log4net;
using log4net.Repository.Hierarchy;

namespace RobotsAtWar.Server.Host.Controllers
{
    public class HostGameController : ApiController
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Warrior));

        // POST api/<controller>
        /// <summary>
        /// User who wants to host a new game rooms creates a request to this controller
        /// </summary>
        /// <param name="myGuid">GUID of the user who makes a request</param>
        /// <returns>The GUID of a new war room</returns>
        [Route("HostGame/{myGuid}")]
        public Guid Post(string myGuid)
        {
            Guid a =  Guid.Parse(myGuid);
            Guid warRoomGuid = Guid.NewGuid();
            BattleFieldSingleton.BattleFieldByGuid.Add(warRoomGuid, new BattleField());
            Logger.Info("new game room added: \n"+warRoomGuid+"\nHost guid:\n"+ myGuid);
            BattleFieldSingleton.GetBattleFieldByGuid(warRoomGuid).RegisterWarrior(a);
            return warRoomGuid;
        }
    }
}