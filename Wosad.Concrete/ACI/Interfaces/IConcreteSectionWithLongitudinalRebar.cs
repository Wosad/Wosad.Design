using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Concrete.ACI.Infrastructure.Entities.Rebar;
using Wosad.Concrete.ACI.Infrastructure.Entities.Section.Strains;

namespace Wosad.Concrete.ACI
{
    public interface IConcreteSectionWithLongitudinalRebar
    {
        IConcreteSection Section { get;  }
        List<RebarPoint> LongitudinalBars { get; }
        double Get_d(LinearStrainDistribution strainDistribution);
    }
}
