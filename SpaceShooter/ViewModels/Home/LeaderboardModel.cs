using System;
using System.Collections.Generic;
using SpaceShooter.Models;

namespace SpaceShooter.ViewModels
{
    public class LeaderboardModel : HomeModel
    {
        public List<PlayerListEntry> Leaderboard {get; private set;}
        public LeaderboardModel(string sessionId) : base(sessionId)
        {
            Leaderboard = PlayerListEntry.GetLeaderboard();
        }
    }
}
