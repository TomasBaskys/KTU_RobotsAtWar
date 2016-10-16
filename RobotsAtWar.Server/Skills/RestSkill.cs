using RobotsAtWar.Server.Enums;
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

        public RestOutcome Rest(int moveLength, WarriorState warriorState)
        {
            if (moveLength > 5 || moveLength < 1)
                return RestOutcome.WrongData;
            _restTimer.Sleep(moveLength);
            return warriorState.State != States.Interrupted ? RestOutcome.Success : RestOutcome.Interrupted;
        }
    }
}