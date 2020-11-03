using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace ImageBase.Common.UnitTests
{
    public class HammingDistanceUnitTests
    {
        public static IEnumerable<object[]> Strings =>
            new List<object[]>
            {
                new object[] { new string('0',64), new string('0', 64), 0 },
                new object[] { new string('1',64), new string('0', 64), 64 },
                new object[] { new string('1',32)+ new string('0', 32),
                    new string('0', 32) + new string('1', 32), 64 },
            };

        [Theory]
        [MemberData(nameof(Strings))]
        public void CalculateString(string a, string b, int expected)
        {
            Assert.Equal(expected, HammingDistance.Calculate(a, b));
        }

        [Theory]
        [InlineData(long.MaxValue, 0, 63)]
        [InlineData(long.MaxValue, long.MaxValue, 0)]
        [InlineData(500, 501, 1)]
        [InlineData(500, 502, 1)]
        [InlineData(500, 600, 6)]
        [InlineData(254, 0, 7)]
        [InlineData(0, 0, 0)]
        public void CalculateLong(long a, long b, int expected)
        {
            Assert.Equal(expected, HammingDistance.Calculate(a, b));
        }

        public static IEnumerable<object[]> Bits =>
            new List<object[]>
            {
                new object[] {new BitArray(64),new BitArray(64), 0 },
                new object[] {new BitArray(64),new BitArray(64, true), 64 },
                new object[] {new BitArray(new[]{true, true, false, false}), new BitArray(new[] { false, true, false, false }), 1 },
                new object[] {new BitArray(new[]{true, true, false, false}), new BitArray(new[] { false, false, false, false }), 2 },
                new object[] {new BitArray(new[]{true, true, false, false}), new BitArray(new[] { false, false, false, true }), 3 }
            };

        [Theory]
        [MemberData(nameof(Bits))]
        public void CalculateBitArray(BitArray a, BitArray b, int expected)
        {
            Assert.Equal(expected, HammingDistance.Calculate(a, b));
        }
    }

}