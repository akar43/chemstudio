﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chemistry_Studio
{
    class Program
    {
        static List<ParseTree> completeTrees=new List<ParseTree>();
        static Char[] delims = {' ',',',':','?','.','-'};
        static double threshold = 0.75;
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

        static List<int> sortPerm(List<int> inVec)
        {
            int temp, tempPos;
            List<int> sortedPerm=new List<int>();

            for (int i = 0; i < inVec.Count; i++)
                sortedPerm.Add(i);
            int j;
            for (int i = 0; i < inVec.Count - 1; i++)
            {
                temp = inVec[i]; tempPos = i;
                for (j = i + 1; j < inVec.Count; j++)
                {
                    if (inVec[j] < temp)
                    {
                        temp = inVec[j];
                        tempPos = j;
                    }
                }
                int temp11 = inVec[i];
                inVec[i] = inVec[tempPos];
                inVec[tempPos] = temp11;

                temp11 = sortedPerm[i];
                sortedPerm[i] = sortedPerm[tempPos];
                sortedPerm[tempPos] = temp11;
            }

            return sortedPerm;
        }

        static List<int> numberPos(List<string> subStr, List<int> numsInQues)
        {
	        List<int> positions=new List<int>();
            int temp = 0;
	        for (int i =0; i<subStr.Count; i++)
		        if (int.TryParse(subStr[i],out temp)) {
			        positions.Add(i);
			        numsInQues.Add(temp);
		        }
	        return positions;
        }

        static List<int> distOrder(int pos, List<int> numPreds)
        {
	        List<int> distances=new List<int>();
            for(int i=0;i<numPreds.Count;i++)
            {
        
                distances.Add(Math.Abs(pos-numPreds[i]));
            }
            List<int> sortedDist = sortPerm(distances);
            return sortedDist;   
        }

        static Dictionary<string,double> tokenFind(List<string> sentence)
        {
            int flag,minLength;
            List<string> token;
            Dictionary<string,double> predicates = new Dictionary<string,double>();
            while (sentence.Count != 0)
            {
                
                flag = 0; minLength = int.MaxValue;
                foreach (KeyValuePair<string, string> pair in Tokens.tokenList)
                {
                    token = tokenize(pair.Key);
                    if (sentence.Count < token.Count)
                        continue;
                    double conf = listMatch(sentence, token, token.Count);
                    
                    if (conf > 0.75)
                    {
                        predicates.Add(pair.Value,conf);
                        
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

        static Dictionary<int,List<string>> numFind(List<string> sentence, List<int> predsPos, List<string> predsOrder)
        {
            List<int> numsInQues = new List<int>();
            List<int> numPos = numberPos(sentence, numsInQues);
            Dictionary<int, List<string>> numOrder = new Dictionary<int, List<string>>();
            for (int i = 0; i < numPos.Count; i++)
            {
                List<int> temp = distOrder(numPos[i], predsPos);
                List<string> sortedOrder = new List<string>();
                for (int j = 0; j < temp.Count; j++)
                    sortedOrder.Add(predsOrder[temp[j]]);
                numOrder.Add(numsInQues[i], sortedOrder);
            }
            return numOrder;
        }

        
        /*static List<int> substringMatch(List<string> sentence, string substring)
        {
            List<string> substrList = tokenize(substring);
        }


        static List<int> numPredicatesFind(List<string> sentence, List<int> predsPos)
        {
            
            
        }*/

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
            Tokens.initialize();
            Tokens.initializePredSpec();
            /*
            string sentence = "Which element has the highest ionisation energy?";
            sentence = sentence.ToLower();
            List<string> splitWords = tokenize(sentence);
            Dictionary<string,double> tokensConf = tokenFind(splitWords);
            //List<string> tokens = tokensConf.Keys.ToList();
            foreach(KeyValuePair<string,double> temp in tokensConf)
                Console.WriteLine(temp.Key+"\t"+temp.Value);
            Console.ReadLine();
            //List<string> tokens = new List<string>(new String[] { "x", "IonicRadius", "y", "IE", "Same" });
             * */
            ParseTree tree = new ParseTree(new Node());             
            Console.Write("Please enter the tokens you want to assemble: ");
            string input = Console.ReadLine();
            List<string> tokens = tokenize(input);

            typeSafe(tokens, (ParseTree)tree.Clone());
            foreach (ParseTree x in completeTrees)
            {
                Console.WriteLine(x);
            }
            Console.ReadLine();
        }
    }
}
