using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server.Weapons
{
    public class BareHands : IWeapon
    {
        public int CalculateAttackDamage(Strength strength)
        {
            switch (strength)
            {
                case Strength.Weak:
                    return 1;
                case Strength.Medium:
                    return 2;
                case Strength.Strong:
                    return 3;
                default:
                    return 0;
            }
        }
    }
}