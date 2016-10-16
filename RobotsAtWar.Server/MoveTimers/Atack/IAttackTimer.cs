using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server.MoveTimers.Atack
{
    public interface IAttackTimer
    {
        void Sleep(Strength strength);
        void Stun(Strength strength);
    }
}