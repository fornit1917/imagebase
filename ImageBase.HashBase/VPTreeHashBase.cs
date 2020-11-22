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
            throw new NotImplementedException();
        }
    }
}
