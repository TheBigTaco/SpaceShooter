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
    public class Login
    {
        public string Username {get;}
        public string Hash {get;}
        public string Salt {get;}
        private static RandomNumberGenerator rng = RandomNumberGenerator.Create();

        public Login (string username, string hash, string salt)
        {
            Username = username;
            Salt = salt;
            Hash = hash;
        }

        public static string MakeSalt()
        {
            byte[] rndArray = new byte[22];
            rng.GetBytes(rndArray);
            rndArray[21] = 0;
            BigInteger number = new BigInteger(rndArray);
            string result = number.ToString("X");
            if (result[0] == '0')
            {
                result = result.Substring(1);
            }
            Console.WriteLine("##############" + result);
            return result;
        }
    }
}
