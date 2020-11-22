using ImageBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageBase.HashBase
{
    public class VPTree
    {
        public VPTree(IEnumerable<HashItem> items)
        {
            Data = items;
            CreteNode();
        }

        /// <summary>
        /// A list of data points it contains
        /// </summary>
        public IEnumerable<HashItem> Data { get; set; }

        /// <summary>
        /// A vantage point chosen from data
        /// </summary>
        public HashItem VantagePoint { get; set; }

        /// <summary>
        /// A radius value defining the range of the node
        /// </summary>
        public int Mu { get; set; }

        /// <summary>
        /// The left subtree
        /// </summary>
        public VPTree Inside { get; set; }

        /// <summary>
        /// The right subtree
        /// </summary>
        public VPTree Outside { get; set; }

        private void CreteNode()
        {
            List<HashItem> items = Data.ToList();

            int itemCount = items.Count;
            int vantagePointIndex = new Random().Next(0, itemCount - 1);

            VantagePoint = items[vantagePointIndex];

            items.Remove(VantagePoint);
            itemCount = items.Count;
            if (itemCount == 0)
                return;

            int median = itemCount / 2;

            items.Sort(new HashComparer(VantagePoint));

            Mu = HammingDistance.Calculate(items[median].Hash, VantagePoint.Hash);

            if (median > 0)
                Inside = new VPTree(items.GetRange(0, median));

            Outside = new VPTree(items.GetRange(median, itemCount - median));
        }

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
                    var result = HammingDistance.Calculate(x.Hash, vantagePoint.Hash).CompareTo(HammingDistance.Calculate(y.Hash, vantagePoint.Hash));
                    if (result != 0) return result;
                }

                return x.ObjectId.CompareTo(y.ObjectId);
            }
        }
    }
}
