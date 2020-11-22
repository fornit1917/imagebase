using System.Collections.Generic;
using ImageBase.Common;

namespace ImageBase.HashBase
{
    public class HashComparer : IComparer<HashItem>
    {
        private readonly HashItem vantagePoint;

        public HashComparer(HashItem vantagePoint)
        {
            this.vantagePoint = vantagePoint;
        }

        public int Compare(HashItem x, HashItem y)
        {
            if (x.Hash != y.Hash)
            {
                var result = HammingDistance.Calculate(x.Hash, vantagePoint.Hash)
                    .CompareTo(HammingDistance.Calculate(y.Hash, vantagePoint.Hash));
                if (result != 0) return result;
            }

            return x.ObjectId.CompareTo(y.ObjectId);
        }
    }
}