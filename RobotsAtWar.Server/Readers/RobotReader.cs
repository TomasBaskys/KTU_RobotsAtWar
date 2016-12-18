using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace RobotsAtWar.Server.Readers
{
    public class RobotReader
    {
        public Robot GetRobotInfo(string robotId)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["RobotsAtWarDB"].ConnectionString))
            {
                var query = $@"
SELECT 
    ID_Robot AS RobotId,
    RobotName
FROM 
    [dbo].[Robots] 
WHERE 
    ID_Robot = '{robotId}'";

                return db.Query<Robot>(query).FirstOrDefault();
            }
        }
    }
}
