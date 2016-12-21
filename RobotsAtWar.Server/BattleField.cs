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

        private readonly string _hostId;

        public List<Robot> Robots { get; }

        public readonly string RoomId;

        public bool IsBattleRunning { get; set; }

        public BattleField(string hostId, out string battleFieldId)
        {
            _hostId = hostId;
            Robots = new List<Robot>(BattleFieldCapacity);

            RoomId = battleFieldId = Guid.NewGuid().ToString();
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
            IsBattleRunning = true;
        }
    }
}
