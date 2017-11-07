using System;
using System.Collections.Generic;
using SpaceShooter.Models;

namespace SpaceShooter.ViewModels
{
    public class RegisterModel
    {
        public bool RegisterFailed {get; set;} = false;
        public bool RegisterSuccess {get; set;} = false;
        public Player RegisteredPlayer {get; set;} = null;
        
        public RegisterModel()
        {

        }
    }
}
