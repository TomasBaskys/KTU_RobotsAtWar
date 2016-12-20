using System.Threading;
using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server
{
    public static class RobotActions
    {
        public static int Attack(Robot robot, ActionStrength attackStrength)
        {
            int damage;

            robot.State = RobotState.Attacking;

            Thread.Sleep((int)attackStrength * 1000);

            if (robot.State == RobotState.Attacking)
            {
                damage = MakeDamage(robot.Enemy, attackStrength);
                robot.State = RobotState.Idle;
            }
            else
            {
                damage = -1; //interupted
            }

            return damage;
        }

        private static int MakeDamage(Robot enemy, ActionStrength attackStrength)
        {
            var damage = 0;

            if (enemy.State != RobotState.Defending)
            {
                damage = CalculateInpact(attackStrength, 10);

                enemy.Status.Life -= damage;
                enemy.State = IsAlive(enemy) ? RobotState.Interrupted : RobotState.Dead;
            }

            return enemy.State != RobotState.Dead ? damage : -99; //dead;
        }

        public static void Defence(Robot robot, ActionStrength defenceStrength)
        {
            robot.State = RobotState.Defending;

            Thread.Sleep((int)defenceStrength * 1000);

            robot.State = RobotState.Idle;
        }

        public static int Rest(Robot robot, ActionStrength restStrength)
        {
            int healthToRestore = 0;

            robot.State = RobotState.Resting;

            Thread.Sleep((int)restStrength * 1000);

            if (robot.State == RobotState.Resting)
            {
                //Magic formula
                healthToRestore = CalculateInpact(restStrength, 5);
            }

            return healthToRestore;
        }

        public static RobotStatus Check(Robot robot)
        {
            return robot.Enemy.Status;
        }

        private static int CalculateInpact(ActionStrength actionStrength, int number)
        {
            return (number + ((int)actionStrength - 1) * 2) * (int)actionStrength;
        }

        private static bool IsAlive(Robot robot) => robot.Status.Life > 0;
    }
}