using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server.Skills
{
    public interface IAttackSkill
    {
        AttackOutcome Attack(Strength strength, WarriorState warriorState, Warrior enemy);
    }
}