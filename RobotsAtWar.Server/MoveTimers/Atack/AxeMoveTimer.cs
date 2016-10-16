using System.Threading;
using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server.MoveTimers.Atack
{
    public class AxeMoveTimer:IAttackTimer
    {
        private const int MoveTimeInMiliseconds = 150;
        private const int StunTimeInMiliseconds = 70;

        void IAttackTimer.Sleep(Strength strength)
        {
            for (var i = 0; i < ((int)strength) + 1; i++)
            {
                Thread.Sleep(MoveTimeInMiliseconds);
            }
        }

        void IAttackTimer.Stun(Strength strength)
        {
            for (var i = 0; i < ((int) strength) + 1; i++)
            {
                Thread.Sleep(StunTimeInMiliseconds);
            }
        }
    }
}