using System;
using System.Collections.Generic;
using SpaceShooter.Models;

namespace SpaceShooter.ViewModels
{
    public class SearchModel : HomeModel
    {
        public Dictionary<string, PlayerListEntry> Results {get; private set;}
        public SearchModel(string searchTerm, string sessionId) : base(sessionId)
        {
            Results = PlayerListEntry.GetSearchResults(searchTerm);
        }
    }
}
