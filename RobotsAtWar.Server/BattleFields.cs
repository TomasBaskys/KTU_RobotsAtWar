using System;
using System.Collections.Generic;
using System.Linq;

namespace RobotsAtWar.Server
{
    public static class BattleFields
    {
        private static readonly IList<BattleField> Rooms = new List<BattleField>();

        public static Guid CreateBattleField(Guid hostId)
        {
            Guid battleFieldId;
            Rooms.Add(new BattleField(hostId, out battleFieldId));

            return battleFieldId;
        }

        public static void JoinBattleField(Guid battleFieldId, Guid robotId)
        {
            BattleField battleField = GetBattleField(battleFieldId);

            battleField.RegisterRobot(robotId);
        }

        public static IList<BattleField> GetBattleFields()
        {
            return Rooms;
        }

        public static BattleField GetBattleField(Guid battleFieldId)
        {
            BattleField battleField = Rooms.FirstOrDefault(r => r.RoomId == battleFieldId);

            if (battleField == null)
            {
                throw new ArgumentException($"Battle Field with id: '{battleFieldId}' does not exists.");
            }

            return battleField;
        }
    }
}
