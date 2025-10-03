using System;
namespace Persistence.Utils
{
    public static class RandomGenerator
    {
        private static Random random = new Random();

        public static string RandomAlphaNumericString(int length)
        {
            const string allChars = "abcdefghijklmnopqrstuvwxyz0123456789";
            const int numChars = 36;
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(numChars);
                chars[i] = allChars[index];
            }
            return new string(chars);
        }
    }
}