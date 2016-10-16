using System;
using log4net;
using log4net.Config;
using NUnit.Framework;
using RobotsAtWar.Server;
using RobotsAtWar.Server.Enums;
using RobotsAtWar.Server.MoveTimers.Atack;
using RobotsAtWar.Server.MoveTimers.Defend;
using RobotsAtWar.Server.MoveTimers.Rest;
using RobotsAtWar.Server.Potions;
using RobotsAtWar.Server.Shields;
using RobotsAtWar.Server.Skills;
using RobotsAtWar.Server.Weapons;

namespace RobotsAtWar.tests
{
    [TestFixture]
    public class WarriorTester
    {
        private static ILog _logger = LogManager.GetLogger(typeof(WarriorTester));
        private Warrior _testWarrior;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            XmlConfigurator.Configure();
        }

        [SetUp]
        public void SetUp()
        {

            _testWarrior = new Warrior(new Guid(), new AttackSkill(new BareHandsMoveTimer(), new BareHands()),
                    new DefenceSkill(new DefenceTimer(), new LightShield()),
                    new CheckSkill(),
                    new RestSkill(new RestTimer(), new Potion()))
            {
                Enemy = new Warrior(new Guid(), new AttackSkill(new BareHandsMoveTimer(), new BareHands()),
                    new DefenceSkill(new DefenceTimer(), new LightShield()),
                    new CheckSkill(),
                    new RestSkill(new RestTimer(), new Potion()))
            };
        }

        [TestCase(Strength.Weak, AttackOutcome.Success)]
        [TestCase(Strength.Medium, AttackOutcome.Success)]
        [TestCase(Strength.Strong, AttackOutcome.Success)]
        [TestCase((Strength)12, AttackOutcome.WrongData)]
        public void AttackMethodTest(Strength strength, AttackOutcome expectation)
        {
            var attackOutcome = _testWarrior.Attack(strength);
            _logger.InfoFormat("Attacked for {0}.", attackOutcome);
            Assert.AreEqual(attackOutcome, expectation);
        }

        [Test]
        public void Check()
        {
            var checkOutcome = _testWarrior.Check();
            Assert.AreEqual(checkOutcome.State, States.DoingNothing);
        }
    }
}
