using System.Web.Http;
using RobotsAtWar.Server;
using RobotsAtWar.Server.Enums;

#pragma warning disable 1591

namespace RobotsAtWar.WebApi.Controllers
{
    public class ActionsController : ApiController
    {
        private IReceiver _robotAction;

        [HttpGet]
        public int Attack(string battleFieldId, string robotId, ActionStrength attackStrength)
        {
            int damage;
            BattleField battleField = BattleFields.GetBattleField(battleFieldId);

            if (battleField.BattleState == BattleState.Running)
            {
                Robot robot = battleField.GetRobot(robotId);

                _robotAction = new RobotActions(robot, attackStrength);
                damage = new AttackAction(_robotAction).Execute();
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

                _robotAction = new RobotActions(robot, defenceStrength);
                new DefenceAction(_robotAction).Execute();
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

                _robotAction = new RobotActions(robot, restStrength);
                healPoints = new RestAction(_robotAction).Execute();
            }

            return healPoints;
        }
    }
}

#pragma warning restore 1591
