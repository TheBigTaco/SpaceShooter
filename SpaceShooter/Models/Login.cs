using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace SpaceShooter.Models
{
    public class Login
    {
      public string Username {get;}
      public string Hash {get;}
      public string Salt {get;}
      private static RandomNumberGenerator rng = RandomNumberGenerator.Create();

      public Login (string username, string hash)
      {
        Username = username;
        Hash = hash;
        Salt = MakeSalt();
      }
      public static string MakeSalt()
      {
        byte[] rndArray = new byte[4];
        rng.GetBytes(rndArray);
        int number = BitConverter.ToInt32(rndArray, 0);
        string result = number.ToString("X");
        Console.WriteLine(result);
        return result;
      }
    }
}
