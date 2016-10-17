using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server.Weapons
{
    public interface IWeapon
    {
        int CalculateAttackDamage(ActionStrength actionStrength);
    }
}