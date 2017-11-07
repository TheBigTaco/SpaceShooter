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
            var cmd = DB.BeginCommand("INSERT INTO sessions (session_id, player_id) VALUES (@SessionId, @PlayerId);");
            cmd.Parameters.Add(new MySqlParameter("@SessionId", SessionId));
            cmd.Parameters.Add(new MySqlParameter("@PlayerId", PlayerId));
            cmd.ExecuteNonQuery();
            DB.EndCommand();
        }
        public static List<Session> GetAll()
        {
            var output = new List<Session> {};
            var cmd = DB.BeginCommand("SELECT * FROM sessions;");
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                string SessionId = rdr.GetString(0);
                int PlayerId = rdr.GetInt32(1);
                output.Add(new Session(PlayerId, SessionId));
            }
            DB.EndCommand();
            return output;
        }
    }
}
