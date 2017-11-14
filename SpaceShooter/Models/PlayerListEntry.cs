using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace SpaceShooter.Models
{
    public class PlayerListEntry
    {
        public string Name {get; private set;}
        public long Score {get; private set;}
        public int PlayerId{get; private set;}

        public PlayerListEntry(int playerId, string name, long score)
        {
            Name = name;
            Score = score;
            PlayerId = playerId;
        }
        public override bool Equals(object other)
        {
            if (!(other is PlayerListEntry))
            {
                return false;
            }
            else
            {
                PlayerListEntry otherPlayerListEntry = (PlayerListEntry)other;
                return (
                    this.Name == otherPlayerListEntry.Name &&
                    this.Score == otherPlayerListEntry.Score
                );
            }
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode() + Score.GetHashCode();
        }
        public static List<PlayerListEntry> GetLeaderboard()
        {
            var output = new List<PlayerListEntry> {};
            var _conn = new DBConnection();
            var cmd = _conn.BeginCommand("SELECT players.login_name, players.id, game_stats.score FROM game_stats JOIN players ON (players.id = game_stats.player_id) ORDER BY game_stats.score DESC;");
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                string name = rdr.GetString(0);
                int id = rdr.GetInt32(1);
                long score = rdr.GetInt64(2);
                output.Add(new PlayerListEntry(id, name, score));
            }
            _conn.EndCommand();
            return output;
        }
        // string = name, long = score
        public static Dictionary<string, PlayerListEntry> GetSearchResults(string search)
        {
            var output = new Dictionary<string, PlayerListEntry> {};
            if(search != null)
            {
                Regex regex = new Regex($@"{search}", RegexOptions.IgnoreCase);
                var _conn = new DBConnection();
                var cmd = _conn.BeginCommand("SELECT players.login_name, players.id, game_stats.score FROM game_stats JOIN players ON (players.id = game_stats.player_id) ORDER BY game_stats.score DESC;");
                var rdr = cmd.ExecuteReader() as MySqlDataReader;
                while (rdr.Read())
                {
                    string name = rdr.GetString(0);
                    int id = rdr.GetInt32(1);
                    long score = rdr.GetInt64(2);
                    if (!output.ContainsKey(name))
                    {
                        Match match = regex.Match(name);
                        if(match.Success)
                        {
                            PlayerListEntry newEntry = new PlayerListEntry(id, name, score);
                            output.Add(name, newEntry);
                        }
                    }
                }
                _conn.EndCommand();
            }
            return output;
        }
    }
}
