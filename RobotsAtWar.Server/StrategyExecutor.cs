using System;
using System.Collections.Generic;

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
                    case "Check":
                        RobotActions.Check(robot);
                        break;
                    default:
                        throw new InvalidOperationException($"Action '{robotTurn.Action}' is invalid. Possible actions: 'Attack', 'Defence', 'Rest', 'Check'.'");
                }

                index++;
            }
        }
    }
}