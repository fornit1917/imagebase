using ImageBase.Common;
using System.Collections.Generic;

namespace ImageBase.HashBase
{
    public class VPTreeHashBase : IHashBase
    {
        private VPTree root;

        public void CreateIndex(IEnumerable<HashItem> items)
        {
            root = new VPTree(items, 100);
        }

        public IReadOnlyList<long> Search(long hash, int radius, int limit)
        {
            var allItems = new List<HashItem>();
            var resultIDs = new List<long>(limit);
            SearchAllIDs(root, allItems, hash, radius);
            allItems.Sort(new HashComparer(new HashItem() { Hash = hash }));
            for (int i = 0; i < limit && i < allItems.Count; i++)
            {
                resultIDs.Add(allItems[i].ObjectId);
            }

            return resultIDs;
        }

        public void Add(HashItem item)
        {
            SearchToAdd(root, item);
        }

        public void Remove(HashItem item)
        {
            SearchToRemove(root, item);
        }

        private void SearchAllIDs(VPTree node, List<HashItem> resultIDs, long hash, int radius)
        {
            int centerToPointDistance = HammingDistance.Calculate(node.VantagePoint, hash);

            if ((centerToPointDistance - radius) > node.Radius)
            {
                if (node.Outside != null)
                {
                    SearchAllIDs(node.Outside, resultIDs, hash, radius);
                    return;
                }
                if (node.Items != null)
                {
                    foreach (var point in node.Items)
                    {
                        var dist = HammingDistance.Calculate(point.Hash, hash);
                        if (dist <= radius) resultIDs.Add(point);
                    }
                    return;
                }
            }

            if ((centerToPointDistance + radius) < node.Radius)
            {
                if (node.Inside != null)
                {
                    SearchAllIDs(node.Inside, resultIDs, hash, radius);
                    return;
                }

                if (node.Items != null)
                {
                    foreach (var point in node.Items)
                    {
                        var dist = HammingDistance.Calculate(point.Hash, hash);
                        if (dist <= radius) resultIDs.Add(point);
                    }
                    return;
                }

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
                if (node.Items != null)
                {
                    foreach (var point in node.Items)
                    {
                        var dist = HammingDistance.Calculate(point.Hash, hash);
                        if (dist <= radius) resultIDs.Add(point);
                    }
                }
            }
        }
       
        private void SearchToAdd(VPTree node, HashItem item)
        {
            int centerToPointDistance = HammingDistance.Calculate(node.VantagePoint, item.Hash);

            if (centerToPointDistance > node.Radius)
            {
                if (node.Outside != null)
                {
                    SearchToAdd(node.Outside, item);
                }

                node.Items?.Add(item);
            }
            else if (centerToPointDistance < node.Radius)
            {
                if (node.Inside != null)
                {
                    SearchToAdd(node.Inside, item);
                }

                node.Items?.Add(item);
            }
            else if (node.Outside != null)
            {
                SearchToAdd(node.Outside, item);
            }
            else if (node.Inside != null)
            {
                SearchToAdd(node.Inside, item);
            }
            else
            {
                node.Items?.Add(item);
            }
        }

        private void SearchToRemove(VPTree node, HashItem item)
        {
            int centerToPointDistance = HammingDistance.Calculate(node.VantagePoint, item.Hash);

            if ((centerToPointDistance) > node.Radius)
            {
                if (node.Outside != null)
                {
                    SearchToRemove(node.Outside, item);
                    return;
                }
                if (node.Items != null)
                {
                    node.Items.Remove(item);
                    return;
                }
            }

            if ((centerToPointDistance) < node.Radius)
            {
                if (node.Inside != null)
                {
                    SearchToRemove(node.Inside, item);
                    return;
                }

                if (node.Items != null)
                {
                    node.Items.Remove(item);
                    return;
                }
            }
            if (node.Outside != null)
            {
                SearchToRemove(node.Outside, item);
            }
            if (node.Inside != null)
            {
                SearchToRemove(node.Inside, item);
            }

            node.Items?.Remove(item);
        }
    }
}