using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace ImageBase.HashBase
{
    public class VPTree
    {
        /// <summary>
        /// A list of data points it contains
        /// </summary>
        public List<HashItem> Data { get; set; }

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
    }
}
