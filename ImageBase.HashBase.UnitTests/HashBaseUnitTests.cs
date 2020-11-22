using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ImageBase.HashBase.UnitTests
{
    public class HashBaseUnitTests
    {
        public static IEnumerable<object[]> Strings =>
            new List<object[]>
            {
                new object[] {511,1,10,new List<long>() { 8, 9, 10 } },
                new object[] {511,2,10,new List<long>() { 7, 8, 9, 10, 11 } },
                new object[] {31,4,10,new List<long>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 } },
                new object[] {31,4,3,new List<long>() { 4, 5, 6 } }
            };

        [Theory]
        [MemberData(nameof(Strings))]
        public void CalculatesForSearch(long hash, int radius, int limit, List<long> expectadIDs)
        {
            IEnumerable<HashItem> hashes = new List<HashItem>()
            {
                new HashItem() { ObjectId = 1, Hash = 1 },
                new HashItem() { ObjectId = 2, Hash = 3 },
                new HashItem() { ObjectId = 3, Hash = 7 },
                new HashItem() { ObjectId = 4, Hash = 15 },
                new HashItem() { ObjectId = 5, Hash = 31 },
                new HashItem() { ObjectId = 6, Hash = 63 },
                new HashItem() { ObjectId = 7, Hash = 127 },
                new HashItem() { ObjectId = 8, Hash = 255 },
                new HashItem() { ObjectId = 9, Hash = 511},
                new HashItem() { ObjectId = 10, Hash = 1023 },
                new HashItem() { ObjectId = 11, Hash = 2047 },
                new HashItem() { ObjectId = 12, Hash = 4095 },
                new HashItem() { ObjectId = 13, Hash = 8191 },
            };
            VPTreeHashBase vpTreeHashBase = new VPTreeHashBase();
            vpTreeHashBase.CreateIndex(hashes);

            var res = vpTreeHashBase.Search(hash, radius, limit).OrderBy(x => x);
            Assert.Equal(expectadIDs, res);
        }
    }
}
