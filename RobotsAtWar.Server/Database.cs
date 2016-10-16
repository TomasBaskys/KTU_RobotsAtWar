using System;
using System.Data.SqlClient;
using RobotsAtWar.Server.Tools;

namespace RobotsAtWar.Server
{
    public class Database
    {
        private static readonly string _connectionString =
            "Data Source=" + ConfigSettings.ReadSetting("DatabaseIP") +
            ";Initial Catalog=" + ConfigSettings.ReadSetting("DatabaseName") +
            ";User ID=" + ConfigSettings.ReadSetting("UserID") +
            ";Password=" + ConfigSettings.ReadSetting("UserPassword");


        //private static string constr = "Data Source=10.2.40.195; Initial Catalog=RobotDB; User ID=sa; Password=zZz,.123"; // adform
      //  private static string _connectionString = "Data Source=Win7\\SQLEXPRESS; Initial Catalog=RobotDB; Integrated Security=true;"; // local DB

        /// <summary>
        /// Check in database if user exists;
        /// </summary>
        /// <param name="userGuid">
        /// GUID of the existing user.
        /// </param>
        public static bool UserExists(Guid userGuid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand();
                command.CommandText = "SELECT * FROM Users WHERE UserID = @pUserID";
                command.Parameters.AddWithValue("@pUserID", userGuid);
                command.Connection = connection;

                try
                {
                    command.Connection.Open();
                    var dataReader = command.ExecuteReader();
                    //dataReader.Read();
                    if (dataReader.HasRows)
                        return true;
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Error occurred: " + exception.Message);
                }
            }

            return false;
        }

        //return users with specified Guid freeText from database;
        public static string GetUserFreeText(Guid userGuid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand();
                command.CommandText = "SELECT Text FROM Users WHERE UserID = @pUserID";
                command.Parameters.AddWithValue("@pUserID", userGuid);
                command.Connection = connection;

                try
                {
                    command.Connection.Open();
                    var dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        dataReader.Read();
                        return dataReader["Text"].ToString();
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Error occurred: " + exception.Message);
                }
            }

            return String.Empty;
        }

        //Delete user from unconfirmed users list in database;
        public static void DeleteUser(string userGuid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand();
                command.CommandText = "DELETE FROM Users WHERE UserID = @pUserID";
                command.Parameters.AddWithValue("@pUserID", new Guid(userGuid));
                command.Connection = connection;

                try
                {
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Error occurred: " + exception.Message);
                }
            }
        }

        //Add user to database;
        public static void AddUser(string email, string freeText, string userGuid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand();
                command.CommandText = "INSERT INTO Users (UserID, Email, Text) VALUES (@pUserID, @pEmail, @pText)";
                command.Parameters.AddWithValue("@pUserID", new Guid(userGuid));
                command.Parameters.AddWithValue("@pEmail", email);
                command.Parameters.AddWithValue("@pText", freeText);
                command.Connection = connection;

                try
                {
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Error occurred: " + exception.Message);
                }
            }
        }

        public static void ConfirmUser(Guid userGuid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand();
                command.CommandText = "UPDATE Users SET IsConfirmed = @pIsConfirmed WHERE UserID = @pUserID";
                command.Parameters.AddWithValue("@pUserID", userGuid);
                command.Parameters.AddWithValue("@pIsConfirmed", 1);
                command.Connection = connection;

                try
                {
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Error occurred: " + exception.Message);
                }
            }
        }

        //Add one to WinsInARow counter;
        public static void AddWin(Guid userGuid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand();
                command.CommandText = "SELECT WinsInARow FROM Users WHERE UserID = @pUserID";
                command.Parameters.AddWithValue("@pUserID", userGuid);
                command.Connection = connection;

                try
                {
                    command.Connection.Open();
                    var dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                        dataReader.Read();

                    command.CommandText = "UPDATE Users SET WinsInARow = @pWinsInARow WHERE UserID = @pUserID";
                    command.Parameters.AddWithValue("@pWinsInARow", dataReader.GetInt32(0) + 1);
                    dataReader.Close(); // must be closed before calling ExecuteNonQuery() or ExecuteReader() again
                    command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Error occurred: " + exception.Message);
                }
            }
        }

        //Reset WinsInARow counter to zero;
        public static void ResetWins(Guid userGuid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand();
                command.CommandText = "UPDATE Users SET WinsInARow = @pWinsInARow WHERE UserID = @pUserID";
                command.Parameters.AddWithValue("@pUserID", userGuid);
                command.Parameters.AddWithValue("@pWinsInARow", 0);
                command.Connection = connection;

                try
                {
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Error occurred: " + exception.Message);
                }
            }
        }

        //Change achievement[name] = true;
        public static void SetAchievement(Guid userGuid, string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand();
                command.CommandText = "UPDATE Users SET @pName = 1 WHERE UserID = @pUserID";
                command.Parameters.AddWithValue("@pUserID", userGuid);
                command.Parameters.AddWithValue("@pName", name);
                command.Connection = connection;

                try
                {
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Error occurred: " + exception.Message);
                }
            }
        }

        //return users winsInARow counter;
        public static int GetWinsInARow(Guid userGuid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand();
                command.CommandText = "SELECT WinsInARow FROM Users WHERE UserID = @pUserID";
                command.Parameters.AddWithValue("@pUserID", userGuid);
                command.Connection = connection;

                try
                {
                    command.Connection.Open();
                    var dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        dataReader.Read();
                        return dataReader.GetInt32(0);
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Error occurred: " + exception.Message);
                }
            }

            return 0;
        }

        // returns user rank
        public static int GetRank(Guid userGuid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand();
                command.CommandText = "SELECT Rank FROM Users WHERE UserID = @pUserID";
                command.Parameters.AddWithValue("@pUserID", userGuid);
                command.Connection = connection;

                try
                {
                    command.Connection.Open();
                    var dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        dataReader.Read();
                        return dataReader.GetInt32(0);
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Error occurred: " + exception.Message);
                }
            }

            return 0;
        }

        // sets user rank
        public static void SetRank(Guid userGuid, int rank)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand();
                command.CommandText = "UPDATE Users SET Rank = @pRank WHERE UserID = @pUserID";
                command.Parameters.AddWithValue("@pUserID", userGuid);
                command.Parameters.AddWithValue("@pRank", rank);
                command.Connection = connection;

                try
                {
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Error occurred: " + exception.Message);
                }
            }
        }
    }
}