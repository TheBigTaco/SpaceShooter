using System;
using System.Collections.Generic;
using SpaceShooter.Models;

namespace SpaceShooter.ViewModels
{
    public class LoginModel
    {
        public bool LoginFailed {get; set;} = false;
        public bool LoginSuccess {get; set;} = false;
        public Player LoggedInPlayer {get; set;} = null;
        public LoginModel()
        {

        }
    }
}
