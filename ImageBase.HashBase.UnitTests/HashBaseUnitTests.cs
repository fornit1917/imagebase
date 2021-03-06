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
                    new object[]
                    {
                        new List<HashItem>()
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
                        },
                        37, 2, 10,new List<long>() { 1, 3, 4, 5 }
                    },

                    new object[]
                    {
                        new List<HashItem>()
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
                        },
                        511, 2, 10,new List<long>() { 7, 8, 9 } },

                    new object[] {
                        new List<HashItem>()
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
                        },
                        31, 3, 10,new List<long>() { 2, 3, 5, 6, 7, 8 } },

                    new object[]
                    {
                        new List<HashItem>()
                        {                                                 //Hashes
                            new HashItem() { ObjectId = 1, Hash = 1 },    //1
                            new HashItem() { ObjectId = 2, Hash = 3 },    //11
                            new HashItem() { ObjectId = 3, Hash = 7 },    //11
                            new HashItem() { ObjectId = 4, Hash = 15 },   //111
                            new HashItem() { ObjectId = 5, Hash = 31 },   //1111
                            new HashItem() { ObjectId = 6, Hash = 63 },   //11111
                            new HashItem() { ObjectId = 7, Hash = 127 },  //111111
                            new HashItem() { ObjectId = 8, Hash = 255 },  //1111111
                            new HashItem() { ObjectId = 9, Hash = 511},   //11111111
                            new HashItem() { ObjectId = 10, Hash = 1023 },//111111111
                            new HashItem() { ObjectId = 11, Hash = 2047 },//1111111111
                            new HashItem() { ObjectId = 12, Hash = 4095 },//11111111111
                            new HashItem() { ObjectId = 13, Hash = 8191 },//111111111111
                        },
                        511, 1, 100,new List<long>() { 8, 9, 10 },
                    },

                    new object[]
                    {
                        new List<HashItem>()
                        {                                                 //Hashes
                            new HashItem() { ObjectId = 1, Hash = 1 },    //1
                            new HashItem() { ObjectId = 2, Hash = 3 },    //11
                            new HashItem() { ObjectId = 3, Hash = 7 },    //11
                            new HashItem() { ObjectId = 4, Hash = 15 },   //111
                            new HashItem() { ObjectId = 5, Hash = 31 },   //1111
                            new HashItem() { ObjectId = 6, Hash = 63 },   //11111
                            new HashItem() { ObjectId = 7, Hash = 127 },  //111111
                            new HashItem() { ObjectId = 8, Hash = 255 },  //1111111
                            new HashItem() { ObjectId = 9, Hash = 511},   //11111111
                            new HashItem() { ObjectId = 10, Hash = 1023 },//111111111
                            new HashItem() { ObjectId = 11, Hash = 2047 },//1111111111
                            new HashItem() { ObjectId = 12, Hash = 4095 },//11111111111
                            new HashItem() { ObjectId = 13, Hash = 8191 },//111111111111
                        },
                        511, 2, 100,new List<long>() { 7, 8, 9, 10, 11 }
                    },

                    new object[]
                    {
                        new List<HashItem>()
                        {                                                 //Hashes
                            new HashItem() { ObjectId = 1, Hash = 1 },    //1
                            new HashItem() { ObjectId = 2, Hash = 3 },    //11
                            new HashItem() { ObjectId = 3, Hash = 7 },    //11
                            new HashItem() { ObjectId = 4, Hash = 15 },   //111
                            new HashItem() { ObjectId = 5, Hash = 31 },   //1111
                            new HashItem() { ObjectId = 6, Hash = 63 },   //11111
                            new HashItem() { ObjectId = 7, Hash = 127 },  //111111
                            new HashItem() { ObjectId = 8, Hash = 255 },  //1111111
                            new HashItem() { ObjectId = 9, Hash = 511},   //11111111
                            new HashItem() { ObjectId = 10, Hash = 1023 },//111111111
                            new HashItem() { ObjectId = 11, Hash = 2047 },//1111111111
                            new HashItem() { ObjectId = 12, Hash = 4095 },//11111111111
                            new HashItem() { ObjectId = 13, Hash = 8191 },//111111111111
                        },
                        31, 4, 100,new List<long>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 }
                    },

                    new object[]
                    {
                        new List<HashItem>()
                        {                                                 //Hashes
                            new HashItem() { ObjectId = 1, Hash = 1 },    //1
                            new HashItem() { ObjectId = 2, Hash = 3 },    //11
                            new HashItem() { ObjectId = 3, Hash = 7 },    //11
                            new HashItem() { ObjectId = 4, Hash = 15 },   //111
                            new HashItem() { ObjectId = 5, Hash = 31 },   //1111
                            new HashItem() { ObjectId = 6, Hash = 63 },   //11111
                            new HashItem() { ObjectId = 7, Hash = 127 },  //111111
                            new HashItem() { ObjectId = 8, Hash = 255 },  //1111111
                            new HashItem() { ObjectId = 9, Hash = 511},   //11111111
                            new HashItem() { ObjectId = 10, Hash = 1023 },//111111111
                            new HashItem() { ObjectId = 11, Hash = 2047 },//1111111111
                            new HashItem() { ObjectId = 12, Hash = 4095 },//11111111111
                            new HashItem() { ObjectId = 13, Hash = 8191 },//111111111111
                        },
                        31, 4, 3,new List<long>() { 4, 5, 6 }
                    },

                new object[]
                {
                    new List<HashItem>()
                    {
                        //Hashes
                        new HashItem() {ObjectId = 1, Hash = 1}, //1
                        new HashItem() {ObjectId = 2, Hash = 3}, //11
                        new HashItem() {ObjectId = 3, Hash = 7}, //11
                        new HashItem() {ObjectId = 4, Hash = 15}, //111
                        new HashItem() {ObjectId = 5, Hash = 31}, //1111
                        new HashItem() {ObjectId = 6, Hash = 63}, //11111
                        new HashItem() {ObjectId = 7, Hash = 127}, //111111
                        new HashItem() {ObjectId = 14, Hash = 127}, //111111
                        new HashItem() {ObjectId = 15, Hash = 127}, //111111
                        new HashItem() {ObjectId = 16, Hash = 127}, //111111
                        new HashItem() {ObjectId = 17, Hash = 127}, //111111
                        new HashItem() {ObjectId = 8, Hash = 255}, //1111111
                        new HashItem() {ObjectId = 9, Hash = 511}, //11111111
                        new HashItem() {ObjectId = 10, Hash = 1023}, //111111111
                        new HashItem() {ObjectId = 11, Hash = 2047}, //1111111111
                        new HashItem() {ObjectId = 12, Hash = 4095}, //11111111111
                        new HashItem() {ObjectId = 13, Hash = 8191}, //111111111111
                    },
                    127, 4, 100, new List<long>() {3, 4, 5, 6, 7, 8, 9, 10, 11, 14, 15, 16, 17}
                },

                new object[]
                {
                    new List<HashItem>()
                    {
                        //Hashes
                        new HashItem() {ObjectId = 1, Hash = 1}, //1
                        new HashItem() {ObjectId = 2, Hash = 3}, //11
                        new HashItem() {ObjectId = 3, Hash = 7}, //11
                        new HashItem() {ObjectId = 4, Hash = 15}, //111
                        new HashItem() {ObjectId = 5, Hash = 31}, //1111
                        new HashItem() {ObjectId = 6, Hash = 63}, //11111
                        new HashItem() {ObjectId = 7, Hash = 127}, //111111
                        new HashItem() {ObjectId = 14, Hash = 127}, //111111
                        new HashItem() {ObjectId = 15, Hash = 127}, //111111
                        new HashItem() {ObjectId = 16, Hash = 127}, //111111
                        new HashItem() {ObjectId = 17, Hash = 127}, //111111
                        new HashItem() {ObjectId = 8, Hash = 255}, //1111111
                        new HashItem() {ObjectId = 9, Hash = 511}, //11111111
                        new HashItem() {ObjectId = 10, Hash = 1023}, //111111111
                        new HashItem() {ObjectId = 11, Hash = 2047}, //1111111111
                        new HashItem() {ObjectId = 12, Hash = 4095}, //11111111111
                        new HashItem() {ObjectId = 18, Hash = 4095}, //11111111111
                        new HashItem() {ObjectId = 13, Hash = 8191}, //111111111111
                    },
                    8191, 6, 100, new List<long>() {7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18}
                },
            };

        [Theory]
        [MemberData(nameof(Strings))]
        public void CalculatesForSearch(IEnumerable<HashItem> hashesInput, long hashSearch, int searchRadius, int limit, List<long> expectedIDs)
        {
            //check 100 times because vantage index can be different
            for (int i = 0; i < 100; i++)
            {
                VPTreeHashBase vpTreeHashBase = new VPTreeHashBase();
                vpTreeHashBase.CreateIndex(hashesInput);

                var result = vpTreeHashBase.Search(hashSearch, searchRadius, limit).OrderBy(x => x);

                Assert.Equal(expectedIDs, result);
            }
        }
    }
}