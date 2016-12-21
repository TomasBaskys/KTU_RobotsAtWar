using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using RobotsAtWar.Server;

#pragma warning disable 1591

namespace RobotsAtWar.WebApi.Controllers
{
    public class BattleFieldController : ApiController
    {
        [HttpGet]
        public string HostBattle(string robotId, PlayType playType)
        {
            string battleFieldId = BattleFields.CreateBattleField(robotId);

            BattleFields.JoinBattleField(battleFieldId, robotId, playType);

            return battleFieldId;
        }

        [HttpGet]
        public void JoinBattle(string battleId, string robotId, PlayType playType)
        {
            BattleFields.JoinBattleField(battleId, robotId, playType);
        }

        [HttpGet]
        public IEnumerable<Battle> GetBattles()
        {
            return BattleFields.GetBattleFields();
        }

        [HttpGet]
        public string StartDemoBattle(string robotId)
        {
            string battleFieldId = BattleFields.CreateBattleField(robotId);

            BattleFields.JoinBattleField(battleFieldId, robotId, PlayType.Manual);
            BattleFields.JoinBattleField(battleFieldId, "DEMO", PlayType.Auto);

            BattleField battle = BattleFields.GetBattleField(battleFieldId);
            battle.StartBattle();

            return battleFieldId;
        }
    }
}

#pragma warning restore 1591
