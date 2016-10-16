using System;
using System.Collections.Generic;

namespace RobotsAtWar.Server
{
    public class BattleFieldSingleton
    {
        public static Dictionary<Guid, BattleField> BattleFieldByGuid = new Dictionary<Guid, BattleField>();

        public static bool ContainsBattleField(Guid opponentGuid)
        {
            return BattleFieldByGuid.ContainsKey(opponentGuid);
        }

        public static BattleField GetBattleFieldByGuid(Guid guid)
        {
            return BattleFieldByGuid[guid];
        }

    }
}
