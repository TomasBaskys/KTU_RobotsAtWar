using System;
using System.Data.SqlClient;
//using Dapper;

namespace RobotsAtWar.Server
{
    public static class Leaderboard
    {
        private const string DbConnectionString =
           // "Data Source=10.2.40.195;Initial Catalog=RobotDB;User ID=sa;Password=zZz,.123";
            "Data Source=localhost;Initial Catalog=RobotDB;User ID=martynas;Password=databasepass";


        public static int GetLeaderboardPosition(string userGuid)
        {
            using (var connection = new SqlConnection(DbConnectionString))
            {
                connection.Open();

                var warriorSql = String.Format("SELECT * FROM Users WHERE UserID='{0}'", userGuid);
                //var warrior = null;connection.Execute(warriorSql);
                return 4;
            }
        }
    }
}
