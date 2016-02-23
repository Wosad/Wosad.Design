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
           double f_yt, double s, ICalcLog log)
        {
            double f_c = Section.Material.SpecifiedCompressiveStrength;
            double b_w = Section.b_w;

            //Table 9.6.3.3—Required Av,min
            double A_vMin1 = 0.75 * Section.Material.Sqrt_f_c_prime * ((b_w) / (f_yt)) * s;
            double A_vMin2 = 50 * ((b_w) / (f_yt)) * s;
            return Math.Max(A_vMin1, A_vMin2);
        }
    }
}
