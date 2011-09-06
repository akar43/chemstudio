﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chemistry_Studio
{
    static class Tokens
    {
        public static Dictionary<string, string> tokenList = new Dictionary<string, string>();
        public static void initialize()
        {
            tokenList.Add("h", "H");
            tokenList.Add("hydrogen", "H");
            tokenList.Add("he", "He");
            tokenList.Add("helium", "He");
            tokenList.Add("li", "Li");
            tokenList.Add("lithium", "Li");
            tokenList.Add("be", "Be");
            tokenList.Add("beryllium", "Be");
            tokenList.Add("b", "B");
            tokenList.Add("boron", "B");
            tokenList.Add("c", "C");
            tokenList.Add("carbon", "C");
            tokenList.Add("n", "N");
            tokenList.Add("nitrogen", "N");
            tokenList.Add("o", "O");
            tokenList.Add("oxygen", "O");
            tokenList.Add("f", "F");
            tokenList.Add("fluorine", "F");
            tokenList.Add("ne", "Ne");
            tokenList.Add("neon", "Ne");
            tokenList.Add("na", "Na");
            tokenList.Add("sodium", "Na");
            tokenList.Add("mg", "Mg");
            tokenList.Add("magnesium", "Mg");
            tokenList.Add("al", "Al");
            tokenList.Add("aluminium", "Al");
            tokenList.Add("si", "Si");
            tokenList.Add("silicon", "Si");
            tokenList.Add("p", "P");
            tokenList.Add("phosphorus", "P");
            tokenList.Add("s", "S");
            tokenList.Add("sulfur", "S");
            tokenList.Add("cl", "Cl");
            tokenList.Add("chlorine", "Cl");
            tokenList.Add("k", "K");
            tokenList.Add("potassium", "K");
            tokenList.Add("ar", "Ar");
            tokenList.Add("argon", "Ar");
            tokenList.Add("ca", "Ca");
            tokenList.Add("calcium", "Ca");
            tokenList.Add("sc", "Sc");
            tokenList.Add("scandium", "Sc");
            tokenList.Add("ti", "Ti");
            tokenList.Add("titanium", "Ti");
            tokenList.Add("v", "V");
            tokenList.Add("vanadium", "V");
            tokenList.Add("cr", "Cr");
            tokenList.Add("chromium", "Cr");
            tokenList.Add("mn", "Mn");
            tokenList.Add("manganese", "Mn");
            tokenList.Add("fe", "Fe");
            tokenList.Add("iron", "Fe");
            tokenList.Add("ni", "Ni");
            tokenList.Add("nickel", "Ni");
            tokenList.Add("co", "Co");
            tokenList.Add("cobalt", "Co");
            tokenList.Add("cu", "Cu");
            tokenList.Add("copper", "Cu");
            tokenList.Add("zn", "Zn");
            tokenList.Add("zinc", "Zn");
            tokenList.Add("ga", "Ga");
            tokenList.Add("gallium", "Ga");
            tokenList.Add("ge", "Ge");
            tokenList.Add("germanium", "Ge");
            tokenList.Add("as", "As");
            tokenList.Add("arsenic", "As");
            tokenList.Add("se", "Se");
            tokenList.Add("selenium", "Se");
            tokenList.Add("br", "Br");
            tokenList.Add("bromine", "Br");
            tokenList.Add("kr", "Kr");
            tokenList.Add("krypton", "Kr");
            tokenList.Add("rb", "Rb");
            tokenList.Add("rubidium", "Rb");
            tokenList.Add("sr", "Sr");
            tokenList.Add("strontium", "Sr");
            tokenList.Add("y", "Y");
            tokenList.Add("yttrium", "Y");
            tokenList.Add("zr", "Zr");
            tokenList.Add("zirconium", "Zr");
            tokenList.Add("nb", "Nb");
            tokenList.Add("niobium", "Nb");
            tokenList.Add("mo", "Mo");
            tokenList.Add("molybdenum", "Mo");
            tokenList.Add("tc", "Tc");
            tokenList.Add("technetium", "Tc");
            tokenList.Add("ru", "Ru");
            tokenList.Add("ruthenium", "Ru");
            tokenList.Add("rh", "Rh");
            tokenList.Add("rhodium", "Rh");
            tokenList.Add("pd", "Pd");
            tokenList.Add("palladium", "Pd");
            tokenList.Add("ag", "Ag");
            tokenList.Add("silver", "Ag");
            tokenList.Add("cd", "Cd");
            tokenList.Add("cadmium", "Cd");
            tokenList.Add("in", "In");
            tokenList.Add("indium", "In");
            tokenList.Add("sn", "Sn");
            tokenList.Add("tin", "Sn");
            tokenList.Add("sb", "Sb");
            tokenList.Add("antimony", "Sb");
            tokenList.Add("i", "I");
            tokenList.Add("iodine", "I");
            tokenList.Add("te", "Te");
            tokenList.Add("tellurium", "Te");
            tokenList.Add("xe", "Xe");
            tokenList.Add("xenon", "Xe");
            tokenList.Add("cs", "Cs");
            tokenList.Add("caesium", "Cs");
            tokenList.Add("ba", "Ba");
            tokenList.Add("barium", "Ba");
            tokenList.Add("la", "La");
            tokenList.Add("lanthanum", "La");
            tokenList.Add("ce", "Ce");
            tokenList.Add("cerium", "Ce");
            tokenList.Add("pr", "Pr");
            tokenList.Add("praseodymium", "Pr");
            tokenList.Add("nd", "Nd");
            tokenList.Add("neodymium", "Nd");
            tokenList.Add("pm", "Pm");
            tokenList.Add("promethium", "Pm");
            tokenList.Add("sm", "Sm");
            tokenList.Add("samarium", "Sm");
            tokenList.Add("eu", "Eu");
            tokenList.Add("europium", "Eu");
            tokenList.Add("gd", "Gd");
            tokenList.Add("gadolinium", "Gd");
            tokenList.Add("tb", "Tb");
            tokenList.Add("terbium", "Tb");
            tokenList.Add("dy", "Dy");
            tokenList.Add("dysprosium", "Dy");
            tokenList.Add("ho", "Ho");
            tokenList.Add("holmium", "Ho");
            tokenList.Add("er", "Er");
            tokenList.Add("erbium", "Er");
            tokenList.Add("tm", "Tm");
            tokenList.Add("thulium", "Tm");
            tokenList.Add("yb", "Yb");
            tokenList.Add("ytterbium", "Yb");
            tokenList.Add("lu", "Lu");
            tokenList.Add("lutetium", "Lu");
            tokenList.Add("hf", "Hf");
            tokenList.Add("hafnium", "Hf");
            tokenList.Add("ta", "Ta");
            tokenList.Add("tantalum", "Ta");
            tokenList.Add("w", "W");
            tokenList.Add("tungsten", "W");
            tokenList.Add("re", "Re");
            tokenList.Add("rhenium", "Re");
            tokenList.Add("os", "Os");
            tokenList.Add("osmium", "Os");
            tokenList.Add("ir", "Ir");
            tokenList.Add("iridium", "Ir");
            tokenList.Add("pt", "Pt");
            tokenList.Add("platinum", "Pt");
            tokenList.Add("au", "Au");
            tokenList.Add("gold", "Au");
            tokenList.Add("hg", "Hg");
            tokenList.Add("mercury", "Hg");
            tokenList.Add("tl", "Tl");
            tokenList.Add("thallium", "Tl");
            tokenList.Add("pb", "Pb");
            tokenList.Add("lead", "Pb");
            tokenList.Add("bi", "Bi");
            tokenList.Add("bismuth", "Bi");
            tokenList.Add("at", "At");
            tokenList.Add("astatine", "At");
            tokenList.Add("po", "Po");
            tokenList.Add("polonium", "Po");
            tokenList.Add("rn", "Rn");
            tokenList.Add("radon", "Rn");
            tokenList.Add("fr", "Fr");
            tokenList.Add("francium", "Fr");
            tokenList.Add("ra", "Ra");
            tokenList.Add("radium", "Ra");
            tokenList.Add("ac", "Ac");
            tokenList.Add("actinium", "Ac");
            tokenList.Add("pa", "Pa");
            tokenList.Add("protactinium", "Pa");
            tokenList.Add("th", "Th");
            tokenList.Add("thorium", "Th");
            tokenList.Add("np", "Np");
            tokenList.Add("neptunium", "Np");
            tokenList.Add("u", "U");
            tokenList.Add("uranium", "U");
            tokenList.Add("am", "Am");
            tokenList.Add("americium", "Am");
            tokenList.Add("pu", "Pu");
            tokenList.Add("plutonium", "Pu");
            tokenList.Add("cm", "Cm");
            tokenList.Add("curium", "Cm");
            tokenList.Add("bk", "Bk");
            tokenList.Add("berkelium", "Bk");
            tokenList.Add("cf", "Cf");
            tokenList.Add("californium", "Cf");
            tokenList.Add("es", "Es");
            tokenList.Add("einsteinium", "Es");
            tokenList.Add("fm", "Fm");
            tokenList.Add("fermium", "Fm");
            tokenList.Add("md", "Md");
            tokenList.Add("mendelevium", "Md");
            tokenList.Add("no", "No");
            tokenList.Add("nobelium", "No");
            tokenList.Add("rf", "Rf");
            tokenList.Add("rutherfordium", "Rf");
            tokenList.Add("lr", "Lr");
            tokenList.Add("lawrencium", "Lr");
            tokenList.Add("db", "Db");
            tokenList.Add("dubnium", "Db");
            tokenList.Add("bh", "Bh");
            tokenList.Add("bohrium", "Bh");
            tokenList.Add("sg", "Sg");
            tokenList.Add("seaborgium", "Sg");
            tokenList.Add("hs", "Hs");
            tokenList.Add("hassium", "Hs");
            tokenList.Add("mt", "Mt");
            tokenList.Add("meitnerium", "Mt");
            tokenList.Add("ds", "Ds");
            tokenList.Add("darmstadtium", "Ds");
            tokenList.Add("rg", "Rg");
            tokenList.Add("roentgenium", "Rg");
            tokenList.Add("uut", "Uut");
            tokenList.Add("ununtrium", "Uut");
            tokenList.Add("cn", "Cn");
            tokenList.Add("copernicium", "Cn");
            tokenList.Add("uup", "Uup");
            tokenList.Add("ununpentium", "Uup");
            tokenList.Add("uuq", "Uuq");
            tokenList.Add("ununquadium", "Uuq");
            tokenList.Add("uuh", "Uuh");
            tokenList.Add("ununhexium", "Uuh");
            tokenList.Add("uuo", "Uuo");
            tokenList.Add("ununoctium", "Uuo");
            tokenList.Add("uus", "Uus");
            tokenList.Add("ununseptium", "Uus");
            tokenList.Add("alkali metal", "AlkaliMetal()");
            tokenList.Add("alkaline earth metal", "AlkalineEarthMetal()");
            tokenList.Add("transition element", "transition element");
            tokenList.Add("transition metal", "transition element");
            tokenList.Add("metalloid", "Metalloid()");
            tokenList.Add("halogen", "Halogen()");
            tokenList.Add("noble gas", "NobleGas()");
            tokenList.Add("inert gas", "NobleGas()");
            tokenList.Add("lanthanide", "RareEarthElement()");
            tokenList.Add("actinide", "RareEarthElement()");
            tokenList.Add("gas at stp", "GasAtSTP()");
            tokenList.Add("liquid at stp", "LiquidAtSTP()");
            tokenList.Add("ionic bond", "IonicBond()");
            tokenList.Add("covalent", "CovalentBond()");
            tokenList.Add("spin paired", "SpinPaired()");
            tokenList.Add("atomic radius", "AtomicRadius()");
            tokenList.Add("atomic size", "AtomicRadius()");
            tokenList.Add("ionic radius", "IonicRadius()");
            tokenList.Add("ionic size", "IonicRadius()");
            tokenList.Add("maximum", "Max()");
            tokenList.Add("greatest", "Max()");
            tokenList.Add("most", "Max()");
            tokenList.Add("highest", "Max()");
            tokenList.Add("strongest", "Max()");
            tokenList.Add("best", "Max()");
            tokenList.Add("smallest", "Min()");
            tokenList.Add("least", "Min()");
            tokenList.Add("minimum", "Min()");
            tokenList.Add("lowest", "Min()");
            tokenList.Add("weakest", "Min()");
            tokenList.Add("worst", "Min()");
            tokenList.Add("electronegativity", "Electronegativity()");
            tokenList.Add("metallic", "Metallic()");
            tokenList.Add("increase", "Increase()");
            tokenList.Add("decrease", "Decrease()");
            tokenList.Add("electron affinity", "ElectronAffinity()");
            tokenList.Add("conductor", "Conductance()");
            tokenList.Add("quantum number", "QuantumNumber()");
            tokenList.Add("color", "Color()");
            tokenList.Add("orbital", "Orbitals()");
            tokenList.Add("family", "Family()");
            tokenList.Add("oxidation state", "OxidationState()");
            tokenList.Add("oxidation number", "OxidationState()");
            tokenList.Add("ionization energy", "IE()");
            tokenList.Add("atomic number", "AtomicNumber()");
            tokenList.Add("group", "GroupNumber()");
            tokenList.Add("period", "PeriodNumber()");
        }
    }
}

