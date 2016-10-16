using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server.Shields
{
    public class NormalShield : BaseShield
    {
        private const double KDamageMitigation = 0.65;
        public int MinDiceRoll = 1;
        public int MaxDiceRoll = 3;
        public int DiceRollBaseline = 1;

        public override int MitigateDamage(int damage)
        {
            return (int) (KDamageMitigation*damage);
        }
    }
}
