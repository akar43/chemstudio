using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chemistry_Studio
{
    public class ParseTree : ICloneable, IComparable<ParseTree>
    {
        public Node root;
        public List<Node> holeList;
        public double confidence;

        public ParseTree(Node root)
        {
            this.confidence = 1;
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
            newTree.confidence = this.confidence;
            return newTree;
        }

        public int CompareTo(ParseTree otherTree)
        {
            return this.confidence.CompareTo(otherTree.confidence); 
        }

        public override string ToString()
        {
            return root.ToString();
        }

        public void standardForm()
        {
            this.root.standardForm();
        }

        public bool isEqual(ParseTree otherTree)
        {
            return this.root.isEqual(otherTree.root);
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
            this.data = "Hole";
            this.isHole = true;
            this.outputType = "bool";
        }

        /*
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
                    tempNode.outputType = x;
                }
            }
        }*/

        public void holeFill(ParseTree subtree)
        {
            this.isHole = false;
            this.outputType = subtree.root.outputType;
            this.data = subtree.root.data;
            this.children = subtree.root.children.Select(i => (Node)i.Clone()).ToList();
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
            string output;
            if (this.isHole) { output = "Hole"; return output; }
            output = this.data;
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

        // to be called by root of a parsetree only
        public void standardForm()
        {
            List<string> inVec = new List<string>();
            List<Node> tempList = new List<Node>();
            foreach (Node t in this.children)
            {
                tempList.Add(t);
                inVec.Add(t.data);
            }
           
            string temp;
            int tempPos;
            List<int> sortedPerm=new List<int>();
            for (int i = 0; i < inVec.Count; i++)
                sortedPerm.Add(i);
            int j;
            for (int i = 0; i < inVec.Count - 1; i++)
            {
                temp = inVec[i]; tempPos = i;
                for (j = i + 1; j < inVec.Count; j++)
                {
                    if (inVec[j].CompareTo(temp) < 0)
                    {
                        temp = inVec[j];
                        tempPos = j;
                    }
                }
                string temp2 = inVec[i];
                inVec[i] = inVec[tempPos];
                inVec[tempPos] = temp2;

                int temp3;
                temp3 = sortedPerm[i];
                sortedPerm[i] = sortedPerm[tempPos];
                sortedPerm[tempPos] = temp3;
            }

            this.children = new List<Node>();
            for(int i=0; i<tempList.Count; i++)
                this.children.Add(tempList[sortedPerm[i]]);
            foreach(Node t in this.children)
                t.standardForm();
        }

        //to be called only by parseTree
        public bool isEqual(Node other)
        {
            bool flag = false;
            if (this.data == other.data && this.children.Count == other.children.Count)
            {
                flag = true;
                int limit = this.children.Count;
                for (int i = 0; i < limit; i++)
                    flag = flag && this.children[i].isEqual(other.children[i]);
            }
            return flag;
        }
    }
}
