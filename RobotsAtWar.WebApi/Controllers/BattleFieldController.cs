using System;
using System.Collections.Generic;
using System.Web.Http;
using RobotsAtWar.Server;

#pragma warning disable 1591

namespace RobotsAtWar.WebApi.Controllers
{
    public class BattleFieldController : ApiController
    {
        [HttpGet]
        public string HostBattle(string robotId)
        {
            string battleFieldId = BattleFields.CreateBattleField(robotId);

            BattleFields.JoinBattleField(battleFieldId, robotId);

            return battleFieldId;
        }

        [HttpGet]
        public void JoinBattle(string battleId, string robotId)
        {
            BattleFields.JoinBattleField(battleId, robotId);
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

            BattleFields.JoinBattleField(battleFieldId, robotId);
            BattleFields.JoinBattleField(battleFieldId, "DEMO");

            return battleFieldId;
        }
    }
}

#pragma warning restore 1591
