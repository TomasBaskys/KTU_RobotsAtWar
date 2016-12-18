using System.Web.Http;
using RobotsAtWar.Server;
using RobotsAtWar.Server.Enums;

#pragma warning disable 1591

namespace RobotsAtWar.WebApi.Controllers
{
    public class ActionsController : ApiController
    {
        [HttpGet]
        public int Attack(string battleFieldId, string robotId, ActionStrength attackStrength)
        {
            int damage;
            BattleField battleField = BattleFields.GetBattleField(battleFieldId);

            if (battleField.IsBattleRunning)
            {
                Robot robot = battleField.GetRobot(robotId);

                damage = RobotActions.Attack(robot, attackStrength);
            }
            else
            {
                damage = -99;
            }

            return damage;
        }

        [HttpGet]
        public void Defence(string battleFieldId, string robotId, ActionStrength defenceStrength)
        {
            BattleField battleField = BattleFields.GetBattleField(battleFieldId);

            if (battleField.IsBattleRunning)
            {
                Robot robot = battleField.GetRobot(robotId);

                RobotActions.Defence(robot, defenceStrength);
            }
        }

        [HttpGet]
        public void Rest(string battleFieldId, string robotId, ActionStrength restStrength)
        {
            BattleField battleField = BattleFields.GetBattleField(battleFieldId);

            if (battleField.IsBattleRunning)
            {
                Robot robot = battleField.GetRobot(robotId);

                RobotActions.Rest(robot, restStrength);
            }
        }

        [HttpGet]
        public RobotStatus Check(string battleFieldId, string robotId)
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
