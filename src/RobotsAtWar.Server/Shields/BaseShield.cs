using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsAtWar.Server.Shields
{
    public abstract class BaseShield : IShield
    {
        public int MinDiceRoll;
        public int MaxDiceRoll = 5;
        public int DiceRollBaseline = 1;

        public abstract int MitigateDamage(int damage);
    }
}
