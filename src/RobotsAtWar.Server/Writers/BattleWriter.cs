using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace RobotsAtWar.Server.Writers
{
    public class BattleWriter
    {
        public void AddBattle(BattleField battleField)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["RobotsAtWarDB"].ConnectionString))
            {
                var query = $@"
INSERT INTO [dbo].[ActiveBattles] ( 
    [ID_HostRobot],
    [ID_Battle],
    [BattleName],
    [ID_BattleState],
    [BattleType])
VALUES (
    '{battleField.HostRobotId}',
    '{battleField.BattleId}',
    '{battleField.BattleName}',
    {(int)battleField.BattleState},
    '{battleField.BattleType}')";

                db.Execute(query);
            }
        }
    }
}
