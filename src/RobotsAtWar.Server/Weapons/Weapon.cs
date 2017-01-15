using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server.Weapons
{
    public abstract class Weapon
    {
        private readonly int _multiplier;

        protected Weapon(int multiplier)
        {
            _multiplier = multiplier;
        }

        public int CalculateAttackDamage(ActionStrength actionStrength)
        {
            switch (actionStrength)
            {
                case ActionStrength.Weak:
                    return 1 * _multiplier;
                case ActionStrength.Medium:
                    return 2 * _multiplier;
                case ActionStrength.Strong:
                    return 3 * _multiplier;
                default:
                    return 0;
            }
        }
    }
}