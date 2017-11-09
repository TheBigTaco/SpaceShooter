using System;
using System.Collections.Generic;
using SpaceShooter.Models;

namespace SpaceShooter.ViewModels
{
    public class ProfileModel : HomeModel
    {
        public long Highscore {get;}
        public int TotalEnemiesDestroyed {get;}
        public string TotalPlayTime {get;}
        public long TotalScore {get;}
        public GameStats MostRecentStats {get;}
        public Player Player {get;}
        public List<PlayerListEntry> Friends {get;}
        public bool? Follow {get;}

        public ProfileModel(int profileId, string sessionId) : base(sessionId)
        {
            Highscore = GameStats.GetPlayerHighScore(profileId);
            TotalEnemiesDestroyed = GameStats.GetPlayerTotalEnemiesDestroyed(profileId);
            TotalPlayTime = new TimeSpan(GameStats.GetPlayerTotalTimePlayed(profileId)*10000).ToString(@"hh\:mm\:ss");
            TotalScore = GameStats.GetPlayerTotalScore(profileId);
            MostRecentStats = GameStats.GetPlayerMostRecentStats(profileId);
            Player = Player.FindById(profileId);
            Friends = Friend.GetFriendList(profileId);
            Follow = FriendPair.CheckForFriend(base.CurrentSession.PlayerId, profileId);
        }
    }
}
