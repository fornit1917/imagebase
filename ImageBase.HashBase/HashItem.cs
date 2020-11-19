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
    }
}
