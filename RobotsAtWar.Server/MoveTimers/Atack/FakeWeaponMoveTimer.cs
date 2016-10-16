using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server.MoveTimers.Atack
{
    public class FakeWeaponMoveTimer : IAttackTimer
    {
        public void Sleep(Strength strength){}
        public void Stun(Strength strength){}
    }
}
