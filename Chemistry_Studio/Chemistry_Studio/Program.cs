using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chemistry_Studio
{
    class Program
    {
        static List<ParseTree> completeTrees=new List<ParseTree>();
        static Char[] delims = {' ',',',':','?','.','-'};
        private static bool remNullStr(String s)
        {
            if (s.Length==0)
                return true;
            else return false;
        }
        
        static List<string> tokenize(string inString)
        {
            string[] split = inString.Split(delims);
            List<string> wordList = new List<string>(split);
            wordList.RemoveAll(remNullStr);
            return wordList;
        }
        
        static double listMatch(List<string> sentence, List<string> tokens, int wordMatch)
        {
            double conf = 1.0;
            
            for (int i=0;i<wordMatch;i++)
            {
                conf*=LD.Compute(tokens[i],sentence[i]);
                
            }
            return conf;
        }

        static List<string> tokenFind(List<string> sentence)
        {
            int flag,minLength;
            List<string> token;
            List<string> predicates = new List<string>();
            while (sentence.Count != 0)
            {
                
                flag = 0; minLength = 100;
                foreach (KeyValuePair<string, string> pair in Tokens.tokenList)
                {
                    token = tokenize(pair.Key);
                    if (sentence.Count < token.Count)
                        continue;
                    double conf = listMatch(sentence, token, token.Count);
                    
                    if (conf > 0.75)
                    {
                        predicates.Add(pair.Value);
                        
                        flag = 1;
                        if (minLength > token.Count)
                            minLength = token.Count;
                    }
                }
                if (flag == 0)
                    sentence.RemoveAt(0);
                else
                    sentence.RemoveRange(0, minLength);
            }
            return predicates;
        }

        public static Node findNextHole(Node root)
        {
            if (root.isHole == true) return root;
            else
            {
                foreach (Node temp in root.children)
                {
                    Node leftHole = findNextHole(temp);
                    if (leftHole != null) return leftHole;
                }
                return null;
            }
        }

        public static void typeSafe(List<string> tokens, ParseTree tree)
        {
            if (tokens.Count() == 0) { completeTrees.Add(tree); return; }
            else
            {
                foreach (string tok in tokens)
                {
                    //Console.WriteLine("{0}\t{1}", Tokens.outputTypePredicates[tok], tree.holeList[0].outputType);
                    if(Tokens.outputTypePredicates[tok] == tree.holeList[0].outputType)
                    {
                        List<string> newTokens = tokens.Select(i => (string)i.Clone()).ToList();
                        newTokens.Remove(tok);

                        ParseTree newTree = (ParseTree)tree.Clone();
                        newTree.holeList[0].holeFill(tok);
                        typeSafe(newTokens, (ParseTree) newTree.Clone());
                    }
                }
            }
        }

        public static void Main(string[] args)
        {
           // string sentence = "Which element has the highest ionisation energy?";
           // Tokens.initialize();
            Tokens.initializePredSpec();
           // sentence = sentence.ToLower();
           // List<string> splitWords = tokenize(sentence);
            
          //  foreach(string temp in tokenFind(splitWords))
          //      Console.WriteLine(temp);
          //  Console.ReadLine();
            List<string> tokens = new List<string>(new String[] { "IE", "x", "MAX" });
            ParseTree tree = new ParseTree(new Node());
            //tree.root.children
            typeSafe(tokens, (ParseTree)tree.Clone());
            foreach (ParseTree x in completeTrees)
            {
                Console.WriteLine(x);
            }
            //Console.ReadLine();
        }
    }
}
