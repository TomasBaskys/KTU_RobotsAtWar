using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server.Weapons
{
    public class DoubleSidedAxe : Weapon
    {
        private static int _multiplier = 3;

        public DoubleSidedAxe() : base(_multiplier) { }
    }
}