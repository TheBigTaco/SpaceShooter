using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace SpaceShooter.Models
{
    public class Friend
    {
        public int Player1Id {get; private set;}
        public int Player2Id {get; private set;}

        public Friend(int player1, int player2)
        {
            Player1Id = player1;
            Player2Id = player2;
        }
        public override bool Equals(object other)
        {
            if (!(other is Friend))
            {
                return false;
            }
            else
            {
                Friend otherFriend = (Friend)other;
                return (
                    this.Player1Id == otherFriend.Player1Id &&
                    this.Player2Id == otherFriend.Player2Id
                );
            }
        }
        public override int GetHashCode()
        {
            return Player1Id.GetHashCode() + Player2Id.GetHashCode();
        }
        public void Save()
        {
            var cmd = DB.BeginCommand("INSERT INTO friends (player_1_id, player_2_id) VALUES (@Player1Id, @Player2Id);");
            cmd.Parameters.Add(new MySqlParameter("@Player1Id", Player1Id));
            cmd.Parameters.Add(new MySqlParameter("@Player2Id", Player2Id));
            cmd.ExecuteNonQuery();
            DB.EndCommand();
        }
        public static List<Friend> GetAllFriendPairs()
        {
            var output = new List<Friend> {};
            var cmd = DB.BeginCommand("SELECT * FROM friends;");
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int player1Id = rdr.GetInt32(0);
                int player2Id = rdr.GetInt32(1);
                output.Add(new Friend(player1Id, player2Id));
            }
            DB.EndCommand();
            return output;
        }
        public static List<Friend> GetAllFriendsForPlayer(int id)
        {
            var output = new List<Friend> {};
            var cmd = DB.BeginCommand("SELECT * FROM friends WHERE player_1_id = @id;");
            cmd.Parameters.Add(new MySqlParameter("@id", id));
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int player1Id = rdr.GetInt32(0);
                int player2Id = rdr.GetInt32(1);
                output.Add(new Friend(player1Id, player2Id));
            }
            DB.EndCommand();
            return output;
        }
    }
}
