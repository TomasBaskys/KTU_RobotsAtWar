using System;
using System.Collections.Generic;
using System.Linq;
using RobotsAtWar.Server.Readers;

namespace RobotsAtWar.Server
{
    public static class BattleFields
    {
        private static readonly IList<BattleField> Rooms = new List<BattleField>();

        public static string CreateBattleField(string hostId)
        {
            string battleFieldId;
            Rooms.Add(new BattleField(hostId, out battleFieldId));

            return battleFieldId;
        }

        public static void JoinBattleField(string battleFieldId, string robotId)
        {
            BattleField battleField = GetBattleField(battleFieldId);

            battleField.RegisterRobot(robotId);
        }

        public static IEnumerable<Battle> GetBattleFields()
        {
            var battleReader = new BattleReader();
            return battleReader.GetActiveBattles();
        }

        public static BattleField GetBattleField(string battleFieldId)
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
