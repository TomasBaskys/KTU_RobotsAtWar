using System;
using System.Web.Http;
using System.Web.Http.Description;
using log4net;
using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server.Host.Controllers
{
    public class JoinGameController : ApiController
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Warrior));

        // POST api/<controller>
        /// <summary>
        /// Players who want to join an already created room should make a request to this controller
        /// </summary>
        /// <param name="roomGuid">The GUID of war room that user wants to join</param>
        /// <param name="myGuid">The GUID of user</param>
        /// <returns>DefenceOutcome string if attempt to join was succesfull</returns>
        [Route("JoinGame/{roomGuid}/{myGuid}")]
        [ResponseType(typeof(JoinRoomOutcome))]
        public JoinRoomOutcome Post(Guid roomGuid, Guid myGuid)
        {
            if (BattleFieldSingleton.ContainsBattleField(roomGuid))
            {
                if (BattleFieldSingleton.GetBattleFieldByGuid(roomGuid).RegisterWarrior(myGuid))
                {
                    BattleFieldSingleton.GetBattleFieldByGuid(roomGuid).BothUsersJoined();
                    Logger.Info(myGuid+ " user has joined the room:\n "+roomGuid);
                    return JoinRoomOutcome.Success;
                }
                return JoinRoomOutcome.UserAlreadyInRoom;
            }
            return JoinRoomOutcome.NoSuchRoomExists;
        }
    }
}