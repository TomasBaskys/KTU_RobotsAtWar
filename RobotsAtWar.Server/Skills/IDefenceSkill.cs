using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server.Skills
{
    public interface IDefenceSkill
    {
        DefenceOutcome Defend(int timeToDefend, WarriorState warriorState);
        AttackOutcome GetAttacked(int damage, WarriorState warriorState, Warrior enemy);
    }
}