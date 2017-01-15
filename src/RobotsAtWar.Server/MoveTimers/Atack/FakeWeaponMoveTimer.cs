using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server.MoveTimers.Atack
{
    public class FakeWeaponMoveTimer : IAttackTimer
    {
        public void Sleep(ActionStrength actionStrength){}
        public void Stun(ActionStrength actionStrength){}
    }
}
