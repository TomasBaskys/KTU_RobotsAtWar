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

            if (battleField.BattleState == BattleState.Running)
            {
                Robot robot = battleField.GetRobot(robotId);

                damage = RobotActions.Attack(robot, attackStrength);
            }
            else
            {
                damage = -99; //battle not running
            }

            return damage;
        }

        [HttpGet]
        public void Defence(string battleFieldId, string robotId, ActionStrength defenceStrength)
        {
            BattleField battleField = BattleFields.GetBattleField(battleFieldId);

            if (battleField.BattleState == BattleState.Running)
            {
                Robot robot = battleField.GetRobot(robotId);

                RobotActions.Defence(robot, defenceStrength);
            }
        }

        [HttpGet]
        public int Rest(string battleFieldId, string robotId, ActionStrength restStrength)
        {
            var healPoints = 0;

            BattleField battleField = BattleFields.GetBattleField(battleFieldId);

            if (battleField.BattleState == BattleState.Running)
            {
                Robot robot = battleField.GetRobot(robotId);

                healPoints = RobotActions.Rest(robot, restStrength);
            }

            return healPoints;
        }

        [HttpGet]
        public RobotStatus Check(string battleFieldId, string robotId)
        {
            BattleField battleField = BattleFields.GetBattleField(battleFieldId);

            if (battleField.BattleState == BattleState.Running)
            {
                Robot robot = battleField.GetRobot(robotId);

                return RobotActions.Check(robot);
            }

            return null;
        }
    }
}

#pragma warning restore 1591
