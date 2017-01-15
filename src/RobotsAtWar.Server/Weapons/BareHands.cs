using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server.Weapons
{
    public class BareHands : Weapon
    {
        private static int _multiplier = 1;

        public BareHands() : base(_multiplier) { }
    }
}