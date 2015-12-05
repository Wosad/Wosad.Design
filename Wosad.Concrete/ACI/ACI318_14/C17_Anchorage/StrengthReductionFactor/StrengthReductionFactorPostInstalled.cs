using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Concrete.ACI318_11.Anchorage
{
    public class StrengthReductionFactorPostInstalled: StrengthReductionFactorBase
    {

        private static double GetStrengthReductionFactorForConcrete(
    SupplementaryReinforcmentCondition Condition,
    AnchorLoadType LoadType)
        {
            double phi = 0.0;

            if (Condition == SupplementaryReinforcmentCondition.A)
            {
                if (LoadType == AnchorLoadType.Tension)
                {
                    phi = 0.75;
                }
                else //LoadType.Shear
                {
                    phi = 0.75;
                }
            }
            else
            {
                if (LoadType == AnchorLoadType.Tension)
                {
                    phi = 0.7;
                }
                else //LoadType.Shear
                {
                    phi = 0.7;
                }
            }

            return phi;
        }
    }
}
