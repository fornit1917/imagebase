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
            itemsList = items.ToList();

            startIndex = 0;
            endIndex = itemsList.Count() - 1;

            CreateNode();
        }

        private VPTree(List<HashItem> items, int startIndex, int endIndex)
        {
            itemsList = items;
            this.startIndex = startIndex;
            this.endIndex = endIndex;

            CreateNode();
        }

        /// <summary>
        /// A vantage point chosen from data
        /// </summary>
        public HashItem VantagePoint { get; private set; }

        /// <summary>
        /// A radius value defining the range of the node
        /// </summary>
        public int Radius { get; private set; }

        /// <summary>
        /// The left subtree
        /// </summary>
        public VPTree Inside { get; private set; }

        /// <summary>
        /// The right subtree
        /// </summary>
        public VPTree Outside { get; private set; }

        private readonly int startIndex;
        private readonly int endIndex;
        private readonly List<HashItem> itemsList;

        private void CreateNode()
        {
            int childrenCount = endIndex - startIndex;

            int vantagePointIndex = new Random().Next(startIndex, endIndex);
            VantagePoint = itemsList[vantagePointIndex];

            SwapItems(itemsList, startIndex, vantagePointIndex);

            if (childrenCount < 1)
                return;

            int median = (endIndex + startIndex) / 2;

            itemsList.Sort(startIndex + 1, childrenCount, new HashComparer(VantagePoint));

            Radius = HammingDistance.Calculate(itemsList[median].Hash, VantagePoint.Hash);

            if (median - startIndex > 0)
                Inside = new VPTree(itemsList, startIndex + 1, median);

            Outside = new VPTree(itemsList, median + 1, endIndex);
        }

        private void SwapItems(List<HashItem> items, int index1, int index2)
        {
            var temp = items[index1];
            items[index1] = VantagePoint;
            items[index2] = temp;
        }
    }
}
