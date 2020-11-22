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
                new object[] {37,2,10,new List<long>() { 1, 3, 4, 5 } },
                new object[] {511,2,10,new List<long>() { 7, 8, 9 } },
                new object[] {31,3,10,new List<long>() { 2, 3, 5, 6, 7, 8 } }
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
                new HashItem() { ObjectId = 4, Hash = 37 },
                new HashItem() { ObjectId = 5, Hash = 53 },
                new HashItem() { ObjectId = 6, Hash = 63 },
                new HashItem() { ObjectId = 7, Hash = 127 },
                new HashItem() { ObjectId = 8, Hash = 255 },
                new HashItem() { ObjectId = 9, Hash = 511},
                new HashItem() { ObjectId = 10, Hash = 512},
                new HashItem() { ObjectId = 11, Hash = 500},
            };
            VPTreeHashBase vpTreeHashBase = new VPTreeHashBase();
            vpTreeHashBase.CreateIndex(hashes);

            var res = vpTreeHashBase.Search(hash, radius, limit).OrderBy(x => x);
            Assert.Equal(expectadIDs, res);
        }
    }
}
