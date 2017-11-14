using System;
using MySql.Data.MySqlClient;
using SpaceShooter;

namespace SpaceShooter.Models
{
    public static class DB
    {
        public static void ClearAll()
        {
            var _conn = new DBConnection();
            var cmd = _conn.BeginCommand(@"DELETE FROM profiles; DELETE FROM game_stats; DELETE FROM friends; DELETE FROM sessions; DELETE FROM players;");
            cmd.ExecuteNonQuery();
            _conn.EndCommand();
        }
    }

    public class DBConnection
    {
        private MySqlConnection _currentConnection = null;

        public MySqlCommand BeginCommand(string query)
        {
            if (_currentConnection != null)
            {
                throw new InvalidOperationException("Connection already in use");
            }
            _currentConnection = new MySqlConnection(DBConfiguration.ConnectionString);
            _currentConnection.Open();
            var cmd = _currentConnection.CreateCommand() as MySqlCommand;
            cmd.CommandText = query;
            return cmd;
        }

        public void EndCommand()
        {
            _currentConnection.Close();
            if (_currentConnection != null)
            {
                _currentConnection.Dispose();
                _currentConnection = null;
            }
        }
    }
}
