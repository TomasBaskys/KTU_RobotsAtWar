using System;
using log4net;
using RobotsAtWar.Server.Enums;
using RobotsAtWar.Server.MoveTimers;

namespace RobotsAtWar.Server
{
    public interface IShield
    {
        Response Defend(int time);
    }

    public abstract class BaseShield : IShield
    {
        public DateTime DefenceStartTime;
        protected IMoveTimer MoveTimer;

        public abstract Response Defend(int time);
    }

    public class BasicShield : BaseShield
    {
        public BasicShield(IMoveTimer moveTimer)
        {
            MoveTimer = moveTimer;
        }

        public override Response Defend(int time)
        {
            if (time < 1)
            {
                return Response.WrongData;
            }

            DefenceStartTime = DateTime.UtcNow;
            MoveTimer.Sleep(time);

            return Response.Success;
        }
    }
}
