/*using System.Threading;
using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server
{
    public class RobotActions
    {
        private readonly Robot _robot;

        protected RobotActions(Robot robot)
        {
            _robot = robot;
        }

        public void Attack(ActionStrength attackStrength)
        {
            _robot.State = RobotState.Attacking;

            Thread.Sleep((int)attackStrength * 1000);

            if (_robot.State == RobotState.Attacking)
            {
                _robot.Enemy.TakeAttack(attackStrength);
                _robot.State = RobotState.Idle;
            }
        }

        private void TakeAttack(ActionStrength attackStrength)
        {
            if (_robot.State != RobotState.Defending)
            {
                _robot.Status.Life -= CalculateInpact(attackStrength, 10);

                _robot.State = _robot.IsAlive ? RobotState.Interrupted : RobotState.Dead;
            }
        }

        public void Defence(ActionStrength defenceStrength)
        {
            _robot.State = RobotState.Defending;

            Thread.Sleep((int)defenceStrength * 1000);

            _robot.State = RobotState.Idle;
        }

        public int Rest(ActionStrength restStrength)
        {
            int healthToRestore = 0;

            _robot.State = RobotState.Resting;

            Thread.Sleep((int)restStrength * 1000);

            if (_robot.State == RobotState.Resting)
            {
                //Magic formula
                healthToRestore = CalculateInpact(restStrength, 5);
            }

            return healthToRestore;
        }

        public RobotStatus Check()
        {
            return _robot.Enemy.Status;
        }

        public bool IsAlive()
        {
            return _robot.Status.Life > 0;
        }

        private int CalculateInpact(ActionStrength actionStrength, int number)
        {
            return (number + ((int)actionStrength - 1) * 2) * (int)actionStrength;
        }
    }
}*/