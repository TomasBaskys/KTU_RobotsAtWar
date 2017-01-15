using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server.Shields
{
    public class HeavyShield : BaseShield, IShield
    {
        private const double KDamageMitigation = 0.25;
        public int MinDiceRoll = 1;
        public int MaxDiceRoll = 5;
        public int DiceRollBaseline = 1;


        public override int MitigateDamage(int damage)
        {
            var mitigatedDamage = (int)(damage*KDamageMitigation);
            return mitigatedDamage;
        }
    }
}