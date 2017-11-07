using System;
using System.Collections.Generic;
using SpaceShooter.Models;

namespace SpaceShooter.ViewModels
{
    public class IndexModel : HomeModel
    {
        public bool RegisterFailed {get; set;} = false;
        public bool RegisterSuccess {get; set;} = false;
        public Player RegisteredPlayer {get; set;} = null;
        public bool LoginFailed {get; set;} = false;
        public bool LoginSuccess {get; set;} = false;
        public Player LoggedInPlayer {get; set;} = null;
        public IndexModel()
        {

        }
        public IndexModel(string sessionId) : base(sessionId)
        {

        }
    }
}
