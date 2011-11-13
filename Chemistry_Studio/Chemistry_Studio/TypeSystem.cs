using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chemistry_Studio
{
    public abstract class TypeSystem {}
    public abstract class NumericType : TypeSystem {}
    public abstract class BooleanType : TypeSystem {}
    public abstract class OtherType : TypeSystem {}

    //Numeric Types
    public abstract class FirstIonisationEnergy : NumericType {}
}
