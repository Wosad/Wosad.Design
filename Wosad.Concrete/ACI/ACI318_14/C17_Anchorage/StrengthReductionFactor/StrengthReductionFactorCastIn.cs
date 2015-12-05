using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Concrete.ACI318_11.Anchorage
{
    public class StrengthReductionFactorCastIn: StrengthReductionFactorBase
    {

        public static double GetStrengthReductionFactorForConcrete(
            SupplementaryReinforcmentCondition Condition,
            AnchorReliabilityAndSensitivityCategory Category,
            AnchorLoadType LoadType)
        {
            double phi = 0.0;

            if (Condition == SupplementaryReinforcmentCondition.A)
            {
                if (LoadType == AnchorLoadType.Tension)
                {
                    switch (Category)
                    {
                        case AnchorReliabilityAndSensitivityCategory.Category1:
                            phi = 0.75;
                            break;
                        case AnchorReliabilityAndSensitivityCategory.Category2:
                            phi = 0.65;
                            break;
                        case AnchorReliabilityAndSensitivityCategory.Category3:
                            phi = 0.55;
                            break;
                        default:
                            phi = 0.55;
                            break;
                    }
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
                    switch (Category)
                    {
                        case AnchorReliabilityAndSensitivityCategory.Category1:
                            phi = 0.65;
                            break;
                        case AnchorReliabilityAndSensitivityCategory.Category2:
                            phi = 0.55;
                            break;
                        case AnchorReliabilityAndSensitivityCategory.Category3:
                            phi = 0.45;
                            break;
                        default:
                            phi = 0.45;
                            break;
                    }
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
