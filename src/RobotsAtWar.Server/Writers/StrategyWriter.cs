using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RobotsAtWar.Server.Writers
{
    public class StrategyWriter
    {
        public void UpdateStrategy(string robotId, RobotStrategy strategy)
        {
            string strategyJson = JsonConvert.SerializeObject(strategy, new StringEnumConverter());

            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["RobotsAtWarDB"].ConnectionString))
            {
                var query = $@"
IF EXISTS (SELECT 1 FROM dbo.Strategies WHERE ID_Robot = '{robotId}')
BEGIN
    UPDATE 
        [dbo].[Strategies]
    SET 
        [Strategy] = '{strategyJson}'
    WHERE 
        [ID_Robot] = '{robotId}'
END
ELSE
BEGIN
    INSERT INTO [dbo].[Strategies] (
        [ID_Robot],
        [Strategy])
     VALUES (
        '{robotId}',
        '{strategyJson}'
     )
END";

                db.Execute(query);
            }
        }
    }
}