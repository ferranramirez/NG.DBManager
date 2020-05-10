using System;
using System.Text;

namespace NG.DBManager.Test.UnitTest.Fixture.Utils
{
    public static class RandomGenerator
    {
        public static string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            Random random = new Random(new DateTime().Millisecond);
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public static int NextInt32()
        {
            Random rng = new Random(new DateTime().Millisecond);
            int firstBits = rng.Next(0, 1 << 4) << 28;
            int lastBits = rng.Next(0, 1 << 28);
            return firstBits | lastBits;
        }

        public static decimal NextDecimal()
        {
            Random rng = new Random(new DateTime().Millisecond);
            byte scale = (byte)rng.Next(29);
            bool sign = rng.Next(2) == 1;
            return new decimal(NextInt32(),
                               NextInt32(),
                               NextInt32(),
                               sign,
                               scale);
        }
    }
}

