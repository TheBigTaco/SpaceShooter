using System;
using System.Collections.Generic;
using SpaceShooter.Models;

namespace SpaceShooter.ViewModels
{
    public class HomeModel
    {
        public Session CurrentSession {get; private set;}

        public HomeModel()
        {
            CurrentSession = null;
        }
        public HomeModel(string sessionId)
        {
            CurrentSession = Session.FindById(sessionId);
        }
    }
}
