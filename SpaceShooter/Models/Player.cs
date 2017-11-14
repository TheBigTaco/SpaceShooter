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
    public class Player
    {
        public int Id {get; private set;}
        public string Username {get;}
        public string Hash {get;}
        public string Salt {get;}
        private static RandomNumberGenerator rng = RandomNumberGenerator.Create();

        public Player (string username, string hash, string salt, int id = 0)
        {
            Username = username;
            Salt = salt;
            Hash = hash;
            Id = id;
        }
        public static string MakeSalt()
        {
            byte[] rndArray = new byte[22];
            rng.GetBytes(rndArray);
            rndArray[21] = 0;
            BigInteger number = new BigInteger(rndArray);
            string result = number.ToString("x").PadLeft(42, '0');
            if (result.Length > 42)
            {
                result = result.Substring(result.Length - 42);
            }
            return result;
        }
        public override bool Equals(object other)
        {
            if (!(other is Player))
            {
                return false;
            }
            else
            {
                Player otherPlayer = (Player)other;
                return (
                    this.Username == otherPlayer.Username &&
                    this.Hash == otherPlayer.Hash &&
                    this.Salt == otherPlayer.Salt
                );
            }
        }
        public override int GetHashCode()
        {
            return Username.GetHashCode() + Hash.GetHashCode() + Salt.GetHashCode();
        }
        // Saves into DB if username not taken, else throws exception
        public void Save()
        {
            if (DoesUsernameExist(Username))
            {
                throw new InvalidOperationException("User already exists");
            }
            else
            {
                var _conn = new DBConnection();
                var cmd = _conn.BeginCommand("INSERT INTO players (login_name, password_hash, salt) VALUES (@Username, @Hash, @Salt);");
                cmd.Parameters.Add(new MySqlParameter("@Username", Username));
                cmd.Parameters.Add(new MySqlParameter("@Hash", Hash));
                cmd.Parameters.Add(new MySqlParameter("@Salt", Salt));
                cmd.ExecuteNonQuery();
                this.Id = (int)cmd.LastInsertedId;
                _conn.EndCommand();
            }
        }
        public static List<Player> GetAll()
        {
            var output = new List<Player> {};
            var _conn = new DBConnection();
            var cmd = _conn.BeginCommand("SELECT * FROM players;");
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string username = rdr.GetString(1);
                string hash = rdr.GetString(2);
                string salt = rdr.GetString(3);
                output.Add(new Player(username, hash, salt, id));
            }
            _conn.EndCommand();
            return output;
        }
        public static Player FindById(int searchId)
        {
            var _conn = new DBConnection();
            var cmd = _conn.BeginCommand("SELECT * FROM players WHERE id = @Id;");
            cmd.Parameters.Add(new MySqlParameter("@Id", searchId));
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int id = 0;
            string username = "";
            string hash = "";
            string salt = "";
            while (rdr.Read())
            {
                id = rdr.GetInt32(0);
                username = rdr.GetString(1);
                hash = rdr.GetString(2);
                salt = rdr.GetString(3);
            }
            var output = new Player(username, hash, salt, id);
            _conn.EndCommand();
            return output;
        }
        public static bool DoesUsernameExist(string username)
        {
            var _conn = new DBConnection();
            var cmd = _conn.BeginCommand("SELECT COUNT(*) FROM players WHERE login_name = @LoginName;");
            cmd.Parameters.Add(new MySqlParameter("@LoginName", username));
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            bool output = false;
            while(rdr.Read())
            {
                int count = rdr.GetInt32(0);
                if (count > 0)
                {
                    output = true;
                }
            }
            _conn.EndCommand();
            return output;
        }
        public static Player FindByUsername(string searchName)
        {
            var _conn = new DBConnection();
            var cmd = _conn.BeginCommand("SELECT * FROM players WHERE login_name = @LoginName;");
            cmd.Parameters.Add(new MySqlParameter("@LoginName", searchName));
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int id = 0;
            string username = "";
            string hash = "";
            string salt = "";
            while (rdr.Read())
            {
                id = rdr.GetInt32(0);
                username = rdr.GetString(1);
                hash = rdr.GetString(2);
                salt = rdr.GetString(3);
            }
            var output = new Player(username, hash, salt, id);
            _conn.EndCommand();
            return output;
        }
        // Return new session on successful login, else null
        public static Session Login(string username, string password)
        {
            Player player = FindByUsername(username);
            Hash testHash = new Hash(password, player.Salt);
            if (player.Hash == testHash.Result)
            {
                Session newSession = new Session(player.Id);
                newSession.Save();
                return newSession;
            }
            return null;
        }
    }
}
