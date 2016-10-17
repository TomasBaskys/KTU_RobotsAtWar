using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server
{
    public class RobotStatus
    {
        private readonly object _lockState = new object();
        private readonly object _lockState2 = new object();
        private RobotState _robotState;
        private int _life;

        public RobotState RobotState
        {
            get
            {
                lock (_lockState)
                {
                    return _robotState;
                }
            }
            set
            {
                lock (_lockState)
                {
                    _robotState = value;
                }
            }
        }

        public int Life
        {
            get
            {
                lock (_lockState2)
                {
                    return _life;
                }
            }

            set
            {
                lock (_lockState2)
                {
                    _life = value;
                }
            }
        }

        public RobotStatus()
        {
            RobotState = RobotState.Idle;
            Life = 100;
        }

    }
}