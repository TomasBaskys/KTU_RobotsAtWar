using System.Collections.Generic;
using System.Web.Http;
using RobotsAtWar.Server;

#pragma warning disable 1591

namespace RobotsAtWar.WebApi.Controllers
{
    public class BattleFieldController : ApiController
    {
        [HttpGet]
        public string HostBattle(string robotId, string battleName, RoomType roomType)
        {
            return BattleFields.CreateBattleField(robotId, battleName, roomType);
        }

        [HttpGet]
        public void JoinBattle(string battleId, string robotId, PlayType playType)
        {
            BattleFields.JoinBattleField(battleId, robotId, playType);
        }

        [HttpGet]
        public IEnumerable<BattleField> GetBattles()
        {
            return BattleFields.GetBattleFields();
        }

        [HttpGet]
        public BattleField GetBattle(string battleFieldId)
        {
            return BattleFields.GetBattleField(battleFieldId);
        }

        [HttpGet]
        public List<Robot> RobotsInBattle(string battleFieldId)
        {
            BattleField battle = BattleFields.GetBattleField(battleFieldId);

            return battle.Robots;
        }

        [HttpGet]
        public int RobotsInBattleCount(string battleFieldId)
        {
            BattleField battle = BattleFields.GetBattleField(battleFieldId);

            return battle.Robots.Count;
        }

        [HttpGet]
        public string StartDemoBattle(string robotId)
        {
            string battleFieldId = BattleFields.CreateBattleField(robotId, "DemoBattle", RoomType.Private);

            BattleFields.JoinBattleField(battleFieldId, robotId, PlayType.Manual);
            BattleFields.JoinBattleField(battleFieldId, "DEMO", PlayType.Auto);

            BattleField battle = BattleFields.GetBattleField(battleFieldId);
            battle.StartBattle();

            return battleFieldId;
        }

        [HttpGet]
        public string RobotStatus(string battleFieldId, string robotId)
        {
            return BattleFields.RobotStatus(battleFieldId, robotId);
        }
    }
}

#pragma warning restore 1591
