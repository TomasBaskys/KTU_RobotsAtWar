using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server.Weapons
{
    public interface IWeapon
    {
        int CalculateAttackDamage(Strength strength);
    }
}