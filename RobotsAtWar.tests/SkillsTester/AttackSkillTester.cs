using NUnit.Framework;
using RobotsAtWar.Server;
using RobotsAtWar.Server.Enums;
using RobotsAtWar.Server.MoveTimers.Atack;
using RobotsAtWar.Server.Skills;
using RobotsAtWar.Server.Weapons;

namespace RobotsAtWar.tests.SkillsTester
{
    [TestFixture]
    public class AttackSkillTester
    {
        private AttackSkill _testAttackSkill;

        [SetUp]
        public void SetUp()
        {
            _testAttackSkill = new AttackSkill(new FakeWeaponMoveTimer(), new Axe());
        }

        [Test]
        public void PotionTest()
        {
            //var attackOutcome = _testAttackSkill.Attack(Strength.Strong, new WarriorState(), new Warrior());
        }
    }
}