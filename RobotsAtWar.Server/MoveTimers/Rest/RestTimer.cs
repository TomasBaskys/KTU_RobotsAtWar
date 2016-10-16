using System.Threading;

namespace RobotsAtWar.Server.MoveTimers.Rest
{
    public class RestTimer : IRestTimer
    {
        private const int MoveTimeInMiliseconds = 100;

        public void Sleep(int moveLength)
        {
            for (int i = 0; i < moveLength; i++)
            {
                Thread.Sleep(MoveTimeInMiliseconds);                
            }
        }
    }
}