using ImageBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageBase.HashBase
{
    public class VPTree
    {
        public VPTree(IEnumerable<HashItem> items, int limitCount)
        {
            this.limitCount = limitCount;
            allItemsList = items.ToList();

            startIndex = 0;
            endIndex = allItemsList.Count() - 1;

            CreateNode();
        }

        private VPTree(List<HashItem> items, int startIndex, int endIndex, int limitCount)
        {
            this.limitCount = limitCount;
            allItemsList = items;
            this.startIndex = startIndex;
            this.endIndex = endIndex;

            CreateNode();
        }

        private VPTree(List<HashItem> lists)
        {
            this.Items = lists;
        }

        /// <summary>
        /// Items of the current leaf
        /// </summary>
        public List<HashItem> Items { get; private set; }

        /// <summary>
        /// A vantage point chosen from data
        /// </summary>
        public long VantagePoint { get; private set; }
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
        private readonly List<HashItem> allItemsList;
        private readonly int limitCount;

        private void CreateNode()
        {
            int itemsCount = endIndex - startIndex + 1;

            int vantagePointIndex = new Random().Next(startIndex, endIndex);
            VantagePoint = allItemsList[vantagePointIndex].Hash;
            SwapItems(allItemsList, startIndex, vantagePointIndex);
         
            int median = (endIndex + startIndex) / 2;
            allItemsList.Sort(startIndex, itemsCount, new HashComparer(VantagePoint));
            Radius = HammingDistance.Calculate(allItemsList[median].Hash, VantagePoint);

            if (itemsCount > 2 * limitCount)
            {
                Inside = new VPTree(allItemsList, startIndex, median, limitCount);
                Outside = new VPTree(allItemsList, median + 1, endIndex, limitCount);
            }
            else
            {
                if (itemsCount > 1)
                {
                    int middleCount = itemsCount / 2;
                    Inside = new VPTree(allItemsList.GetRange(startIndex, middleCount));
                    Outside = new VPTree(allItemsList.GetRange(startIndex + middleCount, itemsCount - middleCount));
                }
                else
                {
                    Inside = new VPTree(allItemsList.GetRange(startIndex, 1));
                }
            }
        }

        private void SwapItems(List<HashItem> items, int index1, int index2)
        {
            var temp = items[index1];
            items[index1] = items[index2];
            items[index2] = temp;
        }
    }
}