using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Numerics;

namespace SpaceShooter.Models
{
    public class Session
    {
        public string SessionId {get;}
        public int PlayerId {get;}

        public Session(int playerId)
        {
            SessionId = Player.MakeSalt();
            PlayerId = playerId;
        }
    }
}
