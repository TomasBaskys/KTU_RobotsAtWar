using NUnit.Framework;

namespace RobotsAtWar.Server.Test
{
    [TestFixture]
    public class RobotActionsTests
    {
        private static Robot _robot;
        private static Robot _enemy;

        [SetUp]
        public void Setup()
        {
            _robot = new Robot();
            _enemy = new Robot();

            _robot.Enemy = _enemy;
            _enemy.Enemy = _robot;
        }
    }
}
