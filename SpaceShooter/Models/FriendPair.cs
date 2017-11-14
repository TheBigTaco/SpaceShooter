using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace SpaceShooter.Models
{
    public class FriendPair
    {
        public int Player1Id {get; private set;}
        public int Player2Id {get; private set;}

        public FriendPair(int player1, int player2)
        {
            Player1Id = player1;
            Player2Id = player2;
        }
        public override bool Equals(object other)
        {
            if (!(other is FriendPair))
            {
                return false;
            }
            else
            {
                FriendPair otherFriendPair = (FriendPair)other;
                return (
                    this.Player1Id == otherFriendPair.Player1Id &&
                    this.Player2Id == otherFriendPair.Player2Id
                );
            }
        }
        public override int GetHashCode()
        {
            return Player1Id.GetHashCode() + Player2Id.GetHashCode();
        }
        public void Save()
        {
            var _conn = new DBConnection();
            var cmd = _conn.BeginCommand("INSERT INTO friends (player_1_id, player_2_id) VALUES (@Player1Id, @Player2Id);");
            cmd.Parameters.Add(new MySqlParameter("@Player1Id", Player1Id));
            cmd.Parameters.Add(new MySqlParameter("@Player2Id", Player2Id));
            cmd.ExecuteNonQuery();
            _conn.EndCommand();
        }
        public static void Unfollow(int player1id, int player2id)
        {
            var _conn = new DBConnection();
            var cmd = _conn.BeginCommand("DELETE FROM friends WHERE player_1_id = @Player1Id AND player_2_id = @Player2Id;");
            cmd.Parameters.Add(new MySqlParameter("@Player1Id", player1id));
            cmd.Parameters.Add(new MySqlParameter("@Player2Id", player2id));
            cmd.ExecuteNonQuery();
            _conn.EndCommand();
        }
        public static List<FriendPair> GetAllFriendPairs()
        {
            var output = new List<FriendPair> {};
            var _conn = new DBConnection();
            var cmd = _conn.BeginCommand("SELECT * FROM friends;");
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int player1Id = rdr.GetInt32(0);
                int player2Id = rdr.GetInt32(1);
                output.Add(new FriendPair(player1Id, player2Id));
            }
            _conn.EndCommand();
            return output;
        }
        public static bool? CheckForFriend(int player1Id, int player2Id)
        {
            if(player1Id != player2Id)
            {
                var _conn = new DBConnection();
                var cmd = _conn.BeginCommand("SELECT COUNT(*) FROM friends WHERE player_1_id = @Player1Id AND player_2_id = @Player2Id;");
                cmd.Parameters.Add(new MySqlParameter("Player1Id", player1Id));
                cmd.Parameters.Add(new MySqlParameter("Player2Id", player2Id));
                var rdr = cmd.ExecuteReader() as MySqlDataReader;
                int count = 0;
                while (rdr.Read())
                {
                    count = rdr.GetInt32(0);
                }
                _conn.EndCommand();
                if(count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return null;
        }
    }
}
