using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace SpaceShooter.Models
{
    public class Friend
    {
        public string Name {get;}
        public int FriendId {get;}
        public Friend (string name, int friendId)
        {
            Name = name;
            FriendId = friendId;
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
                    this.Name == otherFriend.Name &&
                    this.FriendId == otherFriend.FriendId
                );
            }
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode() + FriendId.GetHashCode();
        }
        public static List<Friend> GetAllFriendsForPlayer(int id)
        {
            var output = new List<Friend> {};
            var cmd = DB.BeginCommand("SELECT players.login_name, friends.player_2_id FROM players JOIN friends ON (players.id = friends.player_2_id) JOIN game_stats ON (friends.player_2_id = game_stats.player_id) WHERE friends.player_1_id = @id ORDER BY game_stats.score DESC;");
            cmd.Parameters.Add(new MySqlParameter("@id", id));
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                string friendName = rdr.GetString(0);
                int friendId = rdr.GetInt32(1);
                output.Add(new Friend(friendName, friendId));
            }
            DB.EndCommand();
            return output;
        }
    }
}
