using System.Threading;
using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server
{
    public static class RobotActions
    {
        public static int Attack(Robot robot, ActionStrength attackStrength)
        {
            int damage;

            robot.Status.RobotState= RobotState.Attacking;

            Thread.Sleep((int)attackStrength * 1000);

            if (robot.Status.RobotState == RobotState.Attacking)
            {
                damage = MakeDamage(robot.Enemy, attackStrength);
                robot.Status.RobotState = RobotState.Idle;
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

            if (enemy.Status.RobotState != RobotState.Defending)
            {
                damage = CalculateInpact(attackStrength, 10);

                enemy.Status.Life -= damage;
                enemy.Status.RobotState = IsAlive(enemy) ? RobotState.Interrupted : RobotState.Dead;
                enemy.Status.LastReceivedDamage = damage;
            }

            return enemy.Status.RobotState != RobotState.Dead ? damage : -99; //dead;
        }

        public static void Defence(Robot robot, ActionStrength defenceStrength)
        {
            robot.Status.RobotState = RobotState.Defending;

            Thread.Sleep((int)defenceStrength * 1000);

            robot.Status.RobotState = RobotState.Idle;
        }

        public static int Rest(Robot robot, ActionStrength restStrength)
        {
            int healthToRestore = 0;

            robot.Status.RobotState = RobotState.Resting;

            Thread.Sleep((int)restStrength * 1000);

            if (robot.Status.RobotState == RobotState.Resting)
            {
                //Magic formula
                healthToRestore = CalculateInpact(restStrength, 5);
            }

            return healthToRestore;
        }

        private static int CalculateInpact(ActionStrength actionStrength, int number)
        {
            return (number + ((int)actionStrength - 1) * 2) * (int)actionStrength;
        }

        private static bool IsAlive(Robot robot) => robot.Status.Life > 0;
    }
}