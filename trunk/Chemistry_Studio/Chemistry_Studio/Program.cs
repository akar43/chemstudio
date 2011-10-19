using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chemistry_Studio
{
    //Structure to contain positions and confidences of tokens obtained during parse
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
        
        //Predicate to remove all null strings detected during the parse
        private static bool remNullStr(String s)
        {
            if (s.Length==0)
                return true;
            else return false;
        }
        
        //Tokenize the sentence
        static List<string> tokenize(string inString)
        {
            string[] split = inString.Split(delims);
            List<string> wordList = new List<string>(split);
            wordList.RemoveAll(remNullStr);
            return wordList;
        }
        
        //Compare two lists and returns confidence of comaprision
        static double listMatch(List<string> sentence, List<string> tokens, int wordMatch)
        {
           /* double conf = 1.0;
            
            for (int i=0;i<wordMatch;i++)
            {
                conf*=LD.Compute(tokens[i],sentence[i]);
                
            }
            return conf;*/
            string sent="";string toks="";
            for (int i = 0; i < wordMatch; i++)
            {
                sent = sent + " " + sentence[i];
                toks = toks + " " + tokens[i];
            }
            return LD.Compute(sent, toks);
        }


        //Gives the sorting permutation of a list
        public static List<int> sortPerm(List<int> inVec)
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

        //Returns positions of numbers in the sentence
        static List<int> numberPos(List<string> subStr, List<int> numsInQues)
        {
	        List<int> positions=new List<int>();
            int temp = 0;
            for (int i = 0; i < subStr.Count; i++)
            {
                if (int.TryParse(subStr[i], out temp))
                {
                    positions.Add(i);
                    numsInQues.Add(temp);
                }
            }
	        return positions;
        }

        //Returns the numeric predicates sorted in order of distance from the given number
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

        //Find tokens in the question and return the token dictionary
        static Dictionary<string,Pos_Conf> tokenFind(List<string> sentence)
        {
            int flag,minLength;
            List<string> token;
            int startPos = 0;
            Dictionary<string,Pos_Conf> predicates = new Dictionary<string,Pos_Conf>();
            while (sentence.Count != 0)
            {
                
                flag = 0; minLength = int.MaxValue;
                double maxConf = 0; string maxPred=""; int maxPos=0;
                foreach (KeyValuePair<string, string> pair in Tokens.tokenList)
                {
                    token = tokenize(pair.Key);
                    if (sentence.Count < token.Count)
                        continue;
                    double conf = listMatch(sentence, token, token.Count);
                    
                    if (conf > threshold)
                    {
                        if (maxConf < conf)
                        {
                            maxConf = conf;
                            maxPos = startPos;
                            maxPred = pair.Value;
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
                    if (predicates.ContainsKey(maxPred))
                    {
                        predicates[maxPred].update(maxConf, maxPos);
                    }
                    else
                    {
                        Pos_Conf temp = new Pos_Conf(maxConf, maxPos);
                        predicates.Add(maxPred, temp);
                    }
                    sentence.RemoveRange(0, minLength);
                    startPos += minLength;
                }
            }
            return predicates;
        }

        //Find all numbers in the question
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

        //Helper DFS function - Currently not used
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

        //Fit the tokens in a typesafe manner
        public static void typeSafe(List<ParseTree> unusedTokens, ParseTree tree, List<ParseTree> allTokens)
        {
            if (tree.confidence < 0.5) return;
            if (tree.holeList.Count == 0)
            {
                //No holes
                ParseTree newTree = (ParseTree)tree.Clone();

                //Reduce confidence by proportion of unused tokens
                tree.confidence *= (1 - (float)unusedTokens.Count / allTokens.Count);
                if (tree.confidence >= 0.5)
                {
                    tree.standardForm();
                    bool flag = false;
                    foreach (ParseTree t in completeTrees)
                        flag = flag || t.isEqual(tree);
                    if (!flag) completeTrees.Add(tree);
                }

                if (unusedTokens.Count != 0)
                {
                    //Adding conjunction as the root
                    ParseTree andToken = new ParseTree(new Node());
                    andToken.root.isHole = false;
                    andToken.root.data = "And";
                    andToken.root.outputType = Tokens.outputTypePredicates[andToken.root.data];
                    //Set output type
                    andToken.root.children = new List<Node>();

                    List<string> paramAnd = Tokens.inputTypePredicates[andToken.root.data];
                    if (paramAnd[0] != "null")
                    {
                        foreach (string x in paramAnd)
                        {
                            Node tempNode = new Node();     //check that it appends to end of list
                            andToken.root.children.Add(tempNode);
                            tempNode.outputType = x;
                        }
                    }
                    ParseTree newAndTree = (ParseTree)andToken.Clone();
                    newAndTree.holeList[0].holeFill(newTree);
                    newAndTree.confidence = 0.9 * newTree.confidence;
                    typeSafe(unusedTokens, (ParseTree)newAndTree.Clone(), allTokens);
                }
            }

            if (unusedTokens.Count() == 0 && tree.holeList.Count!=0)
            {
                //tokens finished but there are holes
                foreach (ParseTree tok in allTokens)
                {
                    if (tok.root.outputType == tree.holeList[0].outputType)
                    {
                        List<ParseTree> newTokens = new List<ParseTree>();
                        ParseTree newTree = (ParseTree)tree.Clone();
                        newTree.holeList[0].holeFill(tok);
                        newTree.confidence *= 0.9;
                        typeSafe(newTokens, (ParseTree)newTree.Clone(), allTokens);
                    }
                }
            }
            if(unusedTokens.Count() != 0 && tree.holeList.Count!=0)
            {
                //there are unused tokens and holes to be filled
                bool flag = false;
                int counter = -1;

                foreach (ParseTree tok in unusedTokens)
                {
                    counter++;
                    //Console.WriteLine("{0}\t{1}", Tokens.outputTypePredicates[tok], tree.holeList[0].outputType);
                    if (tok.root.outputType == tree.holeList[0].outputType)
                    {
                        flag = true;
                        List<ParseTree> newTokens = unusedTokens.Select(i => (ParseTree)i.Clone()).ToList();
                        
                        //newTokens.Remove(tok); //ERROR
                        newTokens.RemoveAt(counter);

                        ParseTree newTree = (ParseTree)tree.Clone();
                        newTree.holeList[0].holeFill(tok);
                        typeSafe(newTokens, (ParseTree)newTree.Clone(), allTokens);
                    }
                }
                if (flag == false)
                {
                    //none of the unused tokens can fill a hole - need to repeat some used token ?
                    foreach (ParseTree tok in allTokens)
                    {
                        if (tok.root.outputType == tree.holeList[0].outputType)
                        {
                            List<ParseTree> newTokens = unusedTokens.Select(i => (ParseTree)i.Clone()).ToList();
                            ParseTree newTree = (ParseTree)tree.Clone();
                            newTree.holeList[0].holeFill(tok);
                            newTree.confidence *= 0.9;
                            typeSafe(newTokens, (ParseTree)newTree.Clone(), allTokens);
                        }
                    }
                }
            }
        }

        //Helper function to display all tokens
        static void viewTokens(Dictionary<string, Pos_Conf> tokensConf)
        {
            Console.WriteLine("Displaying all tokens");
            foreach (KeyValuePair<string, Pos_Conf> temp in tokensConf)
            {
                for (int i = 0; i < temp.Value.conf.Count; i++)
                    Console.WriteLine(temp.Key + "\t" + temp.Value.conf[i] + "\t" + temp.Value.positions[i]);
            }
        }

        static void viewNumbers(Dictionary<int, List<string>> nums)
        {
            foreach (KeyValuePair<int, List<string>> numConfs in nums)
            {
                Console.Write("{0}\t", numConfs.Key);
                foreach (string pred in numConfs.Value)
                {
                    if (pred.IndexOf("NA") == -1)
                        Console.Write("{0} ", pred);
                }
                Console.WriteLine();
            }
        }

        //Prepare datastructures and find numbers with association with numeric predicates
        static Dictionary<int, List<string>> numberFind(Dictionary<string,Pos_Conf> tokensConf, List<string> numericPreds, List<string> splitWords)
        {
            List<string> predsOrder = new List<string>();
            List<int> predsPos = new List<int>();

            for (int i = 0; i < numericPreds.Count; i++)
            {
                if (tokensConf.ContainsKey(numericPreds[i]))
                {
                    for (int j = 0; j < tokensConf[numericPreds[i]].positions.Count; j++)
                    {
                        predsOrder.Add(numericPreds[i] + "#" + j);
                        predsPos.Add(tokensConf[numericPreds[i]].positions[j]);
                    }
                }
                else
                {
                    predsOrder.Add(numericPreds[i] + "#NA");
                    predsPos.Add(-1);
                }
            }
            return numFind(splitWords, predsPos, predsOrder);
        }

        //Add all non-numeric tokens to token list
        static void addSimpleTokens(Dictionary<string,Pos_Conf> tokensConf, List<string> numericPreds, List<ParseTree> tokenTrees)
        {
            foreach (KeyValuePair<string, Pos_Conf> tokenStructs in tokensConf)
            {
                if (numericPreds.Contains(tokenStructs.Key))
                    continue;
                for (int i = 0; i < tokenStructs.Value.positions.Count; i++)
                {
                    ParseTree temp = new ParseTree(new Node());
                    temp.root.isHole = false;
                    temp.root.data = tokenStructs.Key;
                    temp.root.outputType = Tokens.outputTypePredicates[tokenStructs.Key];
                    //Set output type
                    temp.root.children = new List<Node>();

                    List<string> param = Tokens.inputTypePredicates[tokenStructs.Key];
                    if (param[0] != "null")
                    {
                        foreach (string x in param)
                        {
                            Node tempNode = new Node();     //check that it appends to end of list
                            temp.root.children.Add(tempNode);
                            tempNode.outputType = x;
                        }
                    }
                    tokenTrees.Add((ParseTree)temp.Clone());
                }
            }
        }

        static void addCompoundTokens(Dictionary<int, List<string>> nums, Dictionary<string,Pos_Conf> tokensConf, List<ParseTree> tokenTrees)
        {
            //Add numeric tokens as Same(NumericPred(...),number)
            foreach (KeyValuePair<int, List<string>> numConfs in nums)
            {
                ParseTree temp = new ParseTree(new Node());
                temp.root.isHole = false;
                temp.root.data = "Same";
                temp.root.outputType = Tokens.outputTypePredicates["Same"];
                temp.root.children = new List<Node>();
                List<string> param = Tokens.inputTypePredicates["Same"];
                if (param[0] != "null")
                {
                    foreach (string x in param)
                    {
                        Node tempNode = new Node();     //check that it appends to end of list
                        temp.root.children.Add(tempNode);
                        tempNode.outputType = x;
                    }
                }
                //Assume that a number always has a numeric predicate associated with it!
                temp.root.children[0].data = numConfs.Value[0].Split('#')[0];
                temp.root.children[0].isHole = false;
                temp.root.children[0].children = new List<Node>();

                param = Tokens.inputTypePredicates[temp.root.children[0].data];
                if (param[0] != "null")
                {
                    foreach (string x in param)
                    {
                        Node tempNode = new Node();     //check that it appends to end of list
                        temp.root.children[0].children.Add(tempNode);
                        tempNode.outputType = x;
                    }
                }
                int result;
                int.TryParse(numConfs.Value[0].Split('#')[1], out result);
                tokensConf[numConfs.Value[0].Split('#')[0]].conf[result] = 0;

                temp.root.children[1].data = numConfs.Key.ToString();
                temp.root.children[1].isHole = false;
                tokenTrees.Add((ParseTree)temp.Clone());
            }
        }
        public static void Main(string[] args)
        {
            Tokens.initialize();
            Tokens.initializePredSpec();
            List<string> numericPreds = new List<string>(new string[] { "IE", "Group", "Period", "AtomicNumber", "OxidationState" });
            //string sentence = "Which element has the highest ionisation energy ?";
            //string sentence = "Which of the following elements has the smallest atomic radius";
            //string sentence = "Which element between group 3 and group 5 ";
            //string sentence = "Which element has the maximum affinity to electron ?";
            //string sentence = "Which element is in Group 2 and period 3";

            string sentence = "";
            foreach (string str in args)
                sentence += " " + str;
            sentence = sentence.ToLower();
            List<string> splitWords = tokenize(sentence);
            List<string> splitWordsNum = tokenize(sentence);
            Dictionary<string,Pos_Conf> tokensConf = tokenFind(splitWords);
            Dictionary<int, List<string>> nums = numberFind(tokensConf, numericPreds, splitWordsNum);
            
            List<ParseTree> tokenTrees = new List<ParseTree>();

            addSimpleTokens(tokensConf, numericPreds, tokenTrees);
            addCompoundTokens(nums, tokensConf, tokenTrees);

            
            //Add all the remaining numeric predicates to the token list
            foreach (string pred in numericPreds)
            {
                ParseTree temp = new ParseTree(new Node());
                if (tokensConf.ContainsKey(pred))
                {
                    for (int i = 0; i < tokensConf[pred].positions.Count; i++)
                    {
                        if (tokensConf[pred].conf[i] == 0)
                            continue;


                        temp.root.isHole = false;
                        temp.root.data = pred;
                        temp.root.outputType = Tokens.outputTypePredicates[pred];
                        //Set output type
                        temp.root.children = new List<Node>();

                        List<string> param = Tokens.inputTypePredicates[pred];
                        if (param[0] != "null")
                        {
                            foreach (string x in param)
                            {
                                Node tempNode = new Node();     //check that it appends to end of list
                                temp.root.children.Add(tempNode);
                                tempNode.outputType = x;
                            }
                        }
                        tokenTrees.Add((ParseTree)temp.Clone());
                        //Console.WriteLine(temp.ToString());
                    }
                }
                    
            }

            //Add "And" to allTokens   
            /*if (!tokensConf.ContainsKey("And"))
            {
                ParseTree andToken = new ParseTree(new Node());
                andToken.root.isHole = false;
                andToken.root.data = "And";
                andToken.root.outputType = Tokens.outputTypePredicates[andToken.root.data];
                //Set output type
                andToken.root.children = new List<Node>();

                List<string> paramAnd = Tokens.inputTypePredicates[andToken.root.data];
                if (paramAnd[0] != "null")
                {
                    foreach (string x in paramAnd)
                    {
                        Node tempNode = new Node();     //check that it appends to end of list
                        andToken.root.children.Add(tempNode);
                        tempNode.outputType = x;
                    }
                }
                tokenTrees.Add((ParseTree)andToken.Clone());
            }*/

            ParseTree tree = new ParseTree(new Node());
            try
            {
                
                typeSafe(tokenTrees, (ParseTree)tree.Clone(), tokenTrees);
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
