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
            var _conn = new DBConnection();
            var cmd = _conn.BeginCommand("SELECT players.login_name, friends.player_2_id FROM players JOIN friends ON (players.id = friends.player_2_id) JOIN game_stats ON (friends.player_2_id = game_stats.player_id) WHERE friends.player_1_id = @id ORDER BY game_stats.score DESC;");
            cmd.Parameters.Add(new MySqlParameter("@id", id));
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                string friendName = rdr.GetString(0);
                int friendId = rdr.GetInt32(1);
                output.Add(new Friend(friendName, friendId));
            }
            _conn.EndCommand();
            return output;
        }
        public static List<PlayerListEntry> GetFriendList(int profileId)
        {
            var output = new List<PlayerListEntry> {};
            var keys = new List<int> {};
            var _conn = new DBConnection();
            var cmd = _conn.BeginCommand("SELECT players.login_name, players.id, game_stats.score FROM players JOIN friends ON (players.id = friends.player_2_id) JOIN game_stats ON (friends.player_2_id = game_stats.player_id) ORDER BY game_stats.score DESC;");
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                string name = rdr.GetString(0);
                int id = rdr.GetInt32(1);
                long score = rdr.GetInt64(2);
                if (!keys.Contains(id))
                {
                    PlayerListEntry newEntry = new PlayerListEntry(id, name, score);
                    output.Add(newEntry);
                    keys.Add(id);
                }
            }
            _conn.EndCommand();
            return output;
        }
    }
}
