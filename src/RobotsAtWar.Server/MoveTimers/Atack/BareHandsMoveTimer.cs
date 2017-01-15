using System.Threading;
using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server.MoveTimers.Atack
{
    public class BareHandsMoveTimer : IAttackTimer
    {
        private const int MoveTimeInMiliseconds = 100;
        private const int StunTimeInMiliseconds = 50;

        void IAttackTimer.Sleep(ActionStrength actionStrength)
        {
            for (var i = 0; i < ((int)actionStrength); i++)
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