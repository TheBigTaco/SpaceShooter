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
    public class Hash
    {
        private BigInteger h0 = new BigInteger(new byte[]{0b0000_0001, 0b0010_0011, 0b0100_0101, 0b0110_0111, 0b0000_0000});
        private BigInteger h1 = new BigInteger(new byte[]{0b1000_1001, 0b1010_1011, 0b1100_1101, 0b1110_1111, 0b0000_0000});
        private BigInteger h2 = new BigInteger(new byte[]{0b1111_1110, 0b1101_1100, 0b1011_1010, 0b1001_1000, 0b0000_0000});
        private BigInteger h3 = new BigInteger(new byte[]{0b0111_0110, 0b0101_0100, 0b0011_0010, 0b0001_0000, 0b0000_0000});
        private BigInteger h4 = new BigInteger(new byte[]{0b1111_0000, 0b1110_0001, 0b1101_0010, 0b1100_0011, 0b0000_0000});

        private BigInteger A;
        private BigInteger B;
        private BigInteger C;
        private BigInteger D;
        private BigInteger E;

        private static long bitCutter = 4294967296;

        public string Result {get; private set;}

        public Hash (string password, string salt)
        {
            A = h0;
            B = h1;
            C = h2;
            D = h3;
            E = h4;
            Result = HashAlgorithm(password, salt);
        }

        private string HashAlgorithm(string password, string salt)
        {
            string combined = password + salt;
            byte[] initialBytes = Encoding.ASCII.GetBytes(combined);
            BigInteger beginning = new BigInteger(initialBytes);
            beginning = (beginning << 1) + 1;
            int combinedBinary = combined.Length * 8 + 1;
            int shiftAmount = 512 - combinedBinary;
            beginning = (beginning << shiftAmount) + combined.Length;

            byte[] middle = beginning.ToByteArray();
            BigInteger[] chunks = new BigInteger[80];
            for(int i = 0; i < 16; i++)
            {
                byte[] temp = new byte[5];
                temp[0] = middle[4 * i];
                temp[1] = middle[4 * i + 1];
                temp[2] = middle[4 * i + 2];
                temp[3] = middle[4 * i + 3];
                temp[4] = 0;
                chunks[15 - i] = new BigInteger(temp);
            }
            for(int i = 16; i < 80; i++)
            {
                BigInteger xor = (((chunks[i-3] ^ chunks[i-8]) ^ chunks[i-14]) ^ chunks[i-16]);
                BigInteger rotate = LeftRotate(xor, 1);
                chunks[i] = rotate;
            }
            for( int i = 0; i < 80; i++)
            {
                if(i < 20)
                {
                    FunctionOne(chunks[i]);
                }
                else if (i < 40)
                {
                    FunctionTwo(chunks[i]);
                }
                else if (i < 60)
                {
                    FunctionThree(chunks[i]);
                }
                else
                {
                    FunctionFour(chunks[i]);
                }
            }
            h0 = (h0 + A) % bitCutter;
            h1 = (h1 + B) % bitCutter;
            h2 = (h2 + C) % bitCutter;
            h3 = (h3 + D) % bitCutter;
            h4 = (h4 + E) % bitCutter;

            string result = h0.ToString("x") + h1.ToString("x") + h2.ToString("x") + h3.ToString("x") + h4.ToString("x");
            return result;
        }

        private void FunctionOne(BigInteger n)
        {
            BigInteger F = (B & C) | (~B & D);
            BigInteger K = new BigInteger(new byte[]{0b1111_1110, 0b1101_1100, 0b1011_1010, 0b1001_1000, 0b0000_0000});
            BigInteger temp = (LeftRotate(A, 5) + F + E + K + n) % bitCutter;
            Swapper(temp);
        }

        private void FunctionTwo(BigInteger n)
        {
            BigInteger F = (B ^ C ^ D);
            BigInteger K = new BigInteger(new byte[]{0b1010_0001, 0b1110_1011, 0b1101_1001, 0b0110_1110, 0b0000_0000});
            BigInteger temp = (LeftRotate(A, 5) + F + E + K + n) % bitCutter;
            Swapper(temp);
        }

        private void FunctionThree(BigInteger n)
        {
            BigInteger F = (B & C) | (B & D) | (C & D);
            BigInteger K = new BigInteger(new byte[]{0b1101_1100, 0b1011_1100, 0b0001_1011, 0b1000_1111, 0b0000_0000});
            BigInteger temp = (LeftRotate(A, 5) + F + E + K + n) % bitCutter;
            Swapper(temp);
        }

        private void FunctionFour(BigInteger n)
        {
            BigInteger F = (B ^ C ^ D);
            BigInteger K = new BigInteger(new byte[]{0b1101_0110, 0b1100_0001, 0b0011_0001, 0b1100_1010, 0b0000_0000});
            BigInteger temp = (LeftRotate(A, 5) + F + E + K + n) % bitCutter;
            Swapper(temp);
        }
        private void Swapper(BigInteger temp)
        {
            E = D;
            D = C;
            C = LeftRotate(B, 30);
            B = A;
            A = temp;
        }

        private static BigInteger LeftRotate(BigInteger n, int x)
        {
            return ((n << x) | (n >> (32 - x))) % bitCutter;
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
