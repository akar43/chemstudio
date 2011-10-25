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
                if (root.children != null)
                {
                    foreach (Node temp in root.children)
                    {
                        DFSHoleClone(holeList, temp);
                    }
                }
            }
        }

        public Object Clone()
        {
            ParseTree newTree = new ParseTree((Node)this.root.Clone(null));
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

        public string XMLForm()
        {
            string output = "";
            output += "<root>\n" + root.XMLForm() + "<root>\n";
            return output;
        }

        /*
        public void standardForm()
        {
            this.root.standardForm();
        }*/

        public bool isEqual(ParseTree otherTree)
        {
            return this.root.isEqual(otherTree.root);
        }
    }

    public class Node
    {
        public bool isHole;
        public string outputType;
        public string data;
        public List<Node> children;
        public Node parent;

        public Node()
        {
            this.parent = null;
            this.data = "ZZ"; // zz as data represents a hole, reson for naming so is for lexical ordering
            this.isHole = true;
            this.outputType = "bool";
        }

        public Node(Node parent)
        {
            this.parent = parent;
            this.data = "ZZ";
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
            this.children = subtree.root.children.Select(i => (Node)i.Clone(this)).ToList();
        }

        public Object Clone(Node parent) //clones the subtree rooted at this node
        {
            Node newNode = new Node();
            newNode.parent = parent;
            newNode.isHole = this.isHole;
            newNode.data = this.data;
            newNode.outputType = this.outputType;
            if(this.children!=null)
                newNode.children = this.children.Select(i => (Node)i.Clone(newNode)).ToList();
            return newNode;
        }

        public override string ToString()
        {
            string output;
            if (this.isHole) { output = "Hole"; return output; }
            output = this.data;
            if (this.children == null)
                return output;
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

        public string XMLForm()
        {
            string output = "" ;
            if (this.children == null || this.children.Count == 0)
                output += "<leaf> " + this.data + " </leaf>\n";
            else
            {
                output += "<" + this.data + ">\n";
                foreach (Node t in this.children)
                    output += t.XMLForm();
                output += "</" + this.data + ">\n";
            }
            return output;
        }

        public string valueOfSubtree()
        {
            string output;
            if (this.isHole) { output = "ZZ"; return output; }
            output = this.data;
            if (this.children == null)
                return output;
            if (this.children.Count != 0)
            {
                output += "(";

                for (int i = 0; i < this.children.Count; i++)
                {
                    output = output + this.children[i].valueOfSubtree();
                    if (i != this.children.Count - 1)
                        output = output + ",";
                }
                output = output + ")";
            }
            return output;
        }

        public bool isVariableInSubtree()
        {
            if (Tokens.variableTokens.Contains(this.data)) return true;

            bool flag = false;
            if (this.children != null)
            {
                foreach(Node temp in this.children)
                {
                    flag = flag | temp.isVariableInSubtree();
                }
            }
            return flag;
        }

        public bool checkForPermutation()
        {
            int num = this.children.Count;
            for (int i =1; i < num; i++)
            {
                for (int j = i - 1; j < i; j++)
                {
                    if (this.children[i].outputType == this.children[j].outputType)
                    {   
                        if (string.Compare(this.children[i].valueOfSubtree() , this.children[j].valueOfSubtree()) < 0) return false;
                    }
                }
            }

            if (this.parent != null) return this.parent.checkForPermutation();
            else return true;
        }

        /*
        // to be called by root of a parsetree only
        public void standardForm()
        {
            List<string> inVec = new List<string>();
            List<Node> tempList = new List<Node>();
            if (this.children == null)
                return;
            foreach (Node t in this.children)
            {
                tempList.Add(t);
                inVec.Add(t.data);
            }
            this.children = new List<Node>();
           
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

            for(int i=0; i<tempList.Count; i++)
                this.children.Add(tempList[sortedPerm[i]]);
            foreach(Node t in this.children)
                t.standardForm();
        }
        */

        //to be called only by parseTree
        public bool isEqual(Node other)
        {
            bool flag = false;

            if (this.data == other.data)
            {
                flag = true;
                if (this.children != null)
                {
                    int limit = this.children.Count;
                    for (int i = 0; i < limit; i++)
                        flag = flag && this.children[i].isEqual(other.children[i]);
                }
            }
            return flag;
        }
    }
}
