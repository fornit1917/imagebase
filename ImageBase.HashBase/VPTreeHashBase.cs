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
            var allIDs = new List<long>();

            SearchInNode(Root, allIDs, hash, radius);

            var resultIDs = new List<long>(limit);
            for (int i = 0; i < limit && i < allIDs.Count; i++)
            {
                resultIDs.Add(allIDs[i]);
            }
            
            return allIDs;
        }

        private void SearchInNode(VPTree node, List<long> resultIDs, long hash, int radius)
        {
            int centerToPointDistance = HammingDistance.Calculate(node.VantagePoint.Hash, hash);

            if ((centerToPointDistance - radius) > node.Mu)
            {
                if (node.Outside != null)
                {
                    SearchInNode(node.Outside, resultIDs, hash, radius);
                }
                return;
            }

            if ((centerToPointDistance + radius) < node.Mu)
            {
                if (node.Inside != null)
                {
                    SearchInNode(node.Inside, resultIDs, hash, radius);
                }
                if (centerToPointDistance < radius)
                {
                    resultIDs.Add(node.VantagePoint.ObjectId);
                }
                return;
            }

            if (node.Outside != null)
            {
                SearchInNode(node.Outside, resultIDs, hash, radius);
            }
            if (node.Inside != null)
            {
                SearchInNode(node.Inside, resultIDs, hash, radius);
            }
            if (centerToPointDistance < radius)
            {
                resultIDs.Add(node.VantagePoint.ObjectId);
            }
        }
    }
}
