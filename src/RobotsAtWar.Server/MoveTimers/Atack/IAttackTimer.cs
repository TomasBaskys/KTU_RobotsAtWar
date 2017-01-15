using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server.MoveTimers.Atack
{
    public interface IAttackTimer
    {
        void Sleep(ActionStrength actionStrength);
        void Stun(ActionStrength actionStrength);
    }
}