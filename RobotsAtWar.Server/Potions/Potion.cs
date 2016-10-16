using System;
using RobotsAtWar.Server.Enums;

namespace RobotsAtWar.Server.Potions
{
    public class Potion : IPotion
    {
        public RestOutcome Heal(int moveLength, WarriorState warriorState)
        {
            if (warriorState.State != States.Interrupted)
            {
                warriorState.Life += (int) Math.Pow(2, moveLength - 1);
                return RestOutcome.Success;
            }
            return RestOutcome.Interrupted;
        }
    }
}