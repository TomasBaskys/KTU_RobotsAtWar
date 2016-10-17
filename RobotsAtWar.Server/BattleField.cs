using System;
using System.Collections.Generic;
using System.Linq;

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
            Robots.Add(new Robot());

            if (Robots.Count == BattleFieldCapacity)
            {
                SetRobotsEnemies();
            }
        }

        public Robot GetRobot(Guid robotId)
        {
            return Robots.FirstOrDefault(r => r.RobotId == robotId);
        }

        public bool AreRobotsReady()
        {
            return Robots.All(r => r.Ready);
        }

        public bool IsBattleOver()
        {
            return !(Robots[0].IsAlive && Robots[1].IsAlive);
        }

        public Guid GetWinner(Guid myGuid)
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
            return Robots.FirstOrDefault(r => r.IsAlive);
        }
    }
}
