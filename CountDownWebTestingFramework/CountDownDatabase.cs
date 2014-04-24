using System;
using System.Data.SQLite;
using System.IO;

namespace CountDownWebTestingFramework
{
    public static class CountDownDatabase
    {
        private static string _connectionString;
        private static SQLiteConnection _connection;

        public static void OpenConnection()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName +
                          @"\CountDown\App_Data\countdown.sqlite";
            _connectionString = String.Format("Data Source={0}; Version=3;", path);
            _connection = new SQLiteConnection(_connectionString);
            _connection.Open();
        }

        public static void CloseConnection()
        {
            _connection.Close();
        }

        public static void DeleteUser(string email)
        {
            var query = String.Format("SELECT id FROM users WHERE email = '{0}'", email);

            SQLiteCommand command = new SQLiteCommand(query, _connection);

            SQLiteDataReader reader = command.ExecuteReader();

            long id = 0;
            if (reader.Read())
            {
                id = reader.GetInt64(0);
            }
            reader.Close();

            query = String.Format("DELETE FROM users WHERE id = {0}", id);
            command = new SQLiteCommand(query, _connection);
            reader = command.ExecuteReader();
            reader.Close();
        }
    }
}