using System;
using System.Threading;
using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server
{
    public class Robot
    {
        public RobotState State { get; set; }
        public RobotStatus Status { get; set; }
        public Robot Enemy { get; set; }
        public bool Ready { get; set; }
        public Guid RobotId;
        public bool IsAlive => Status.Life > 0;

        public void Attack(ActionStrength attackStrength)
        {
            State = RobotState.Attacking;

            Thread.Sleep((int)attackStrength * 1000);

            if (State == RobotState.Attacking)
            {
                Enemy.TakeAttack(attackStrength);
                State = RobotState.Idle;
            }
        }

        private void TakeAttack(ActionStrength attackStrength)
        {
            if (State != RobotState.Defending)
            {
                Status.Life -= CalculateInpact(attackStrength, 10);

                State = IsAlive ? RobotState.Interrupted : RobotState.Dead;
            }
        }

        public void Defence(ActionStrength defenceStrength)
        {
            State = RobotState.Defending;

            Thread.Sleep((int)defenceStrength * 1000);

            State = RobotState.Idle;
        }

        public int Rest(ActionStrength restStrength)
        {
            int healthToRestore = 0;

            State = RobotState.Resting;

            Thread.Sleep((int)restStrength * 1000);

            if (State == RobotState.Resting)
            {
                //Magic formula
                healthToRestore = CalculateInpact(restStrength, 5);
            }

            return healthToRestore;
        }

        public RobotStatus Check()
        {
            return Enemy.Status;
        }

        private int CalculateInpact(ActionStrength actionStrength, int number)
        {
            return (number + ((int)actionStrength - 1) * 2) * (int)actionStrength;
        }
    }
}