using NUnit.Framework;
using RobotsAtWar.Server.Shields;

namespace RobotsAtWar.tests
{
    [TestFixture]
    public class ShieldsTester
    {
        private HeavyShield _testHeavyShield;
        private LightShield _testLightShield;
        private NormalShield _testNormalShield;

        [SetUp]
        public void SetUp()
        {
            _testHeavyShield = new HeavyShield();
            _testLightShield = new LightShield();
            _testNormalShield = new NormalShield();
        }

        [TestCase(10, 2)]
        [TestCase(12, 3)]
        [TestCase(3, 0)]
        [TestCase(5, 1)]
        public void HeavyShieldTest(int damage, int expectation)
        {
            var mitigatedDamage = _testHeavyShield.MitigateDamage(damage);
            Assert.AreEqual(mitigatedDamage, expectation);
        }

        [TestCase(10, 6)]
        [TestCase(12, 7)]
        [TestCase(3, 1)]
        [TestCase(5, 3)]
        public void LightShieldTest(int damage, int expectation)
        {
            var mitigatedDamage = _testLightShield.MitigateDamage(damage);
            Assert.AreEqual(mitigatedDamage, expectation);
        }

        [TestCase(10, 6)]
        [TestCase(12, 7)]
        [TestCase(3, 1)]
        [TestCase(5, 3)]
        public void NormalShieldTest(int damage, int expectation)
        {
            var mitigatedDamage = _testNormalShield.MitigateDamage(damage);
            Assert.AreEqual(mitigatedDamage, expectation);
        }
    }
}
