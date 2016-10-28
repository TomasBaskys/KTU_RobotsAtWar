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
                Robot robot = battleField.GetRobot(robotId);

                RobotActions.Attack(robot, attackStrength);
            }
        }

        [HttpGet]
        public void Defence(Guid battleFieldId, Guid robotId, ActionStrength defenceStrength)
        {
            BattleField battleField = BattleFields.GetBattleField(battleFieldId);

            if (battleField.IsBattleRunning)
            {
                Robot robot = battleField.GetRobot(robotId);

                RobotActions.Defence(robot, defenceStrength);
            }
        }

        [HttpGet]
        public void Rest(Guid battleFieldId, Guid robotId, ActionStrength restStrength)
        {
            BattleField battleField = BattleFields.GetBattleField(battleFieldId);

            if (battleField.IsBattleRunning)
            {
                Robot robot = battleField.GetRobot(robotId);

                RobotActions.Rest(robot, restStrength);
            }
        }

        [HttpGet]
        public RobotStatus Check(Guid battleFieldId, Guid robotId)
        {
            BattleField battleField = BattleFields.GetBattleField(battleFieldId);

            if (battleField.IsBattleRunning)
            {
                Robot robot = battleField.GetRobot(robotId);

                return RobotActions.Check(robot);
            }

            return null;
        }
    }
}

#pragma warning restore 1591
