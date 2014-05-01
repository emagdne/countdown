using System;
using System.Data.SQLite;
using System.IO;

namespace CountDown.WebTestingFramework
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 5/1/14</para>
    /// </summary>
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

        public static long CreateUser(string firstName, string lastName, string email, string hash)
        {
            var query =
                String.Format(
                    "INSERT INTO users (first_name, last_name, email, hash) VALUES ('{0}', '{1}', '{2}', '{3}')",
                    firstName,
                    lastName, email, hash);
            SQLiteCommand command = new SQLiteCommand(query, _connection);
            command.ExecuteReader();

            query = String.Format("SELECT id FROM users WHERE email = '{0}'", email);
            command = new SQLiteCommand(query, _connection);
            SQLiteDataReader reader = command.ExecuteReader();

            long id = 0;
            if (reader.Read())
            {
                id = reader.GetInt64(0);
            }
            reader.Close();

            return id;
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

        public static void DeleteUser(long id)
        {
            SQLiteCommand command = new SQLiteCommand(String.Format("DELETE FROM users WHERE id = {0}", id), _connection);
            command.ExecuteReader();
        }

        public static long CreateToDoItem(string title, string description, DateTime start, DateTime due,
            long assigneeId, long ownerId, bool completed)
        {
            var query =
                String.Format(
                    "INSERT INTO todo_items (title, description, start_date, due_date, owner, assigned_to, completed) " +
                    "VALUES ('{0}', '{1}', '{2:yyyy-MM-dd HH:mm:ss.fff}', '{3:yyyy-MM-dd HH:mm:ss.fff}', {4}, {5}, {6})",
                    title, description, start, due, ownerId, assigneeId, completed ? 1 : 0);
            SQLiteCommand command = new SQLiteCommand(query, _connection);
            command.ExecuteReader();

            query =
                String.Format(
                    "SELECT id FROM todo_items " +
                    "WHERE title = '{0}' AND description = '{1}' AND owner = {2} AND assigned_to = {3} AND completed = {4}",
                    title, description, ownerId, assigneeId, completed ? 1 : 0);
            command = new SQLiteCommand(query, _connection);
            SQLiteDataReader reader = command.ExecuteReader();

            long id = 0;
            if (reader.Read())
            {
                id = reader.GetInt64(0);
            }
            reader.Close();

            return id;
        }

        public static void DeleteToDoItem(string title)
        {
            var query = String.Format("SELECT id FROM todo_items WHERE title = '{0}'", title);

            SQLiteCommand command = new SQLiteCommand(query, _connection);

            SQLiteDataReader reader = command.ExecuteReader();

            long id = 0;
            if (reader.Read())
            {
                id = reader.GetInt64(0);
            }
            reader.Close();

            query = String.Format("DELETE FROM todo_items WHERE id = {0}", id);
            command = new SQLiteCommand(query, _connection);
            reader = command.ExecuteReader();
            reader.Close();
        }

        public static void DeleteToDoItem(long id)
        {
            SQLiteCommand command = new SQLiteCommand(String.Format("DELETE FROM todo_items WHERE id = {0}", id),
                _connection);
            command.ExecuteReader();
        }

        public static bool IsToDoItemComplete(long id)
        {
            var query = String.Format("SELECT completed FROM todo_items WHERE id = {0}", id);

            SQLiteCommand command = new SQLiteCommand(query, _connection);

            SQLiteDataReader reader = command.ExecuteReader();

            int completed = 0;
            if (reader.Read())
            {
                completed = reader.GetInt32(0);
            }
            reader.Close();

            return completed == 1;
        }

        public static void UpdateToDoItem(long id, bool completed)
        {
            var query = String.Format("UPDATE todo_items SET completed = {0} WHERE id = {1}", completed ? 1 : 0, id);
            SQLiteCommand command = new SQLiteCommand(query, _connection);
            command.ExecuteReader();
        }
    }
}