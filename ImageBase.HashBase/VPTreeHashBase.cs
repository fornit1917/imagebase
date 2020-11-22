using ImageBase.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageBase.HashBase
{
    public class VPTreeHashBase : IHashBase
    {
        public VPTree Root { get; set; }

        public void CreateIndex(IEnumerable<HashItem> items)
        {
            Root = new VPTree(items);
        }

        public IReadOnlyList<long> Search(long hash, int radius, int limit)
        {
            var allItems = new List<HashItem>();
            var resultIDs = new List<long>(limit);

            SearchAllIDs(Root, allItems, hash, radius);
            allItems.Sort(new VPTree.HashComparer(new HashItem() { ObjectId = -1, Hash = hash }));
            
            for (int i = 0; i < limit && i < allItems.Count; i++)
            {
                resultIDs.Add(allItems[i].ObjectId);
            }
            
            return resultIDs;
        }

        private void SearchAllIDs(VPTree node, List<HashItem> resultIDs, long hash, int radius)
        {
            int centerToPointDistance = HammingDistance.Calculate(node.VantagePoint.Hash, hash);

            if ((centerToPointDistance - radius) > node.Mu)
            {
                if (node.Outside != null)
                {
                    SearchAllIDs(node.Outside, resultIDs, hash, radius);
                }
                return;
            }

            if ((centerToPointDistance + radius) < node.Mu)
            {
                if (node.Inside != null)
                {
                    SearchAllIDs(node.Inside, resultIDs, hash, radius);
                }
                if (centerToPointDistance <= radius)
                {
                    resultIDs.Add(node.VantagePoint);
                }
                return;
            }

            if (node.Outside != null)
            {
                SearchAllIDs(node.Outside, resultIDs, hash, radius);
            }
            if (node.Inside != null)
            {
                SearchAllIDs(node.Inside, resultIDs, hash, radius);
            }
            if (centerToPointDistance <= radius)
            {
                resultIDs.Add(node.VantagePoint);
            }
        }
    }
}
