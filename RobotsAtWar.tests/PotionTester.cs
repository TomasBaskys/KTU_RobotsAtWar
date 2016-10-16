using NUnit.Framework;
using RobotsAtWar.Server;
using RobotsAtWar.Server.Enums;
using RobotsAtWar.Server.Potions;

namespace RobotsAtWar.tests
{
    [TestFixture]
    public class PotionTester
    {
        private Potion _testPotion;
        private WarriorState _testWarriorState;

        [SetUp]
        public void SetUp()
        {
            _testPotion = new Potion(); 
            _testWarriorState = new WarriorState();
            _testWarriorState.State = States.Interrupted;
        }

        [TestCase(States.DoingNothing, RestOutcome.Success)]
        [TestCase(States.Stunned, RestOutcome.Success)]
        [TestCase(States.Interrupted, RestOutcome.Interrupted)]
        public void PotionTest(States state, RestOutcome expectation)
        {
            _testWarriorState.State = state;
            var restOutcome = _testPotion.Heal(3, _testWarriorState);
            Assert.AreEqual(restOutcome, expectation);
        }
    }
}