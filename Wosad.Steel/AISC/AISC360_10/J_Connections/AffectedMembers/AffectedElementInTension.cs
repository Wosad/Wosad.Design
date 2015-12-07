using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Common.Section.Interfaces;
using Wosad.Steel.AISC.Interfaces;



namespace Wosad.Analytics.Steel.AISC360_10.Connections.AffectedElements
{
    public class AffectedElementInTension : AffectedElementBase
    {

        public AffectedElementInTension(ISteelSection Section,ICalcLog CalcLog)
            : base(Section, CalcLog)
        {

        }

        public AffectedElementInTension(ISection Section, ISteelMaterial Material, ICalcLog CalcLog)
            :base(Section,Material, CalcLog)
        {

        }

        public double GetNetArea(double A_g, double N_bolts, double d_h, double s, List<double> g, double t_p, bool IsTensionSplicePlate  )
        {
            if (IsTensionSplicePlate==false)
            {
               return A_g - N_bolts * d_h - g.Sum(gage => Math.Pow(s, 2.0) / (4.0 * gage)); 
            }
            else
            {
                return 0.85 * A_g;
            }
            
        }
    }
}
