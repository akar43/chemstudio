using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chemistry_Studio
{
    public class Node
    {
        string outputType;
        string data;
        List<Node> children;

        public Node()
        {
            children = new List<Node>();
        }

        public Node(string label)
        {
            data = label;
            children = new List<Node>();
        }
    }
}
