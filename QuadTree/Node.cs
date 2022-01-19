using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTree
{
    public enum Region {NW, NE, SW, SE }
    public class Node
    {
        public Node NW { get; set; }
        public Node NE { get; set; }
        public Node SW { get; set; }
        public Node SE { get; set; }
        public Colour C { get; set; }

        public Node(Colour color)
        {
            C = color; // 0 is Black, 1 is White, 2 is Grey
            NW = NE = SW = SE = null;
        }

    }
}
