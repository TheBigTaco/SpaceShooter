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

        public PlayerListEntry(string name, long score)
        {
            Name = name;
            Score = score;
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
            var cmd = DB.BeginCommand("SELECT players.login_name, game_stats.score FROM game_stats JOIN players ON (players.id = game_stats.player_id) ORDER BY game_stats.score DESC;");
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                string name = rdr.GetString(0);
                long score = rdr.GetInt64(1);
                output.Add(new PlayerListEntry(name, score));
            }
            DB.EndCommand();
            return output;
        }
        // string = name, long = score
        public static Dictionary<string, long> GetSearchResults(string search)
        {
            var output = new Dictionary<string, long> {};
            var cmd = DB.BeginCommand("SELECT players.login_name, game_stats.score FROM game_stats JOIN players ON (players.id = game_stats.player_id) ORDER BY game_stats.score DESC;");
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                string name = rdr.GetString(0);
                long score = rdr.GetInt64(1);
                if (!output.ContainsKey(name))
                {
                    output.Add(name, score);
                }
            }
            DB.EndCommand();
            return output;
        }
    }
}
