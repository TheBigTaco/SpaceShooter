using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Numerics;

namespace SpaceShooter.Models
{
    public class GameStats
    {
        public int PlayerId {get; private set;}
        public long Score {get; private set;}
        public int EnemiesDestroyed {get; private set;}
        public long GameTime {get; private set;}
        public DateTime DatePlayed {get; private set;}

        public GameStats(int playerId, long score, int enemiesDestroyed, long gameTime, DateTime datePlayed)
        {
            PlayerId = playerId;
            Score = score;
            EnemiesDestroyed = enemiesDestroyed;
            GameTime = gameTime;
            DatePlayed = datePlayed;
        }
        public override bool Equals(object other)
        {
            if (!(other is GameStats))
            {
                return false;
            }
            else
            {
                GameStats otherGameStats = (GameStats)other;
                return (
                    this.PlayerId == otherGameStats.PlayerId &&
                    this.Score == otherGameStats.Score &&
                    this.EnemiesDestroyed == otherGameStats.EnemiesDestroyed &&
                    this.GameTime == otherGameStats.GameTime &&
                    this.DatePlayed == otherGameStats.DatePlayed
                );
            }
        }
        public override int GetHashCode()
        {
            return PlayerId.GetHashCode() + Score.GetHashCode() + GameTime.GetHashCode() + EnemiesDestroyed.GetHashCode() + DatePlayed.GetHashCode();
        }
        public void Save()
        {
            var cmd = DB.BeginCommand("INSERT INTO game_stats (player_id, score, enemies_destroyed, game_time, date_played) VALUES (@PlayerId, @Score, @EnemiesDestroyed, @GameTime, @DatePlayed);");
            cmd.Parameters.Add(new MySqlParameter("@PlayerId", PlayerId));
            cmd.Parameters.Add(new MySqlParameter("@Score", Score));
            cmd.Parameters.Add(new MySqlParameter("@EnemiesDestroyed", EnemiesDestroyed));
            cmd.Parameters.Add(new MySqlParameter("@GameTime", GameTime));
            cmd.Parameters.Add(new MySqlParameter("@DatePlayed", DatePlayed.ToString("yyyy-MM-dd HH:mm:ss")));
            cmd.ExecuteNonQuery();
            DB.EndCommand();
        }
        public static List<GameStats> GetAllOrderedByScore()
        {
            var output = new List<GameStats> {};
            var cmd = DB.BeginCommand("SELECT * FROM game_stats ORDER BY score DESC;");
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int playerId = rdr.GetInt32(0);
                long score = rdr.GetInt64(1);
                int enemiesDestroyed = rdr.GetInt32(2);
                long gameTime = rdr.GetInt64(3);
                DateTime datePlayed = rdr.GetDateTime(4);
                output.Add(new GameStats(playerId, score, enemiesDestroyed, gameTime, datePlayed));
            }
            DB.EndCommand();
            return output;
        }
        public static long GetPlayerHighScore(int playerId)
        {
            var cmd = DB.BeginCommand("SELECT MAX(score) FROM game_stats WHERE player_id = @PlayerId;");
            cmd.Parameters.Add(new MySqlParameter("@PlayerId", playerId));
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            long output = 0;
            while (rdr.Read())
            {
                if(!rdr.IsDBNull(0))
                {
                    output = rdr.GetInt64(0);
                }
            }
            DB.EndCommand();
            return output;
        }
        public static long GetPlayerTotalScore(int playerId)
        {
            var cmd = DB.BeginCommand("SELECT SUM(score) FROM game_stats WHERE player_id = @PlayerId;");
            cmd.Parameters.Add(new MySqlParameter("@PlayerId", playerId));
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            long output = 0;
            while (rdr.Read())
            {
                if(!rdr.IsDBNull(0))
                {
                    output = rdr.GetInt64(0);
                }
            }
            DB.EndCommand();
            return output;
        }
        public static int GetPlayerTotalEnemiesDestroyed(int playerId)
        {
            var cmd = DB.BeginCommand("SELECT SUM(enemies_destroyed) FROM game_stats WHERE player_id = @PlayerId;");
            cmd.Parameters.Add(new MySqlParameter("@PlayerId", playerId));
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int output = 0;
            while (rdr.Read())
            {
                if(!rdr.IsDBNull(0))
                {
                    output = rdr.GetInt32(0);
                }
            }
            DB.EndCommand();
            return output;
        }
        public static long GetPlayerTotalTimePlayed(int playerId)
        {
            var cmd = DB.BeginCommand("SELECT SUM(game_time) FROM game_stats WHERE player_id = @PlayerId;");
            cmd.Parameters.Add(new MySqlParameter("@PlayerId", playerId));
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            long output = 0;
            while (rdr.Read())
            {
                if(!rdr.IsDBNull(0))
                {
                    output = rdr.GetInt64(0);
                }
            }
            DB.EndCommand();
            return output;
        }
        public static GameStats GetPlayerMostRecentStats(int searchId)
        {
            var cmd = DB.BeginCommand("SELECT * FROM game_stats WHERE player_id = @PlayerId ORDER BY date_played DESC LIMIT 1;");
            cmd.Parameters.Add(new MySqlParameter("@PlayerId", searchId));
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int playerId = 0;
            long score = 0;
            int enemiesDestroyed = 0;
            long gameTime = 0;
            DateTime datePlayed = new DateTime();
            while (rdr.Read())
            {
                playerId = rdr.GetInt32(0);
                score = rdr.GetInt64(1);
                enemiesDestroyed = rdr.GetInt32(2);
                gameTime = rdr.GetInt64(3);
                datePlayed = rdr.GetDateTime(4);
            }
            GameStats output = new GameStats(playerId, score, enemiesDestroyed, gameTime, datePlayed);
            DB.EndCommand();
            return output;
        }
    }
}
