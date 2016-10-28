using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server
{
    public class Robot
    {
        public string RobotId { get; set; }

        public string RobotName { get; set; }

        public RobotState State { get; set; }

        public RobotStatus Status { get; set; }

        public bool Ready { get; set; }

        public Robot Enemy { get; set; }
    }
}