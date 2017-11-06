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
            byte[] rndArray = new byte[4];
            rng.GetBytes(rndArray);
            int number = BitConverter.ToInt32(rndArray, 0);
            string result = number.ToString("X");
            Console.WriteLine(result);
            return result;
        }

        public static string HashAlgorithm(string password, string salt)
        {
            byte[] h0 = {0b1000_0000, 0b1100_0100, 0b0010_1000}
            //TODO: create h1-h5
            string combined = password + salt;
            byte[] initialBytes = Encoding.ASCII.GetBytes(combined);
            return password;
        }

        public static string ToBinaryString(BigInteger n)
        {
            byte[] bytes = n.ToByteArray();
            string output = "";
            for (int i = bytes.Length - 1; i >= 0; i--)
            {
                output += Convert.ToString(bytes[i], 2).PadLeft(8, '0');
                if (i != 0)
                {
                    output += "_";
                }
            }
            return output;
        }
    }
}
