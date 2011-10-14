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
        public static string[] inputTypeList = {"H#null","He#null","lithium#null","Be#null","B#null","C#null","N#null","O#null","F#null","Ne#null","Na#null","Mg#null",
        "Al#null","Si#null","P#null","S#null","Cl#null","K#null","Ar#null","Ca#null","Sc#null","V#null","Cr#null","mn#null","Fe#null",
        "Ni#null","Co#null","Cu#null","Zn#null","Ga#null","Ge#null","As#null","Se#null","Br#null","Kr#null","Rb#null","Sr#null","Y#null",
        "Zr#null","Nb#null","Mo#null","Tc#null","Ru#null","Rh#null","Pd#null","Ag#null","Cd#null","In#null","Sn#null","Sb#null","I#null",
        "Te#null","Xe#null","Cs#null","Ba#null","La#null","Ce#null","Pr#null","Nd#null","Pm#null","Sm#null","Eu#null","Gd#null","Tb#null",
        "Dy#null","Ho#null","Er#null","Tm#null","Yb#null","Lu#null","Hf#null","Ta#null","W#null","Re#null","Os#null","Ir#null","Pt#null",
        "Au#null","Hg#null","Tl#null","Pb#null","Bi#null","At#null","Po#null","Rn#null","Fr#null","Ra#null","Ac#null","Pa#null","Th#null",
        "Np#null","U#null","Am#null","Pu#null","Cm#null","Bk#null","Cf#null","Es#null","Fm#null","Md#null","No#null","Rf#null","Lr#null",
        "Db#null","Bh#null","Sg#null","Hs#null","Mt#null","Ds#null","Rg#null","Uut#null","Cn#null","Uup#null","Uuq#null","Uuh#null",
        "Uuo#null","Uus#null","Max#num","Min#num","IE#elem","Halogen#elem","GasAtSTP#elem","LiquidAtSTP#elem","Metalloid#elem",
        "NobleGas#elem","AlkaliMetal#elem","AlkalineEarthMetal#elem","RareEarthElement#elem","IonicBond#elem#elem","CovalentBond#elem#elem",
        "AtomicNumber#elem","OxidationState#elem","Electronegativity#elem","ElectronAffinity#elem","Conductance#elem","IonicRadius#elem",
        "AtomicRadius#elem","Group#elem","Period#elem","Metallic#elem","Same#num#num","And#bool#bool","Or#bool#bool","x#null","z#null"};

        public static void initializePredSpec()
        {
            inputTypePredicates = new Dictionary<string,List<string>>();
            outputTypePredicates = new Dictionary<string, string>();
            //Initialize input types of predicates
            
            //Initialize output types of predicates
            outputTypePredicates.Add("Max", "bool");
            outputTypePredicates.Add("Min", "bool");
            outputTypePredicates.Add("Same", "bool");

            outputTypePredicates.Add("Period", "num");
            outputTypePredicates.Add("Group", "num");
            outputTypePredicates.Add("AtomicNumber", "num");
            outputTypePredicates.Add("OxidationState", "num");
            outputTypePredicates.Add("IonicRadius", "num");
            outputTypePredicates.Add("AtomicRadius", "num");
            outputTypePredicates.Add("IE", "num");
            outputTypePredicates.Add("Metallic", "num");
            outputTypePredicates.Add("Electronegativity", "num");
            outputTypePredicates.Add("ElectronAffinity", "num");
            outputTypePredicates.Add("Conductance", "num");

            outputTypePredicates.Add("Halogen", "bool");
            outputTypePredicates.Add("GasAtSTP", "bool");
            outputTypePredicates.Add("LiquidAtSTP", "bool");
            outputTypePredicates.Add("Metalloid", "bool");
            outputTypePredicates.Add("NobleGas", "bool");
            outputTypePredicates.Add("AlkaliMetal", "bool");
            outputTypePredicates.Add("AlkalineEarthMetal", "bool");
            outputTypePredicates.Add("RareEarthElement", "bool");
            outputTypePredicates.Add("IonicBond", "bool");
            outputTypePredicates.Add("CovalentBond", "bool");
            outputTypePredicates.Add("And", "bool");
            outputTypePredicates.Add("Or", "bool");

            outputTypePredicates.Add("x", "elem");
            //outputTypePredicates.Add("y", "elem");
            outputTypePredicates.Add("z", "elem");
            outputTypePredicates.Add("h", "elem");
            outputTypePredicates.Add("hydrogen", "elem");
            outputTypePredicates.Add("he", "elem");
            outputTypePredicates.Add("helium", "elem");
            outputTypePredicates.Add("li", "elem");
            outputTypePredicates.Add("lithium", "elem");
            outputTypePredicates.Add("be", "elem");
            outputTypePredicates.Add("beryllium", "elem");
            outputTypePredicates.Add("b", "elem");
            outputTypePredicates.Add("boron", "elem");
            outputTypePredicates.Add("c", "elem");
            outputTypePredicates.Add("carbon", "elem");
            outputTypePredicates.Add("n", "elem");
            outputTypePredicates.Add("nitrogen", "elem");
            outputTypePredicates.Add("o", "elem");
            outputTypePredicates.Add("oxygen", "elem");
            outputTypePredicates.Add("f", "elem");
            outputTypePredicates.Add("fluorine", "elem");
            outputTypePredicates.Add("ne", "elem");
            outputTypePredicates.Add("neon", "elem");
            outputTypePredicates.Add("na", "elem");
            outputTypePredicates.Add("sodium", "elem");
            outputTypePredicates.Add("mg", "elem");
            outputTypePredicates.Add("magnesium", "elem");
            outputTypePredicates.Add("al", "elem");
            outputTypePredicates.Add("aluminium", "elem");
            outputTypePredicates.Add("si", "elem");
            outputTypePredicates.Add("silicon", "elem");
            outputTypePredicates.Add("p", "elem");
            outputTypePredicates.Add("phosphorus", "elem");
            outputTypePredicates.Add("s", "elem");
            outputTypePredicates.Add("sulfur", "elem");
            outputTypePredicates.Add("cl", "elem");
            outputTypePredicates.Add("chlorine", "elem");
            outputTypePredicates.Add("k", "elem");
            outputTypePredicates.Add("potassium", "elem");
            outputTypePredicates.Add("ar", "elem");
            outputTypePredicates.Add("argon", "elem");
            outputTypePredicates.Add("ca", "elem");
            outputTypePredicates.Add("sc", "elem");
            outputTypePredicates.Add("scandium", "elem");
            outputTypePredicates.Add("ti", "elem");
            outputTypePredicates.Add("titanium", "elem");
            outputTypePredicates.Add("v", "elem");
            outputTypePredicates.Add("vanadium", "elem");
            outputTypePredicates.Add("cr", "elem");
            outputTypePredicates.Add("chromium", "elem");
            outputTypePredicates.Add("mn", "elem");
            outputTypePredicates.Add("manganese", "elem");
            outputTypePredicates.Add("fe", "elem");
            outputTypePredicates.Add("iron", "elem");
            outputTypePredicates.Add("ni", "elem");
            outputTypePredicates.Add("nickel", "elem");
            outputTypePredicates.Add("co", "elem");
            outputTypePredicates.Add("cobalt", "elem");
            outputTypePredicates.Add("cu", "elem");
            outputTypePredicates.Add("copper", "elem");
            outputTypePredicates.Add("zn", "elem");
            outputTypePredicates.Add("zinc", "elem");
            outputTypePredicates.Add("ga", "elem");
            outputTypePredicates.Add("gallium", "elem");
            outputTypePredicates.Add("ge", "elem");
            outputTypePredicates.Add("germanium", "elem");
            outputTypePredicates.Add("as", "elem");
            outputTypePredicates.Add("arsenic", "elem");
            outputTypePredicates.Add("se", "elem");
            outputTypePredicates.Add("selenium", "elem");
            outputTypePredicates.Add("br", "elem");
            outputTypePredicates.Add("bromine", "elem");
            outputTypePredicates.Add("kr", "elem");
            outputTypePredicates.Add("krypton", "elem");
            outputTypePredicates.Add("rb", "elem");
            outputTypePredicates.Add("rubidium", "elem");
            outputTypePredicates.Add("sr", "elem");
            outputTypePredicates.Add("strontium", "elem");
            outputTypePredicates.Add("y", "elem");
            outputTypePredicates.Add("yttrium", "elem");
            outputTypePredicates.Add("zr", "elem");
            outputTypePredicates.Add("zirconium", "elem");
            outputTypePredicates.Add("nb", "elem");
            outputTypePredicates.Add("niobium", "elem");
            outputTypePredicates.Add("mo", "elem");
            outputTypePredicates.Add("molybdenum", "elem");
            outputTypePredicates.Add("tc", "elem");
            outputTypePredicates.Add("technetium", "elem");
            outputTypePredicates.Add("ru", "elem");
            outputTypePredicates.Add("ruthenium", "elem");
            outputTypePredicates.Add("rh", "elem");
            outputTypePredicates.Add("rhodium", "elem");
            outputTypePredicates.Add("pd", "elem");
            outputTypePredicates.Add("palladium", "elem");
            outputTypePredicates.Add("ag", "elem");
            outputTypePredicates.Add("silver", "elem");
            outputTypePredicates.Add("cd", "elem");
            outputTypePredicates.Add("cadmium", "elem");
            outputTypePredicates.Add("in", "elem");
            outputTypePredicates.Add("indium", "elem");
            outputTypePredicates.Add("sn", "elem");
            outputTypePredicates.Add("tin", "elem");
            outputTypePredicates.Add("sb", "elem");
            outputTypePredicates.Add("antimony", "elem");
            outputTypePredicates.Add("i", "elem");
            outputTypePredicates.Add("iodine", "elem");
            outputTypePredicates.Add("te", "elem");
            outputTypePredicates.Add("tellurium", "elem");
            outputTypePredicates.Add("xe", "elem");
            outputTypePredicates.Add("xenon", "elem");
            outputTypePredicates.Add("cs", "elem");
            outputTypePredicates.Add("caesium", "elem");
            outputTypePredicates.Add("ba", "elem");
            outputTypePredicates.Add("barium", "elem");
            outputTypePredicates.Add("la", "elem");
            outputTypePredicates.Add("lanthanum", "elem");
            outputTypePredicates.Add("ce", "elem");
            outputTypePredicates.Add("cerium", "elem");
            outputTypePredicates.Add("pr", "elem");
            outputTypePredicates.Add("praseodymium", "elem");
            outputTypePredicates.Add("nd", "elem");
            outputTypePredicates.Add("neodymium", "elem");
            outputTypePredicates.Add("pm", "elem");
            outputTypePredicates.Add("promethium", "elem");
            outputTypePredicates.Add("sm", "elem");
            outputTypePredicates.Add("samarium", "elem");
            outputTypePredicates.Add("eu", "elem");
            outputTypePredicates.Add("europium", "elem");
            outputTypePredicates.Add("gd", "elem");
            outputTypePredicates.Add("gadolinium", "elem");
            outputTypePredicates.Add("tb", "elem");
            outputTypePredicates.Add("terbium", "elem");
            outputTypePredicates.Add("dy", "elem");
            outputTypePredicates.Add("dysprosium", "elem");
            outputTypePredicates.Add("ho", "elem");
            outputTypePredicates.Add("holmium", "elem");
            outputTypePredicates.Add("er", "elem");
            outputTypePredicates.Add("erbium", "elem");
            outputTypePredicates.Add("tm", "elem");
            outputTypePredicates.Add("thulium", "elem");
            outputTypePredicates.Add("yb", "elem");
            outputTypePredicates.Add("ytterbium", "elem");
            outputTypePredicates.Add("lu", "elem");
            outputTypePredicates.Add("lutetium", "elem");
            outputTypePredicates.Add("hf", "elem");
            outputTypePredicates.Add("hafnium", "elem");
            outputTypePredicates.Add("ta", "elem");
            outputTypePredicates.Add("tantalum", "elem");
            outputTypePredicates.Add("w", "elem");
            outputTypePredicates.Add("tungsten", "elem");
            outputTypePredicates.Add("re", "elem");
            outputTypePredicates.Add("rhenium", "elem");
            outputTypePredicates.Add("os", "elem");
            outputTypePredicates.Add("osmium", "elem");
            outputTypePredicates.Add("ir", "elem");
            outputTypePredicates.Add("iridium", "elem");
            outputTypePredicates.Add("pt", "elem");
            outputTypePredicates.Add("platinum", "elem");
            outputTypePredicates.Add("au", "elem");
            outputTypePredicates.Add("gold", "elem");
            outputTypePredicates.Add("hg", "elem");
            outputTypePredicates.Add("mercury", "elem");
            outputTypePredicates.Add("tl", "elem");
            outputTypePredicates.Add("thallium", "elem");
            outputTypePredicates.Add("pb", "elem");
            outputTypePredicates.Add("lead", "elem");
            outputTypePredicates.Add("bi", "elem");
            outputTypePredicates.Add("bismuth", "elem");
            outputTypePredicates.Add("at", "elem");
            outputTypePredicates.Add("astatine", "elem");
            outputTypePredicates.Add("po", "elem");
            outputTypePredicates.Add("polonium", "elem");
            outputTypePredicates.Add("rn", "elem");
            outputTypePredicates.Add("radon", "elem");
            outputTypePredicates.Add("fr", "elem");
            outputTypePredicates.Add("francium", "Fr");
            outputTypePredicates.Add("ra", "elem");
            outputTypePredicates.Add("radium", "elem");
            outputTypePredicates.Add("ac", "elem");
            outputTypePredicates.Add("actinium", "elem");
            outputTypePredicates.Add("pa", "elem");
            outputTypePredicates.Add("protactinium", "elem");
            outputTypePredicates.Add("th", "elem");
            outputTypePredicates.Add("thorium", "elem");
            outputTypePredicates.Add("np", "elem");
            outputTypePredicates.Add("neptunium", "elem");
            outputTypePredicates.Add("u", "elem");
            outputTypePredicates.Add("uranium", "elem");
            outputTypePredicates.Add("am", "elem");
            outputTypePredicates.Add("americium", "elem");
            outputTypePredicates.Add("pu", "elem");
            outputTypePredicates.Add("plutonium", "elem");
            outputTypePredicates.Add("cm", "elem");
            outputTypePredicates.Add("curium", "elem");
            outputTypePredicates.Add("bk", "elem");
            outputTypePredicates.Add("berkelium", "elem");
            outputTypePredicates.Add("cf", "elem");
            outputTypePredicates.Add("californium", "elem");
            outputTypePredicates.Add("es", "elem");
            outputTypePredicates.Add("einsteinium", "elem");
            outputTypePredicates.Add("fm", "elem");
            outputTypePredicates.Add("fermium", "elem");
            outputTypePredicates.Add("md", "elem");
            outputTypePredicates.Add("mendelevium", "elem");
            outputTypePredicates.Add("no", "elem");
            outputTypePredicates.Add("nobelium", "elem");
            outputTypePredicates.Add("rf", "elem");
            outputTypePredicates.Add("rutherfordium", "elem");
            outputTypePredicates.Add("lr", "elem");
            outputTypePredicates.Add("lawrencium", "elem");
            outputTypePredicates.Add("db", "elem");
            outputTypePredicates.Add("dubnium", "elem");
            outputTypePredicates.Add("bh", "elem");
            outputTypePredicates.Add("bohrium", "elem");
            outputTypePredicates.Add("sg", "elem");
            outputTypePredicates.Add("seaborgium", "elem");
            outputTypePredicates.Add("hs", "elem");
            outputTypePredicates.Add("hassium", "elem");
            outputTypePredicates.Add("mt", "Mt");
            outputTypePredicates.Add("meitnerium", "elem");
            outputTypePredicates.Add("ds", "Ds");
            outputTypePredicates.Add("darmstadtium", "elem");
            outputTypePredicates.Add("rg", "Rg");
            outputTypePredicates.Add("roentgenium", "elem");
            outputTypePredicates.Add("uut", "Uut");
            outputTypePredicates.Add("ununtrium", "elem");
            outputTypePredicates.Add("cn", "Cn");
            outputTypePredicates.Add("copernicium", "elem");
            outputTypePredicates.Add("uup", "Uup");
            outputTypePredicates.Add("ununpentium", "elem");
            outputTypePredicates.Add("uuq", "Uuq");
            outputTypePredicates.Add("ununquadium", "elem");
            outputTypePredicates.Add("uuh", "Uuh");
            outputTypePredicates.Add("ununhexium", "elem");
            outputTypePredicates.Add("uuo", "Uuo");
            outputTypePredicates.Add("ununoctium", "elem");
            outputTypePredicates.Add("uus", "Uus");
            outputTypePredicates.Add("ununseptium", "elem");
        }

        public static Dictionary<string, string> tokenList;
        public static string[] tList = {"h#H", "hydrogen#H", "he#He", "helium#He", "li#Li", "lithium#Li", "be#Be", "beryllium#Be", "b#B",
          "boron#B", "c#C", "carbon#C", "n#N", "nitrogen#N", "o#O", "oxygen#O", "f#F", "fluorine#F", "ne#Ne", "neon#Ne", "na#Na", "sodium#Na", "mg#Mg",
          "magnesium#Mg", "al#Al", "aluminium#Al", "si#Si", "silicon#Si", "p#P", "phosphorus#P", "s#S", "sulfur#S", "cl#Cl", "chlorine#Cl",
          "k#K", "potassium#K", "ar#Ar", "argon#Ar", "ca#Ca", "calcium#Ca", "sc#Sc", "scandium#Sc", "ti#Ti", "titanium#Ti", "v#V", "vanadium#V",
          "cr#Cr", "chromium#Cr", "mn#Mn", "manganese#Mn", "fe#Fe", "iron#Fe", "ni#Ni", "nickel#Ni", "co#Co", "cobalt#Co", "cu#Cu", "copper#Cu",
          "zn#Zn", "zinc#Zn", "ga#Ga", "gallium#Ga", "ge#Ge", "germanium#Ge", "as#As", "arsenic#As", "se#Se", "selenium#Se", "br#Br", "bromine#Br",
          "kr#Kr", "krypton#Kr", "rb#Rb", "rubidium#Rb", "sr#Sr", "strontium#Sr", "y#Y", "yttrium#Y", "zr#Zr", "zirconium#Zr", "nb#Nb", "niobium#Nb",
          "mo#Mo", "molybdenum#Mo", "tc#Tc", "technetium#Tc", "ru#Ru", "ruthenium#Ru", "rh#Rh", "rhodium#Rh", "pd#Pd", "palladium#Pd", "ag#Ag",
          "silver#Ag", "cd#Cd", "cadmium#Cd", "in#In", "indium#In", "sn#Sn", "tin#Sn", "sb#Sb", "antimony#Sb", "i#I", "iodine#I", "te#Te",
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
          "ionization energy#IE", "atomic number#AtomicNumber", "group#Group", "period#Period", "element#x"};

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

