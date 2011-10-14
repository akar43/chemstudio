using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chemistry_Studio
{
    public static class Tokens
    {
        public static Dictionary<string, List<string>> inputTypePredicates;
        public static Dictionary<string, string> outputTypePredicates;
        public static string[] outputTypeList = {
            "Max#bool", "Min#bool", "Same#bool", "Period#num", "Group#num", "AtomicNumber#num", "OxidationState#num", "IonicRadius#num",
            "AtomicRadius#num", "IE#num", "Metallic#num", "Electronegativity#num", "ElectronAffinity#num", "Conductance#num", "Halogen#bool",
            "GasAtSTP#bool", "LiquidAtSTP#bool", "Metalloid#bool", "NobleGas#bool", "AlkaliMetal#bool", "AlkalineEarthMetal#bool",
            "RareEarthElement#bool", "IonicBond#bool", "CovalentBond#bool", "And#bool", "Or#bool", "x#elem", "z#elem", "H#elem", "He#elem",
            "Li#elem", "Be#elem", "B#elem", "C#elem", "N#elem", "O#elem", "F#elem", "Ne#elem", "Na#elem", "Mg#elem", "Al#elem", "Si#elem",
            "P#elem", "S#elem", "Cl#elem", "K#elem", "Ar#elem", "Ca#elem", "Sc#elem", "Ti#elem", "V#elem", "Cr#elem", "Mn#elem", "Fe#elem",
            "Ni#elem", "Co#elem", "Cu#elem", "Zn#elem", "Ga#elem", "Ge#elem", "Se#elem", "Br#elem", "Kr#elem", "Rb#elem", "Sr#elem",
            "Y#elem", "Zr#elem", "Nb#elem", "Mo#elem", "Tc#elem", "Ru#elem", "Rh#elem", "Pd#elem", "Ag#elem", "Cd#elem", "Sn#elem",
            "Sb#elem", "I#elem", "Te#elem", "Xe#elem", "Cs#elem", "Ba#elem", "La#elem", "Ce#elem", "Pr#elem", "Nd#elem", "Pm#elem", "Sm#elem",
            "Eu#elem", "Gd#elem", "Tb#elem", "Dy#elem", "Ho#elem", "Er#elem", "Tm#elem", "Yb#elem", "Lu#elem", "Hf#elem", "Ta#elem", "W#elem",
            "Re#elem", "Os#elem", "Ir#elem", "Pt#elem", "Au#elem", "Hg#elem", "Tl#elem", "Pb#elem", "Bi#elem", "At#elem", "Po#elem", "Rn#elem",
            "Fr#elem", "Ra#elem", "Ac#elem", "Pa#elem", "Th#elem", "Np#elem", "U#elem", "Am#elem", "Pu#elem", "Cm#elem", "Bk#elem", "Cf#elem",
            "Es#elem", "Fm#elem", "Md#elem", "No#elem", "Rf#elem", "Lr#elem", "Db#elem", "Bh#elem", "Sg#elem", "Hs#elem", "Mt#elem", "Ds#elem",
            "Rg#elem", "Uut#elem", "Cn#elem", "Uup#elem", "Uuq#elem", "Uuh#elem", "Uuo#elem", "Uus#elem" };
        //Excluded , "As#elem" and , "In#elem"

        public static string[] inputTypeList = {"H#null","He#null","Li#null","Be#null","B#null","C#null","N#null","O#null","F#null","Ne#null","Na#null","Mg#null",
        "Al#null","Si#null","P#null","S#null","Cl#null","K#null","Ar#null","Ca#null","Sc#null","V#null","Cr#null","Mn#null","Fe#null",
        "Ni#null","Co#null","Cu#null","Zn#null","Ga#null","Ge#null","Se#null","Br#null","Kr#null","Rb#null","Sr#null","Y#null",
        "Zr#null","Nb#null","Mo#null","Tc#null","Ru#null","Rh#null","Pd#null","Ag#null","Cd#null","Sn#null","Sb#null","I#null",
        "Te#null","Xe#null","Cs#null","Ba#null","La#null","Ce#null","Pr#null","Nd#null","Pm#null","Sm#null","Eu#null","Gd#null","Tb#null",
        "Dy#null","Ho#null","Er#null","Tm#null","Yb#null","Lu#null","Hf#null","Ta#null","W#null","Re#null","Os#null","Ir#null","Pt#null",
        "Au#null","Hg#null","Tl#null","Pb#null","Bi#null","At#null","Po#null","Rn#null","Fr#null","Ra#null","Ac#null","Pa#null","Th#null",
        "Np#null","U#null","Am#null","Pu#null","Cm#null","Bk#null","Cf#null","Es#null","Fm#null","Md#null","No#null","Rf#null","Lr#null",
        "Db#null","Bh#null","Sg#null","Hs#null","Mt#null","Ds#null","Rg#null","Uut#null","Cn#null","Uup#null","Uuq#null","Uuh#null",
        "Uuo#null","Uus#null","Max#num","Min#num","IE#elem","Halogen#elem","GasAtSTP#elem","LiquidAtSTP#elem","Metalloid#elem",
        "NobleGas#elem","AlkaliMetal#elem","AlkalineEarthMetal#elem","RareEarthElement#elem","IonicBond#elem#elem","CovalentBond#elem#elem",
        "AtomicNumber#elem","OxidationState#elem","Electronegativity#elem","ElectronAffinity#elem","Conductance#elem","IonicRadius#elem",
        "AtomicRadius#elem","Group#elem","Period#elem","Metallic#elem","Same#num#num","And#bool#bool","Or#bool#bool","x#null","z#null"};

        //Excluded ,"As#null" and ,"In#null"
        public static void initializePredSpec()
        {
            inputTypePredicates = new Dictionary<string,List<string>>();
            outputTypePredicates = new Dictionary<string, string>();

            foreach (string str in inputTypeList)
            {
                string[] temp = str.Split('#');
                if (temp.Length == 2)
                    inputTypePredicates.Add(temp[0], new List<string>(new string[] { temp[1] }));
                if (temp.Length == 3)
                    inputTypePredicates.Add(temp[0], new List<string>(new string[] { temp[1], temp[2] }));
                if (temp.Length == 4)
                    inputTypePredicates.Add(temp[0], new List<string>(new string[] { temp[1], temp[2], temp[3] }));
            }   

            foreach (string str in outputTypeList) 
            {
                string[] temp = str.Split('#');
                outputTypePredicates.Add(temp[0], temp[1]);
            }   
        }

        public static Dictionary<string, string> tokenList;
        public static string[] tList = {"h#H", "hydrogen#H", "he#He", "helium#He", "li#Li", "lithium#Li", "be#Be", "beryllium#Be", "b#B",
          "boron#B", "c#C", "carbon#C", "n#N", "nitrogen#N", "o#O", "oxygen#O", "f#F", "fluorine#F", "ne#Ne", "neon#Ne", "na#Na", "sodium#Na", "mg#Mg",
          "magnesium#Mg", "al#Al", "aluminium#Al", "si#Si", "silicon#Si", "p#P", "phosphorus#P", "s#S", "sulfur#S", "cl#Cl", "chlorine#Cl",
          "k#K", "potassium#K", "ar#Ar", "argon#Ar", "ca#Ca", "calcium#Ca", "sc#Sc", "scandium#Sc", "ti#Ti", "titanium#Ti", "v#V", "vanadium#V",
          "cr#Cr", "chromium#Cr", "mn#Mn", "manganese#Mn", "fe#Fe", "iron#Fe", "ni#Ni", "nickel#Ni", "co#Co", "cobalt#Co", "cu#Cu", "copper#Cu",
          "zn#Zn", "zinc#Zn", "ga#Ga", "gallium#Ga", "ge#Ge", "germanium#Ge", "arsenic#As", "se#Se", "selenium#Se", "br#Br", "bromine#Br",
          "kr#Kr", "krypton#Kr", "rb#Rb", "rubidium#Rb", "sr#Sr", "strontium#Sr", "y#Y", "yttrium#Y", "zr#Zr", "zirconium#Zr", "nb#Nb", "niobium#Nb",
          "mo#Mo", "molybdenum#Mo", "tc#Tc", "technetium#Tc", "ru#Ru", "ruthenium#Ru", "rh#Rh", "rhodium#Rh", "pd#Pd", "palladium#Pd", "ag#Ag",
          "silver#Ag", "cd#Cd", "cadmium#Cd", "indium#In", "sn#Sn", "tin#Sn", "sb#Sb", "antimony#Sb", "i#I", "iodine#I", "te#Te",
          "tellurium#Te", "xe#Xe", "xenon#Xe", "cs#Cs", "caesium#Cs", "ba#Ba", "barium#Ba", "la#La", "lanthanum#La", "ce#Ce", "cerium#Ce", "pr#Pr",
          "praseodymium#Pr", "nd#Nd", "neodymium#Nd", "pm#Pm", "promethium#Pm", "sm#Sm", "samarium#Sm", "eu#Eu", "europium#Eu", "gd#Gd",
          "gadolinium#Gd", "tb#Tb", "terbium#Tb", "dy#Dy", "dysprosium#Dy", "ho#Ho", "holmium#Ho", "er#Er", "erbium#Er", "tm#Tm", "thulium#Tm",
          "yb#Yb", "ytterbium#Yb", "lu#Lu", "lutetium#Lu", "hf#Hf", "hafnium#Hf", "ta#Ta", "tantalum#Ta", "w#W", "tungsten#W", "re#Re", "rhenium#Re",
          "os#Os", "osmium#Os", "ir#Ir", "iridium#Ir", "pt#Pt", "platinum#Pt", "au#Au", "gold#Au", "hg#Hg", "mercury#Hg", "tl#Tl", "thallium#Tl",
          "pb#Pb", "lead#Pb", "bi#Bi", "bismuth#Bi", "at#At", "astatine#At", "po#Po", "polonium#Po", "rn#Rn", "radon#Rn", "fr#Fr", "francium#Fr",
          "ra#Ra", "radium#Ra", "ac#Ac", "actinium#Ac", "pa#Pa", "protactinium#Pa", "th#Th", "thorium#Th", "np#Np", "neptunium#Np", "u#U", "uranium#U",
          "am#Am", "americium#Am", "pu#Pu", "plutonium#Pu", "cm#Cm", "curium#Cm", "bk#Bk", "berkelium#Bk", "cf#Cf", "californium#Cf", "es#Es",
          "einsteinium#Es", "fm#Fm", "fermium#Fm", "md#Md", "mendelevium#Md", "no#No", "nobelium#No", "rf#Rf", "rutherfordium#Rf", "lr#Lr",
          "lawrencium#Lr", "db#Db", "dubnium#Db", "bh#Bh", "bohrium#Bh", "sg#Sg", "seaborgium#Sg", "hs#Hs", "hassium#Hs", "mt#Mt", "meitnerium#Mt",
          "ds#Ds", "darmstadtium#Ds", "rg#Rg", "roentgenium#Rg", "uut#Uut", "ununtrium#Uut", "cn#Cn", "copernicium#Cn", "uup#Uup", "ununpentium#Uup",
          "uuq#Uuq", "ununquadium#Uuq", "uuh#Uuh", "ununhexium#Uuh", "uuo#Uuo", "ununoctium#Uuo", "uus#Uus", "ununseptium#Uus",
          "alkali metal#AlkaliMetal", "alkaline earth metal#AlkalineEarthMetal", "transition element#transition element",
          "transition metal#transition element", "metalloid#Metalloid", "halogen#Halogen", "noble gas#NobleGas", "inert gas#NobleGas",
          "lanthanide#RareEarthElement", "actinide#RareEarthElement", "gas at stp#GasAtSTP", "liquid at stp#LiquidAtSTP", "ionic bond#IonicBond",
          "covalent#CovalentBond", "spin paired#SpinPaired", "atomic radius#AtomicRadius", "atomic size#AtomicRadius", "ionic radius#IonicRadius",
          "ionic size#IonicRadius", "maximum#Max", "greatest#Max", "most#Max", "highest#Max", "strongest#Max", "best#Max", "smallest#Min",
          "least#Min", "minimum#Min", "lowest#Min", "weakest#Min", "worst#Min", "electronegativity#Electronegativity", "metallic#Metallic",
          "increase#Increase", "decrease#Decrease", "electron affinity#ElectronAffinity", "conductor#Conductance", "quantum number#QuantumNumber",
          "color#Color", "orbital#Orbitals", "family#Family", "oxidation state#OxidationState", "oxidation number#OxidationState",
          "ionization energy#IE", "atomic number#AtomicNumber", "group#Group", "period#Period", "element#x", "same#Same", "and#And", "or#Or" };

        //Excluded , "in#In" and , "as#As" 
        public static void initialize()
        {
            tokenList = new Dictionary<string, string>();
            foreach (string str in tList)
            {
                string[] temp = str.Split('#');
                tokenList.Add(temp[0], temp[1]);
            }   
        }
    }
}

