using System;
using System.Collections;

namespace ImageBase.Common
{
    public static class HammingDistance
    {
        public static int Calculate(string a, string b)
        {
            int sum = 0;

            for (int i = 0; i < a.Length; i++)
            {
                sum += a[i].Equals(b[i]) ? 0 : 1;
            }

            return sum;
        }

        public static int Calculate(long a, long b)
        {
            long c = a ^ b;
            int sum = 0;

            while (c > 0)
            {
                sum += (c & 1) == 1 ? 1 : 0; ;
                c >>= 1;
            }

            return sum;
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
