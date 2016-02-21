using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.Section.Interfaces;


namespace Wosad.Concrete.ACI
{
    public interface IConcreteSectionWithLongitudinalRebar
    {
        IConcreteSection Section { get;  }
        List<RebarPoint> LongitudinalBars { get; }
        double Get_d(double c, double h, FlexuralCompressionFiberPosition copressionFiber);
    }
}
