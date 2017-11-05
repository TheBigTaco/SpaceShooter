using System;
using MySql.Data.MySqlClient;
using SpaceShooter;

namespace SpaceShooter.Models
{
    public static class DB
    {
        private static MySqlConnection _currentConnection = null;

        public static MySqlCommand BeginCommand(string query)
        {
            if (_currentConnection != null)
            {
                throw new InvalidOperationException("Connection already in use")
            }
            _currentConnection = new MySqlConnection(DBConfiguration.ConnectionString);
            _currentConnection.Open();
            var cmd = _currentConnection.CreateCommand() as MySqlCommand;
            cmd.CommandText = query;
            return cmd;
        }

        public static void EndCommand()
        {
            _currentConnection.Close();
            if (_currentConnection != null)
            {
                _currentConnection.Dispose();
                _currentConnection = null;
            }
        }

        public static void ClearAll()
        {
            BeginCommand(@"DELETE FROM players;");
            cmd.ExecuteNonQuery();
            EndCommand();
        }
    }
}
