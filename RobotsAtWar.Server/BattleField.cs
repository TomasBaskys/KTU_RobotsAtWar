using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RobotsAtWar.Server.Readers;

namespace RobotsAtWar.Server
{
    public class BattleField
    {
        private readonly int BattleFieldCapacity = 2;

        public readonly string HostRobotId;

        public string HostRobotName { get; set; }

        public List<Robot> Robots { get; }

        public BattleState BattleState { get; set; }

        public readonly string BattleId;

        public readonly string BattleName;

        public readonly RoomType BattleType;

        public BattleField()
        {
            Robots = new List<Robot>(BattleFieldCapacity);
        }

        public BattleField(string hostRobotId, string battleName, RoomType battleType, string battleId = null)
        {
            HostRobotId = hostRobotId;
            Robots = new List<Robot>(BattleFieldCapacity);

            BattleId = battleId ?? Guid.NewGuid().ToString();
            BattleName = battleName;
            BattleType = battleType;
            BattleState = BattleState.Pending;
        }

        public void RegisterRobot(string robotId, PlayType playType)
        {
            var reader = new RobotReader();
            Robot robot = reader.GetRobotInfo(robotId, playType);
            robot.Status = new RobotStatus();

            Robots.Add(robot);

            if (Robots.Count == BattleFieldCapacity)
            {
                SetRobotsEnemies();
            }

            robot.Ready = true;
        }

        public Robot GetRobot(string robotId)
        {
            return Robots.FirstOrDefault(r => r.RobotId == robotId);
        }

        public bool AreRobotsReady()
        {
            return Robots.All(r => r.Ready);
        }

        public bool IsBattleOver()
        {
            return !(Robots[0].Status.Life > 0 && Robots[1].Status.Life > 0);
        }

        public string GetWinner(string myGuid)
        {
            Robot aliveRobot = GetAliveRobot();

            return aliveRobot.RobotId;
        }

        private void SetRobotsEnemies()
        {
            Robots[0].Enemy = Robots[1];
            Robots[1].Enemy = Robots[0];
        }

        private Robot GetAliveRobot()
        {
            return Robots.FirstOrDefault(r => r.Status.Life > 0);
        }

        public async void StartBattle()
        {
            await Task.Delay(3000);
            BattleState = BattleState.Running;

            foreach (var robot in Robots)
            {
                if (robot.PlayType == PlayType.Auto)
                {
                    var strategyExecutor = new StrategyExecutor();
                    strategyExecutor.Start(robot, BattleId);
                }
            }
        }
    }

    public enum RoomType
    {
        Public,
        Private
    }

    public enum BattleState
    {
        Pending = 1,
        Running = 2,
        Finished = 3,
        Canceled = 4
    }
}
