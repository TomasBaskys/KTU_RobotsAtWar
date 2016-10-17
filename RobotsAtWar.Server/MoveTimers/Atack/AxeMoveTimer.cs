using System.Threading;
using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server.MoveTimers.Atack
{
    public class AxeMoveTimer:IAttackTimer
    {
        private const int MoveTimeInMiliseconds = 150;
        private const int StunTimeInMiliseconds = 70;

        void IAttackTimer.Sleep(ActionStrength actionStrength)
        {
            for (var i = 0; i < ((int)actionStrength) + 1; i++)
            {
                Thread.Sleep(MoveTimeInMiliseconds);
            }
        }

        void IAttackTimer.Stun(ActionStrength actionStrength)
        {
            for (var i = 0; i < ((int) actionStrength) + 1; i++)
            {
                Thread.Sleep(StunTimeInMiliseconds);
            }
        }
    }
}