using System;
using System.Collections.Generic;
using System.Text;

namespace ImageBase.HashBase
{
    interface IHashBase
    {
        /// <summary>
        /// Creates in memory VP-Tree for specified hashes.
        /// </summary>
        /// <param name="items"></param>
        void CreateIndex(IEnumerable<HashItem> items);

        /// <summary>
        /// Returns list of objects IDs
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="radius"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        IReadOnlyList<long> Search(long hash, int radius, int limit); 
    }
}
