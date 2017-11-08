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
        public string LoginName {get;}

        public ProfileModel(int id, string sessionId) : base(sessionId)
        {
            Highscore = GameStats.GetPlayerHighScore(id);
            TotalEnemiesDestroyed = GameStats.GetPlayerTotalEnemiesDestroyed(id);
            TotalPlayTime = new TimeSpan(GameStats.GetPlayerTotalTimePlayed(id)*10000).ToString(@"hh\:mm\:ss");
            TotalScore = GameStats.GetPlayerTotalScore(id);
            MostRecentStats = GameStats.GetPlayerMostRecentStats(id);
            LoginName = Player.FindById(id).Username;
        }
    }
}
