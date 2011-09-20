using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chemistry_Studio
{
    class Pos_Conf
    {
        public List<double> conf;
        public List<int> positions;

        public Pos_Conf(double confidence, int position)
        {
            conf=new List<double>();
            positions=new List<int>();
            conf.Add(confidence);
            positions.Add(position);
        }

        public void update(double confidence, int position)
        {
            positions.Add(position);
            conf.Add(confidence);
        }

    }
    
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

        static Dictionary<string,Pos_Conf> tokenFind(List<string> sentence)
        {
            int flag,minLength;
            List<string> token;
            int startPos = 0;
            Dictionary<string,Pos_Conf> predicates = new Dictionary<string,Pos_Conf>();
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
                        if (predicates.ContainsKey(pair.Value))
                        {
                            predicates[pair.Value].update(conf, startPos);
                        }
                        else
                        {
                            Pos_Conf temp = new Pos_Conf(conf, startPos);
                            predicates.Add(pair.Value, temp);
                        }
                        
                        flag = 1;
                        if (minLength > token.Count)
                            minLength = token.Count;
                    }
                }
                if (flag == 0)
                {
                    sentence.RemoveAt(0);
                    startPos += 1;
                }
                else
                {
                    sentence.RemoveRange(0, minLength);
                    startPos += minLength;
                }
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

        /*static List<int> numPredicatesFind(List<string> sentence, List<int> predsPos)
        {
            List<string> 
            
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

        public static void typeSafe(List<string> unusedTokens, ParseTree tree, List<string> allTokens)
        {
            if (tree.confidence < 0.4) return;
            if (tree.holeList.Count == 0)
                { completeTrees.Add(tree); return; }

            if (unusedTokens.Count() == 0)
            {
                //tokens finished but there are holes
                foreach (string tok in allTokens)
                {
                    if (Tokens.outputTypePredicates[tok] == tree.holeList[0].outputType)
                    {
                        List<string> newTokens = new List<string>();
                        ParseTree newTree = (ParseTree)tree.Clone();
                        newTree.holeList[0].holeFill(tok);
                        newTree.confidence *= 0.9;
                        typeSafe(newTokens, (ParseTree)newTree.Clone(), allTokens);
                    }
                }
            }
            else
            {
                bool flag = false;
                foreach (string tok in unusedTokens)
                {
                    //Console.WriteLine("{0}\t{1}", Tokens.outputTypePredicates[tok], tree.holeList[0].outputType);
                    if (Tokens.outputTypePredicates[tok] == tree.holeList[0].outputType)
                    {
                        flag = true;
                        List<string> newTokens = unusedTokens.Select(i => (string)i.Clone()).ToList();
                        newTokens.Remove(tok);

                        ParseTree newTree = (ParseTree)tree.Clone();
                        newTree.holeList[0].holeFill(tok);
                        typeSafe(newTokens, (ParseTree)newTree.Clone(), allTokens);
                    }
                }
                if (flag == false)
                {
                    foreach (string tok in allTokens)
                    {
                        if (Tokens.outputTypePredicates[tok] == tree.holeList[0].outputType)
                        {
                            List<string> newTokens = unusedTokens.Select(i => (string)i.Clone()).ToList();
                            ParseTree newTree = (ParseTree)tree.Clone();
                            newTree.holeList[0].holeFill(tok);
                            newTree.confidence *= 0.9;
                            typeSafe(newTokens, (ParseTree)newTree.Clone(), allTokens);
                        }
                    }
                }
            }
        }

        public static void Main(string[] args)
        {
            Tokens.initialize();
            Tokens.initializePredSpec();
            
            /*string sentence = "Which element has the highest ionisation energy highest?";
            sentence = sentence.ToLower();
            List<string> splitWords = tokenize(sentence);
            Dictionary<string,Pos_Conf> tokensConf = tokenFind(splitWords);
            //List<string> tokens = tokensConf.Keys.ToList();
            foreach (KeyValuePair<string, Pos_Conf> temp in tokensConf)
            {
                for (int i = 0; i < temp.Value.conf.Count; i++)
                    Console.WriteLine(temp.Key + "\t" + temp.Value.conf[i] + "\t" + temp.Value.positions[i]);
            }
            Console.ReadLine();
            //List<string> tokens = new List<string>(new String[] { "x", "IonicRadius", "y", "IE", "Same" });
            */

            ParseTree tree = new ParseTree(new Node());             
            //Console.Write("Please enter the tokens you want to assemble: ");
            //string input = Console.ReadLine();
            //List<string> tokens = tokenize(input);

            List<string> tokens = new List<string>(args);

            //string[] args1 = { "Max", "x", "IE" };
            //List<string> tokens = new List<string>(args);
            try
            {
                typeSafe(tokens, (ParseTree)tree.Clone(), tokens);
                string output = "";
                completeTrees.Sort();
                completeTrees.Reverse();
                foreach (ParseTree x in completeTrees)
                {
                    output = output + x + " Confidence = " + x.confidence + " \n";
                }

                Console.WriteLine(output);
            }
            catch(Exception e)
            {
                Console.WriteLine("Program Crashed! with message : " + e.ToString());
            }
            //Console.ReadLine();
        }
    }
}
