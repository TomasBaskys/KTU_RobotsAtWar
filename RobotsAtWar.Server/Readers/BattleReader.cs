using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace RobotsAtWar.Server.Readers
{
    public class BattleReader
    {
        public IEnumerable<Battle> GetActiveBattles()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["RobotsAtWarDB"].ConnectionString))
            {
                var query = $@"
SELECT 
    AB.[ID_Battle] AS BattleId,
    AB.[ID_HostRobot] AS HostRobotId,
    AB.[BattleName] AS BattleName,
    R.[RobotName] AS HostRobotName
FROM 
    [dbo].[ActiveBattles] AB
JOIN
    [dbo].[Robots] R ON R.[ID_Robot] = AB.[ID_HostRobot]
WHERE
    AB.[ID_BattleState] = 1"; //Pending

                return db.Query<Battle>(query);
            }
        }
    }
}
