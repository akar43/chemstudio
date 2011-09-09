using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chemistry_Studio
{
    public class Node : ICloneable
    {
        public string outputType;
        public string data;
        public List<Node> children;
        public Node parent;

        public Node(Node parent)
        {
            this.parent = parent;
            children = new List<Node>();
        }

        public Node(string label, Node parent)
        {
            this.parent = parent;
            data = label;
            children = new List<Node>();
        }

        public Object Clone()
        {
            Node newNode = new Node(this.data, this.parent);
            newNode.outputType = this.outputType;
            newNode.children = this.children.Select(i => (Node)i.Clone()).ToList();
            return newNode;
        }
    }
}
