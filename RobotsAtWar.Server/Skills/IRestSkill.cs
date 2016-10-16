using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server.Skills
{
    public interface IRestSkill
    {
        RestOutcome Rest(int moveLength, WarriorState warriorState);
    }
}