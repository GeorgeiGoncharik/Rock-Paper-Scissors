using System.Security.Cryptography;
using System.Text;

namespace RockPaperScissors
{
    public static class SecurityUtil
    {
        public static byte[] GenerateRandomKey(int size)
        {
            using var generator = RandomNumberGenerator.Create();
            var key = new byte[size];
            generator.GetBytes(key);
            return key;
        }
        
        public static byte[] GetHmac(byte[] key, string message)
        {
            using var hmac = new HMACSHA256(key);
            return hmac.ComputeHash(StringEncode(message));
        }
        
        private static byte[] StringEncode(string message)
        {
            var encoding = new UTF8Encoding();
            return encoding.GetBytes(message);
        }
    }
}