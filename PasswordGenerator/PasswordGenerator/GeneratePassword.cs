using System;
using System.Linq;

namespace PasswordGenerator
{
    class GeneratePassword
    {
        double p = 0.001;
        int v = 10;
        int t = 30;
        int s;
        int l;
        int a = 66;
        public int count;
        string password;

        public string Generator()
        {
            s = CountS();

            l = Convert.ToInt32(Math.Ceiling(Math.Log(s, a)));
            char[] symbols = "abcdefghjklmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789@#$%".ToArray();

            count = symbols.Count();

            Random random = new Random();

            for (int i = 0; i < l; i++)
            {
                password += symbols[random.Next(0, symbols.Length - 1)];
            }

            return password;
        } 
        int CountS()
        {
            var number = Math.Ceiling(v * t / p);

            return Convert.ToInt32(number);
        }
    }
}
