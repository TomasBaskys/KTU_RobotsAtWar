using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace RobotsAtWar.Server.Readers
{
    public class RobotReader
    {
        public Robot GetRobotInfo(string robotId, PlayType playType = PlayType.Manual)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["RobotsAtWarDB"].ConnectionString))
            {
                var query = $@"
SELECT 
    R.ID_Robot AS RobotId,
    R.RobotName,
	S.Strategy
FROM 
    [dbo].[Robots] R
LEFT JOIN 
	[dbo].[Strategies] S ON S.ID_Robot = R.ID_Robot
WHERE 
    R.ID_Robot = '{robotId}'";

                return new Robot(db.Query(query).FirstOrDefault(), playType);
            }
        }
    }
}
