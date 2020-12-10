using System;
using System.Collections.Generic;
using System.Text;

namespace ImageBase.HashBase
{
    public struct HashItem
    {
        /// <summary>
        /// id for object in database
        /// </summary>
        public long ObjectId { get; set; }

        public long Hash { get; set; }

        public override bool Equals(object obj)
        {
            HashItem item = (HashItem) obj;
            return ObjectId == item.ObjectId && Hash == item.Hash;
        }
    }
}