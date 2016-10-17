using System;
using System.Web.Http;
using RobotsAtWar.Server;
using RobotsAtWar.Server.Enums;

#pragma warning disable 1591

namespace RobotsAtWar.WebApi.Controllers
{
    public class ActionsController : ApiController
    {
        [HttpGet]
        public void Attack(Guid battleFieldId, Guid robotId, ActionStrength attackStrength)
        {
            BattleField battleField = BattleFields.GetBattleField(battleFieldId);

            if (battleField.IsBattleRunning)
            {
                battleField.GetRobot(robotId).Attack(attackStrength);
            }
        }

        [HttpGet]
        public void Defence(Guid battleFieldId, Guid robotId, ActionStrength defenceStrength)
        {
            BattleField battleField = BattleFields.GetBattleField(battleFieldId);

            if (battleField.IsBattleRunning)
            {
                battleField.GetRobot(robotId).Defence(defenceStrength);
            }
        }

        [HttpGet]
        public void Rest(Guid battleFieldId, Guid robotId, ActionStrength restStrength)
        {
            BattleField battleField = BattleFields.GetBattleField(battleFieldId);

            if (battleField.IsBattleRunning)
            {
                battleField.GetRobot(robotId).Rest(restStrength);
            }
        }

        [HttpGet]
        public RobotStatus Check(Guid battleFieldId, Guid robotId)
        {
            BattleField battleField = BattleFields.GetBattleField(battleFieldId);

            if (battleField.IsBattleRunning)
            {
                return battleField.GetRobot(robotId).Check();
            }

            return null;
        }
    }
}

#pragma warning restore 1591
