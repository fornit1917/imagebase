using System;
using System.Collections;

namespace ImageBase.Common
{
    public static class HammingDistance
    {
        public static int Calculate(string a, string b)
        {
            int sum = 0;
            var splitFirst = a.ToCharArray();
            var splitSecond = b.ToCharArray();

            for (int i = 0; i < splitFirst.Length; i++)
            {
                sum += splitFirst[i].Equals(splitSecond[i]) ? 0 : 1;
            }

            return sum;
        }

        public static int Calculate(long a, long b)
        {
            return Calculate(new BitArray(BitConverter.GetBytes(a)), new BitArray(BitConverter.GetBytes(b)));
        }

        public static int Calculate(BitArray a, BitArray b)
        {
            int sum = 0;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                sum += a[i] ^ b[i] ? 1 : 0;
            }

            return sum;
        }
    }
}
