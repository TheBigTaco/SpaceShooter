using System;
using System.Collections.Generic;
using SpaceShooter.Models;

namespace SpaceShooter.ViewModels
{
    public class FriendsModel : HomeModel
    {
        public List<PlayerListEntry> FriendsList {get;}
        public FriendsModel(int profileId, string sessionId) : base(sessionId)
        {
            FriendsList = Friend.GetFriendList(profileId);
        }
    }
}
