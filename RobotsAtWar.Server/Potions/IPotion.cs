using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server.Potions
{
    public interface IPotion
    {
        RestOutcome Heal(int moveLength, WarriorState warriorState);
    }
}