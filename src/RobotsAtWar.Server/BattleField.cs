using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RobotsAtWar.Server.Readers;

namespace RobotsAtWar.Server
{
    public class BattleField
    {
        private readonly int BattleFieldCapacity = 2;

        public string HostRobotId { get; set; }

        public string HostRobotName { get; set; }

        public List<Robot> Robots { get; }

        public BattleState BattleState { get; set; }

        public string BattleId { get; set; }

        public string BattleName { get; set; }

        public RoomType BattleType { get; set; }

        public BattleField(string hostRobotId, string battleName, RoomType battleType, string battleId = null)
        {
            HostRobotId = hostRobotId;
            Robots = new List<Robot>(BattleFieldCapacity);

            BattleId = battleId ?? Guid.NewGuid().ToString();
            BattleName = battleName;
            BattleType = battleType;
            BattleState = BattleState.Pending;
        }

        public BattleField(string battleId, string hostRobotId, string battleName, string hostRobotName, string battleType)
        {
            this.BattleId = battleId;
            this.HostRobotId = hostRobotId;
            this.BattleName = battleName;
            this.HostRobotName = hostRobotName;

            RoomType battleTypeEnum;
            Enum.TryParse(battleType, out battleTypeEnum);
            this.BattleType = battleTypeEnum;
        }

        public void RegisterRobot(string robotId, PlayType playType)
        {
            var reader = new RobotReader();

            Robot robot = reader.GetRobotInfo(robotId);

            robot.PlayType = playType;
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

        private void SetRobotsEnemies()
        {
            Robots[0].Enemy = Robots[1];
            Robots[1].Enemy = Robots[0];
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
                    new Thread(() => strategyExecutor.Start(robot, BattleId)).Start();
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

