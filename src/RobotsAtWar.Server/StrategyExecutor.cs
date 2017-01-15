using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RobotsAtWar.Server
{
    public class StrategyExecutor
    {
        public void Start(Robot robot, string battleFieldId)
        {
            BattleField battle = BattleFields.GetBattleField(battleFieldId);

            int index = 0;
            List<RobotTurn> strategy = robot.RobotStrategy.Strategy;

            while (battle.BattleState == BattleState.Running)
            {
                var robotTurn = strategy[index % strategy.Count];

                switch (robotTurn.Action.ToString())
                {
                    case "Attack":
                        RobotActions.Attack(robot, robotTurn.Level);
                        break;
                    case "Defence":
                        RobotActions.Defence(robot, robotTurn.Level);
                        break;
                    case "Rest":
                        RobotActions.Rest(robot, robotTurn.Level);
                        break;
                    default:
                        throw new InvalidOperationException($"Action '{robotTurn.Action}' is invalid. Possible actions: 'Attack', 'Defence', 'Rest'.'");
                }

                Thread.Sleep(500);

                index++;
            }
        }
    }
}