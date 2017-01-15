using System;
using System.Collections.Generic;
using System.Threading;

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

                IReceiver robotAction = new RobotActions(robot, robotTurn.Level);
                ActionCommand command;

                switch (robotTurn.Action.ToString())
                {
                    case "Attack":
                        command = new AttackAction(robotAction);
                        break;
                    case "Defence":
                        command = new DefenceAction(robotAction);
                        break;
                    case "Rest":
                        command = new RestAction(robotAction);
                        break;
                    default:
                        throw new InvalidOperationException($"Action '{robotTurn.Action}' is invalid. Possible actions: 'Attack', 'Defence', 'Rest'.'");
                }

                command.Execute();

                Thread.Sleep(500);

                index++;
            }
        }
    }
}