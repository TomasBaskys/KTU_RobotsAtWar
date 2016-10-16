using System.Threading;

namespace RobotsAtWar.Server.MoveTimers.Defend
{
    public class DefenceTimer:IDefenceTimer
    {
        private const int MoveTimeInMiliseconds = 100;

        public void Sleep(int moveLength)
        {
            for (var i = 0; i < moveLength + 1; i++)
            {
                Thread.Sleep(MoveTimeInMiliseconds);
            }
        }
    }
}