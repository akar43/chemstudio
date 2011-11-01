using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
namespace Chemistry_Studio
{
    //Structure to contain positions and confidences of tokens obtained during parse
    class Position_Confidence
    {
        public List<double> confidences;
        public List<int> positions;

        public Position_Confidence(double confidence, int position)
        {
            confidences=new List<double>();
            positions=new List<int>();
            confidences.Add(confidence);
            positions.Add(position);
        }

        public void add(double confidence, int position)
        {
            positions.Add(position);
            confidences.Add(confidence);
        }
    }

    class Question_Struct
    {
        string id;
        string question;
        List<string> options;
        
        public Question_Struct()
        {
            options=new List<string>();
        }
        public Question_Struct(string fileName)
        {
            options=new List<string>();
            XmlDocument xmlDoc = null;
            // Setting the XmlReaderSettings so as to ignore Comments from the Input XML file.
            XmlReaderSettings readerSettings = new XmlReaderSettings();
            readerSettings.IgnoreWhitespace = true;
            readerSettings.IgnoreComments = true;

            using (XmlReader reader = XmlReader.Create(fileName, readerSettings))
            {
                xmlDoc = new XmlDocument();
                xmlDoc.Load(reader);
            }
            XmlNode questionTag = xmlDoc.FirstChild; // Skipping the root node.
            this.id = questionTag.ChildNodes[0].InnerText;
            this.question = questionTag.ChildNodes[1].InnerText;
            
            for(int i=2;i<questionTag.ChildNodes.Count;i++)
            {
                options.Add(questionTag.ChildNodes[i].InnerText);
            }
        }

        public override string ToString()
        {
            string representation = this.id+") ";
            representation += this.question + "\n\n";
            for (int i = 0; i < options.Count; i++)
            {
                representation += (i+1).ToString() + ". " + options[i] + "\n";
            }
            return representation;
        }

        }

    
    class Program
    {
        static List<ParseTree> completeTrees=new List<ParseTree>();
        static Char[] delims = {' ',',',':','?','.','-'};
        static double tokenMatch_threshold = 0.75;
        static double tree_rejection_threshold = 0.5;
        
        //Predicate to remove all null strings detected during the parse
        private static bool isNullString(String s)
        {
            if (s.Length==0) return true;
            else return false;
        }
        
        //Split a sentence into tokens
        static List<string> tokenize(string sentence)
        {
            string[] tokens = sentence.Split(delims);
            List<string> wordList = new List<string>(tokens);
            wordList.RemoveAll(isNullString);
            return wordList;
        }
        
        //Compare two lists for numwords and returns LD
        static double listMatch(List<string> sentence, List<string> tokens, int numWords)
        {
            /*
            double conf = 1.0;
            for (int i=0;i<wordMatch;i++)
            {
                conf*=LD.Compute(tokens[i],sentence[i]);
                
            }
            return conf;
            */

            string temp1 = ""; string temp2="";
            for (int i = 0; i < numWords; i++)
            {
                temp1 = temp1 + " " + sentence[i];
                temp2 = temp2 + " " + tokens[i];
            }
            return LD.Compute(temp1, temp2);
        }


        //Gives the sorting permutation of a list
        public static List<int> sortingPermutation(List<int> input)
        {
            List<int> output = new List<int>();
            for (int i = 0; i < input.Count; i++)
                output.Add(i);
            
            for(int i = 0; i<input.Count; i++)
                for(int j = i+1; j<input.Count; j++)
                    if (input[j] < input[i])
                    {
                        int temp = input[j];
                        input[i] = input[j];
                        input[j] = temp;

                        //swap positions in permutation list
                        temp = output[i];
                        output[i] = output[j];
                        output[j] = temp;
                    }
            return output;
        }

        //Returns numbers in the sentence along with their positions
        static void findNumbersAndPositions(List<string> tokenizedSentence, out List<string> numbersInQues, out List<int> positions)
        {
            numbersInQues = new List<string>();
	        positions=new List<int>();
            int temp;
            int counter = 0;
            for (int i = 0; i < tokenizedSentence.Count; i++)
            {
                if (int.TryParse(tokenizedSentence[i], out temp))
                {
                    counter++;
                    positions.Add(i);       //number concatenated with something to distinguish multiple occurances
                    numbersInQues.Add(temp.ToString()+"#"+counter.ToString());
                }
            }
            return;
        }

        //Returns the numeric predicates sorted in order of distance from the given number
        static List<int> closestPredicateToNumber(int numberPosition, List<int> predicatePositions)
        {
	        List<int> distances=new List<int>();
            for(int i=0;i<predicatePositions.Count;i++)
            {
                distances.Add(Math.Abs(numberPosition-predicatePositions[i]));
            }
            return sortingPermutation(distances);
        }

        //Find tokens in the question and return the token dictionary
        static Dictionary<string, Position_Confidence> findTokens(List<string> sentence)
        {
            int flag, minLength;
            List<string> tokens;
            int startPosition = 0;
            Dictionary<string,Position_Confidence> predicates = new Dictionary<string,Position_Confidence>();
            while (sentence.Count != 0)
            {
                
                flag = 0; minLength = int.MaxValue;
                double maxConfidence = 0;
                List<string> maxPredicate = new List<string>();
                int maxPosition=0;
                foreach (KeyValuePair<string, List<string>> pair in Tokens.tokenList)
                {
                    tokens = tokenize(pair.Key);
                    if (sentence.Count < tokens.Count)
                        continue;
                    double conf = listMatch(sentence, tokens, tokens.Count);
                    
                    if (conf > tokenMatch_threshold)
                    {
                        if (maxConfidence < conf)
                        {
                            maxConfidence = conf;
                            maxPosition = startPosition;
                            maxPredicate = pair.Value;
                        }

                        flag = 1;
                        if (minLength > tokens.Count)
                            minLength = tokens.Count;
                    }
                }
                if (flag == 0)
                {
                    sentence.RemoveAt(0);
                    startPosition += 1;
                }
                else
                {
                    foreach (string pred in maxPredicate)
                    {
                        if (predicates.ContainsKey(pred))
                        {
                            predicates[pred].add(maxConfidence, maxPosition);
                        }
                        else
                        {
                            Position_Confidence temp = new Position_Confidence(maxConfidence, maxPosition);
                            predicates.Add(pred, temp);
                        }
                    }
                    sentence.RemoveRange(0, minLength);
                    startPosition += minLength;
                }
            }
            return predicates;
        }

        /*
        //For each number in question, sort all numeric predicates in decreasing oreder of likelihood based on closeness with the number
        static Dictionary<int,List<string>> mostLikelyNumericPredicate(List<string> sentence, List<int> predicatePositions, List<string> predicates)
        {
            List<int> numInQuestion = new List<int>();
            List<int> numPositions;
            findNumbersAndPositions(sentence, numInQuestion, out numPositions);
            Dictionary<int, List<string>> output = new Dictionary<int, List<string>>();
            for (int i = 0; i < numPositions.Count; i++)
            {
                List<int> temp = closestPredicateToNumber(numPositions[i], predicatePositions);
                List<string> sortedOrderOfPredicates = new List<string>();
                for (int j = 0; j < temp.Count; j++)
                    sortedOrderOfPredicates.Add(predicates[temp[j]]);
                output.Add(numInQuestion[i], sortedOrderOfPredicates);
            }
            return output;
        }
         */

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

        static bool canFill(string type1, string targetType)
        {
            if (type1 == targetType) return true;
            //list of type hierarchy
            if (type1 == "elem" && targetType == "domain") return true;
            if (type1 == "bool" && targetType == "domain") return true;
            else return false;
        }

        public static bool checkVariableBranch(Node tree)
        {
            if(!(tree.data.Equals("And") || tree.data.Equals("Some")))
                return true;
            bool flag = true;
            if (tree.data.Equals("And"))
            {
                flag = true;
                foreach (Node temp in tree.children)
                    flag = flag && temp.isVariableInSubtree();
            }
            if(tree.data.Equals("Same"))
            {
                flag=false;
                foreach (Node temp in tree.children)
                    flag = flag || temp.isVariableInSubtree();
            }
            
            return flag;
        }
        //Fit the tokens in a typesafe manner
        public static void typeSafe(List<ParseTree> unusedTokens, ParseTree tree, List<ParseTree> allTokens)
        {
            int variableBranchSwitch=1;//MCQ - Change according to question
            if (tree.confidence < tree_rejection_threshold) return;
            if (tree.holeList.Count == 0)
            {
                //No holes
                ParseTree newTree = (ParseTree)tree.Clone();

                //Reduce confidence by proportion of unused tokens
                tree.confidence *= (1 - (float)unusedTokens.Count / allTokens.Count);
                if (tree.confidence >= tree_rejection_threshold)
                {
                    //tree.standardForm();
                    bool flag=true;
                    /*foreach (ParseTree t in completeTrees) //Do you need to check for equality here?-Abhishek
                        flag = flag || t.isEqual(tree);
                    if (!flag)*/ 
                    //Check if "same" has at least one variable branch and "And" has both variable branches in MCQs
                    if (variableBranchSwitch != 0)
                    {
                        flag = checkVariableBranch(tree.root);
                    }
                    if(flag) completeTrees.Add(tree);
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
                    //UPDATE FOR PERMUTATION CHECK
                    Node hole = newAndTree.holeList[0];
                    newAndTree.holeList[0].holeFill(newTree);
                    if (hole.checkForPermutation() == false) return; // this tree is going to generate permutation, discard it

                    newAndTree.confidence = 0.9 * newTree.confidence;
                    typeSafe(unusedTokens, (ParseTree)newAndTree.Clone(), allTokens);
                }
            }

            if (unusedTokens.Count() == 0 && tree.holeList.Count!=0)
            {
                //tokens finished but there are holes
                foreach (ParseTree tok in allTokens)
                {
                    if (canFill(tok.root.outputType, tree.holeList[0].outputType))
                    {
                        List<ParseTree> newTokens = new List<ParseTree>();
                        ParseTree newTree = (ParseTree)tree.Clone();

                        //UPDATE FOR PERMUTATION CHECK
                        Node hole = newTree.holeList[0];
                        newTree.holeList[0].holeFill(tok);
                        if (hole.checkForPermutation() == false) continue; // this tree is going to generate permutation, discard it

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
                    if (canFill(tok.root.outputType, tree.holeList[0].outputType))
                    {
                        flag = true;
                        List<ParseTree> newTokens = unusedTokens.Select(i => (ParseTree)i.Clone()).ToList();
                        
                        //newTokens.Remove(tok); //ERROR
                        newTokens.RemoveAt(counter);

                        ParseTree newTree = (ParseTree)tree.Clone();

                        //UPDATE FOR PERMUTATION CHECK
                        Node hole = newTree.holeList[0];
                        newTree.holeList[0].holeFill(tok);
                        if (hole.checkForPermutation() == false) continue; // this tree is going to generate permutation, discard it

                        typeSafe(newTokens, (ParseTree)newTree.Clone(), allTokens);
                    }
                }
                if (flag == false)
                {
                    //none of the unused tokens can fill a hole - need to repeat some used token ?
                    foreach (ParseTree tok in allTokens)
                    {
                        if (canFill(tok.root.outputType, tree.holeList[0].outputType))
                        {
                            List<ParseTree> newTokens = unusedTokens.Select(i => (ParseTree)i.Clone()).ToList();
                            ParseTree newTree = (ParseTree)tree.Clone();
                            
                            //UPDATE FOR PERMUTATION CHECK
                            Node hole = newTree.holeList[0];
                            newTree.holeList[0].holeFill(tok);
                            if (hole.checkForPermutation() == false) continue; // this tree is going to generate permutation, discard it

                            newTree.confidence *= 0.9;
                            typeSafe(newTokens, (ParseTree)newTree.Clone(), allTokens);
                        }
                    }
                }
            }
        }

        //Helper function to display all tokens
        static void viewTokens(Dictionary<string, Position_Confidence> tokens)
        {
            Console.WriteLine("Displaying all tokens : Token \t Confidence \t Position");
            foreach (KeyValuePair<string, Position_Confidence> temp in tokens)
            {
                for (int i = 0; i < temp.Value.confidences.Count; i++)
                    Console.WriteLine(temp.Key + "\t" + temp.Value.confidences[i] + "\t" + temp.Value.positions[i]);
            }
        }

        static void viewNumbers(Dictionary<int, List<string>> numbers)
        {
            foreach (KeyValuePair<int, List<string>> num in numbers)
            {
                Console.Write("{0}\t", num.Key);
                foreach (string temp in num.Value)
                {
                    if (temp.IndexOf("NA") == -1)
                        Console.Write("{0} ", temp);
                }
                Console.WriteLine();
            }
        }

        //For each number in question, sort all numeric predicates in decreasing oreder of likelihood based on closeness with the number
        static Dictionary<string, List<string>> mostLikelyNumericPredicate(Dictionary<string, Position_Confidence> tokenList,
            List<string> splitWords)
        {
            List<string> predicates = new List<string>();
            List<int> predicatePositions = new List<int>();

            for (int i = 0; i < Tokens.numericPredicates.Count; i++)
            {
                if (tokenList.ContainsKey(Tokens.numericPredicates[i]))
                {
                    for (int j = 0; j < tokenList[Tokens.numericPredicates[i]].positions.Count; j++)
                    {
                        predicates.Add(Tokens.numericPredicates[i] + "#" + j);
                        predicatePositions.Add(tokenList[Tokens.numericPredicates[i]].positions[j]);
                    }
                }
                else
                {
                    predicates.Add(Tokens.numericPredicates[i] + "#NA");
                    predicatePositions.Add(-1);
                }
            }

            List<string> numInQuestion;
            List<int> numPositions;
            findNumbersAndPositions(splitWords, out numInQuestion, out numPositions);
            Dictionary<string, List<string>> output = new Dictionary<string, List<string>>();
            for (int i = 0; i < numPositions.Count; i++)
            {
                List<int> temp = closestPredicateToNumber(numPositions[i], predicatePositions);
                List<string> sortedOrderOfPredicates = new List<string>();
                for (int j = 0; j < temp.Count; j++)
                    sortedOrderOfPredicates.Add(predicates[temp[j]]);
                output.Add(numInQuestion[i], sortedOrderOfPredicates);
            }
            return output;
        }

        //Add all non-numeric simpler tokens to tokentree list
        static void addSimpleTokens(Dictionary<string,Position_Confidence> tokenList, ref List<ParseTree> tokenTrees)
        {
            foreach (KeyValuePair<string, Position_Confidence> tokenStruct in tokenList)
            {
                /*
                // not adding numeric predicates right now
                if (Tokens.numericPredicates.Contains(tokenStruct.Key))
                    continue;
                 */
                for (int i = 0; i < tokenStruct.Value.positions.Count; i++)
                {
                    if (tokenStruct.Value.confidences[i] < tokenMatch_threshold) continue;

                    ParseTree temp = new ParseTree(new Node());
                    temp.root.isHole = false;
                    temp.root.data = tokenStruct.Key;
                    temp.root.outputType = Tokens.outputTypePredicates[tokenStruct.Key]; 
                    temp.root.children = new List<Node>();

                    List<string> param = Tokens.inputTypePredicates[tokenStruct.Key];
                    if (param[0] != "null")
                    {
                        foreach (string x in param)
                        {
                            Node tempNode = new Node(temp.root);
                            temp.root.children.Add(tempNode);
                            tempNode.outputType = x;
                        }
                    }
                    tokenTrees.Add((ParseTree)temp.Clone());  //cloning adjusts the holelist of the parsetree
                }
            }
        }

        static void handleNumbers(Dictionary<string, List<string>> numbersToPredictesMatchingList,
            Dictionary<string, Position_Confidence> tokenList, ref List<ParseTree> tokenTrees)
        {
            //Add numeric tokens as Same(NumericPred(...),number)
            foreach (KeyValuePair<string, List<string>> numToPred in numbersToPredictesMatchingList)
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
                        Node tempNode = new Node(temp.root);     //check that it appends to end of list
                        temp.root.children.Add(tempNode);
                        tempNode.outputType = x;
                    }
                }
                //Assume that a number always has a numeric predicate associated with it!
                //numer is put to left child for lexical ordering business in remooving permutations
                temp.root.children[1].data = numToPred.Value[0].Split('#')[0];
                temp.root.children[1].isHole = false;
                temp.root.children[1].children = new List<Node>();

                param = Tokens.inputTypePredicates[temp.root.children[1].data];
                if (param[0] != "null")
                {
                    foreach (string x in param)
                    {
                        Node tempNode = new Node(temp.root.children[1]);
                        temp.root.children[1].children.Add(tempNode);
                        tempNode.outputType = x;
                    }
                }

                int result;
                int.TryParse(numToPred.Value[0].Split('#')[1], out result);
                tokenList[numToPred.Value[0].Split('#')[0]].confidences[result] = 0;

                temp.root.children[0].data = numToPred.Key.Split('#')[0];
                temp.root.children[0].isHole = false;
                tokenTrees.Add((ParseTree)temp.Clone());
            }
        }

        static void addCoupledTokens(ref Dictionary<string, Position_Confidence> tokenList)
        {
            foreach (KeyValuePair<string, Position_Confidence> temp in tokenList)
            {
                //Ad hoc code for adding Trend predicate on finding a movement type token
                if (Tokens.outputTypePredicates[temp.Key] == "movement")
                {
                    tokenList.Add("Trend", new Position_Confidence(temp.Value.confidences[0], temp.Value.positions[0]));
                    return; //adding only one trend predicaate
                }
            }
            return;
        }

        public static void Main(string[] args)
        {
            Tokens.initialize();
            Tokens.initializePredSpec();
            string question_path = "C:\\Users\\Abhishek\\Documents\\Visual Studio 2010\\Projects\\Chemistry_Studio\\Chemistry_Studio\\Chemistry_Studio\\Questions\\";
            Question_Struct q1=new Question_Struct(question_path+"Q1.txt");
            Question_Struct q2 = new Question_Struct(question_path + "Q2.txt");
            Question_Struct q3 = new Question_Struct(question_path + "Q3.txt");
            Console.WriteLine(q1);
            Console.WriteLine(q2);
            Console.WriteLine(q3);
            string sentence = "";
            foreach (string str in args)
                sentence += " " + str;
            sentence = sentence.ToLower();
            
            List<string> splitWords = tokenize(sentence);
            List<string> splitWordsNumbers = tokenize(sentence);
            
            Dictionary<string,Position_Confidence> tokenList = findTokens(splitWords);
            addCoupledTokens(ref tokenList);
            Dictionary<string, List<string>> numbersToPredictesMatchingList = mostLikelyNumericPredicate(tokenList, splitWordsNumbers);
            
            List<ParseTree> tokenTrees = new List<ParseTree>();

            handleNumbers(numbersToPredictesMatchingList, tokenList, ref tokenTrees);
            addSimpleTokens(tokenList, ref tokenTrees);

            /*viewTokens(tokenList);
            foreach (ParseTree t in tokenTrees)
            {
                Console.WriteLine(t.ToString());
            }*/

            /*
            //Add all the remaining numeric predicates to the token list
            foreach (string pred in Tokens.numericPredicates)
            {
                ParseTree temp = new ParseTree(new Node());
                if (tokenList.ContainsKey(pred))
                {
                    for (int i = 0; i < tokenList[pred].positions.Count; i++)
                    {
                        if (tokenList[pred].confidences[i] == 0)
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
                                Node tempNode = new Node(temp.root);     //check that it appends to end of list
                                temp.root.children.Add(tempNode);
                                tempNode.outputType = x;
                            }
                        }
                        tokenTrees.Add((ParseTree)temp.Clone());
                    }
                }       
            }
            */

            //Add "And" to allTokens explicitly
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
                //Console.WriteLine(completeTrees[0].XMLForm());
            }
            catch(Exception e)
            {
                Console.WriteLine("Program Crashed! with message : " + e.ToString());
            }

            //Console.WriteLine(completeTrees[0].XMLForm());
            Console.ReadLine();
            
        }
    }
}
