using System;
using System.Collections.Generic;
using NUnit.Framework;
using RobotsAtWar.Server.Enums;
using Action = RobotsAtWar.Server.Enums.Action;

namespace RobotsAtWar.Server.Test
{
    [TestFixture]
    public class RobotActionsTests
    {
        private static Robot _robot;
        private static Robot _enemy;

        private static ActionStrength _actionStrength;

        [OneTimeSetUp]
        public static void Setup()
        {
            _robot = new Robot("TestId1", "TestUser1", "{\"Strategy\":[{\"Action\":\"Attack\",\"Level\":\"Weak\"}]}")
            {
                PlayType = PlayType.Auto,
                Status = new RobotStatus()
            };

            _enemy = new Robot("TestId2", "TestUser2", "{\"Strategy\":[{\"Action\":\"Rest\",\"Level\":\"Medium\"}]}")
            {
                PlayType = PlayType.Auto,
                Status = new RobotStatus()
            };

            _robot.Enemy = _enemy;
            _enemy.Enemy = _robot;

            _actionStrength = ActionStrength.Medium;
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.DoesNotThrow(() => new RobotActions(_robot, _actionStrength));
        }

        [Test]
        public void ConstructorWithoutRobotTest()
        {
            Assert.Throws<ArgumentNullException>(() => new RobotActions(null, _actionStrength));
        }

        [TestCase(Action.Attack, 24)]
        [TestCase(Action.Defence, 0)]
        [TestCase(Action.Rest, 14)]
        public void ExecuteActionTest(Action action, int expectedResult)
        {
            var robotActions = new RobotActions(_robot, _actionStrength);

            robotActions.SetAction(action);
            var result = robotActions.GetResult();

            Assert.AreEqual(expectedResult, result);
        }

        [TestCaseSource("ActionCommands")]
        public void ActionCommandPatternTest(KeyValuePair<ActionCommand, int> actionResultPair)
        {
            var result = actionResultPair.Key.Execute();

            Assert.AreEqual(actionResultPair.Value, result);
        }

        private static Dictionary<ActionCommand, int> ActionCommands()
        {
            Setup();

            var robotActions = new RobotActions(_robot, _actionStrength);

            return new Dictionary<ActionCommand, int>
            {
                { new AttackAction(robotActions), 24 },
                { new DefenceAction(robotActions), 0 },
                { new RestAction(robotActions), 14 }
            };
        }
    }
}
