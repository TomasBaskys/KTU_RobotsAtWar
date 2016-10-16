using NUnit.Framework;
using RobotsAtWar.Server.Enums;
using RobotsAtWar.Server.Weapons;

namespace RobotsAtWar.tests
{
    [TestFixture]
    public class WeaponsTester
    {
        private Axe _testAxe;
        private BareHands _bareHands;
        private DoubleSidedAxe _doubleSidedAxe;

        [SetUp]
        public void SetUp()
        {
            _testAxe = new Axe();
            _bareHands = new BareHands();
            _doubleSidedAxe = new DoubleSidedAxe();
        }

        [TestCase(Strength.Weak, 1)]
        [TestCase(Strength.Medium, 2)]
        [TestCase(Strength.Strong, 3)]
        [TestCase((Strength)12, 0)]
        public void AxeTest(Strength strength, int expectation)
        {
            var attackDamage = _testAxe.CalculateAttackDamage(strength);
            Assert.AreEqual(attackDamage, expectation);
        }

        [TestCase(Strength.Weak, 1)]
        [TestCase(Strength.Medium, 2)]
        [TestCase(Strength.Strong, 3)]
        [TestCase((Strength)12, 0)]
        public void BareHandsTest(Strength strength, int expectation)
        {
            var attackDamage = _bareHands.CalculateAttackDamage(strength);
            Assert.AreEqual(attackDamage, expectation);
        }

        [TestCase(Strength.Weak, 1)]
        [TestCase(Strength.Medium, 2)]
        [TestCase(Strength.Strong, 3)]
        [TestCase((Strength)12, 0)]
        public void DoubleSideAxeTest(Strength strength, int expectation)
        {
            var attackDamage = _doubleSidedAxe.CalculateAttackDamage(strength);
            Assert.AreEqual(attackDamage, expectation);
        }
    }
}
