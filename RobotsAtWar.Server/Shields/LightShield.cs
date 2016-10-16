using System.Security.Cryptography;
using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server.Shields
{
    public class LightShield : BaseShield
    {
        private const double KDamageMitigation = 0.65;
        public int MinDiceRoll = 1;
        public int MaxDiceRoll = 6;
        public int DiceRollBaseline = 2;


        public override int MitigateDamage(int damage)
        {
            return (int)(damage*KDamageMitigation);
        }
    }
}