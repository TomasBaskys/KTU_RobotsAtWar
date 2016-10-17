using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server.Weapons
{
    public class Axe : IWeapon
    {
        public int CalculateAttackDamage(ActionStrength actionStrength)
        {
            switch (actionStrength)
            {
                case ActionStrength.Weak:
                    return 1;
                case ActionStrength.Medium:
                    return 2;
                case ActionStrength.Strong:
                    return 3;
                default:
                    return 0;
            }
        }
    }
}