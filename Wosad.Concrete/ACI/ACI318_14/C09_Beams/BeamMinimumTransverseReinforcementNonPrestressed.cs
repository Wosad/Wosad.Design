using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Concrete.ACI;


namespace Wosad.Concrete.ACI318_14
{
    public  partial class ReinforcedConcreteBeamNonprestressed
    {
        public double GetMinimumTransverseReinforcement(IConcreteSection Section,
            List<RebarPoint> LongitudinalBars, ICalcLog log)
        {
            throw new NotImplementedException();
        }
    }
}
