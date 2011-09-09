using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chemistry_Studio
{
    public class Node : ICloneable
    {
        string outputType;
        string data;
        List<Node> children;
        Node parent;

        public Node()
        {
            children = new List<Node>();
        }

        public Node(string label)
        {
            data = label;
            children = new List<Node>();
        }

        public Node Clone()
        {
            Node newNode = (Node)this.MemberwiseClone();
            return newNode;
        }
    }
}
