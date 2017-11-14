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
    public class Session
    {
        public string SessionId {get; private set;}
        public int PlayerId {get; private set;}

        public Session(int playerId, string sessionId = "")
        {
            PlayerId = playerId;
            SessionId = sessionId;
        }
        public override bool Equals(object other)
        {
            if (!(other is Session))
            {
                return false;
            }
            else
            {
                Session otherSession = (Session)other;
                return (
                    this.SessionId == otherSession.SessionId &&
                    this.PlayerId == otherSession.PlayerId
                );
            }
        }
        public override int GetHashCode()
        {
            return SessionId.GetHashCode() + PlayerId.GetHashCode();
        }
        public void Save()
        {
            SessionId = Player.MakeSalt();
            var _conn = new DBConnection();
            var cmd = _conn.BeginCommand("INSERT INTO sessions (session_id, player_id) VALUES (@SessionId, @PlayerId);");
            cmd.Parameters.Add(new MySqlParameter("@SessionId", SessionId));
            cmd.Parameters.Add(new MySqlParameter("@PlayerId", PlayerId));
            cmd.ExecuteNonQuery();
            _conn.EndCommand();
        }
        public static List<Session> GetAll()
        {
            var output = new List<Session> {};
            var _conn = new DBConnection();
            var cmd = _conn.BeginCommand("SELECT * FROM sessions;");
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                string sessionId = rdr.GetString(0);
                int playerId = rdr.GetInt32(1);
                output.Add(new Session(playerId, sessionId));
            }
            _conn.EndCommand();
            return output;
        }
        public static Session FindById(string searchId)
        {
            var _conn = new DBConnection();
            var cmd = _conn.BeginCommand("SELECT * FROM sessions WHERE session_id = @SessionId;");
            cmd.Parameters.Add(new MySqlParameter("@SessionId", searchId));
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            string sessionId = "";
            int playerId = 0;
            while (rdr.Read())
            {
                sessionId = rdr.GetString(0);
                playerId = rdr.GetInt32(1);
            }
            var output = new Session(playerId, sessionId);
            if (output.PlayerId == 0 && sessionId == "")
            {
                output = null;
            }
            _conn.EndCommand();
            return output;
        }
        public static void DeleteById(string searchId)
        {
            var _conn = new DBConnection();
            var cmd = _conn.BeginCommand("DELETE FROM sessions WHERE session_id = @SessionId;");
            cmd.Parameters.Add(new MySqlParameter("@SessionId", searchId));
            cmd.ExecuteNonQuery();
            _conn.EndCommand();
        }
    }
}
