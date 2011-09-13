using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chemistry_Studio
{
    public class ParseTree : ICloneable
    {
        public Node root;
        public List<Node> holeList;

        public ParseTree(Node root)
        {
            this.root = root;
            this.holeList = new List<Node>();
        }

        public static void DFSHoleClone(List<Node> holeList, Node root)
        {
            if (root.isHole == true) holeList.Add(root);
            else
            {
                foreach (Node temp in root.children)
                {
                    DFSHoleClone(holeList, temp);
                }
            }
        }

        public Object Clone()
        {
            ParseTree newTree = new ParseTree((Node)this.root.Clone());
            DFSHoleClone(newTree.holeList, newTree.root);
            return newTree;
        }

        public override string ToString()
        {
            return root.ToString();
        }
    }

    public class Node : ICloneable
    {
        public bool isHole;
        public string outputType;
        public string data;
        public List<Node> children;

        public Node()
        {
            this.isHole = true;
            this.outputType = "null";

        }

        public void holeFill(string label)
        {
            this.isHole = false;
            this.data = label;
            this.children = new List<Node>();
            
            List<string> param = Tokens.inputTypePredicates[label];
            if (param[0] != "null")
            {
                foreach (string x in param)
                {
                    Node tempNode = new Node();     //check that it appends to end of list
                    this.children.Add(tempNode);
                    tempNode.outputType = (string)x.Clone();
                }
            }
        }

        public Object Clone()
        {
            Node newNode = new Node();
            newNode.isHole = this.isHole;
            newNode.data = this.data;
            newNode.outputType = this.outputType;
            if(this.children!=null)
                newNode.children = this.children.Select(i => (Node)i.Clone()).ToList();
            return newNode;
        }

        public override string ToString()
        {
            string output = this.data;
            if (this.children.Count != 0)
            {
                output += "(";
                
                for (int i=0;i<this.children.Count;i++)
                {
                    output = output + this.children[i].ToString();
                    if (i != this.children.Count - 1)
                        output = output + ",";
                }
                output = output + ")";
            }
            return output;
        }
    }
}
