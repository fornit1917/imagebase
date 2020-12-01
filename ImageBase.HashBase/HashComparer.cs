using System.Collections.Generic;
using ImageBase.Common;

namespace ImageBase.HashBase
{
    public class HashComparer : IComparer<HashItem>
    {
        private readonly long vantagePointHash;

        public HashComparer(HashItem vantagePoint)
        {
            this.vantagePointHash = vantagePoint.Hash;
        }

        public int Compare(HashItem x, HashItem y)
        {
            var result = HammingDistance.Calculate(x.Hash, vantagePointHash)
                .CompareTo(HammingDistance.Calculate(y.Hash, vantagePointHash));

            return result;
        }
    }
}