/*using RobotsAtWar.Server.Enums;
using RobotsAtWar.Server.MoveTimers.Rest;
using RobotsAtWar.Server.Potions;

namespace RobotsAtWar.Server.Skills
{
    public class RestSkill : IRestSkill
    {
        private readonly IRestTimer _restTimer;
        private IPotion _potion;

        public RestSkill(IRestTimer timer, IPotion potion)
        {
            _restTimer = timer;
            _potion = potion;
        }

        public RestResult Rest(int moveLength, RobotStatus robotStatus)
        {
            if (moveLength > 5 || moveLength < 1)
                return RestResult.WrongData;
            _restTimer.Sleep(moveLength);
            return robotStatus.RobotState != RobotState.Interrupted ? RestResult.Success : RestResult.Interrupted;
        }
    }
}*/