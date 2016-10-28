using System;
using System.Collections.Generic;
using System.Linq;
using RobotsAtWar.Server.Readers;

namespace RobotsAtWar.Server
{
    public class BattleField
    {
        private readonly int BattleFieldCapacity = 2;

        private readonly Guid _hostId;
        public List<Robot> Robots { get; }

        public Guid RoomId;
        public bool IsBattleRunning { get; set; }

        public BattleField(Guid hostId, out Guid battleFieldId)
        {
            _hostId = hostId;
            Robots = new List<Robot>(BattleFieldCapacity);

            RoomId = battleFieldId = Guid.NewGuid();
        }

        public void RegisterRobot(Guid robotId)
        {
            var reader = new RobotReader();
            Robot robot = reader.GetRobotInfo(robotId);

            Robots.Add(robot);

            if (Robots.Count == BattleFieldCapacity)
            {
                SetRobotsEnemies();
            }
        }

        public Robot GetRobot(Guid robotId)
        {
            return Robots.FirstOrDefault(r => Guid.Parse(r.RobotId) == robotId);
        }

        public bool AreRobotsReady()
        {
            return Robots.All(r => r.Ready);
        }

        public bool IsBattleOver()
        {
            return !(Robots[0].Status.Life > 0 && Robots[1].Status.Life > 0);
        }

        public Guid GetWinner(Guid myGuid)
        {
            Robot aliveRobot = GetAliveRobot();

            return Guid.Parse(aliveRobot.RobotId);
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
    }
}
