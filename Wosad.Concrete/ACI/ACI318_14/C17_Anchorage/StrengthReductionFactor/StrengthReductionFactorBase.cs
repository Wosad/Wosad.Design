using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Concrete.ACI318_11.Anchorage
{
    public abstract class StrengthReductionFactorBase
    {


        public static double GetStrengthReductionFactorForSteel(
            AnchorSteelElementFailureType SteelFailureType,
            AnchorLoadType LoadType)
        {
            double phi = 0.0;
            //17.3.3
            if (SteelFailureType == AnchorSteelElementFailureType.Ductile)
            {
                if (LoadType == AnchorLoadType.Tension)
                {
                    phi = 0.75;
                }
                else //LoadType.Shear
                {
                    phi = 0.65;
                }
            }
            else //Brittle failure
            {
                if (LoadType == AnchorLoadType.Tension)
                {
                    phi = 0.65;
                }
                else //LoadType.Shear
                {
                    phi = 0.6;
                }

            }

            return phi;
        }

    }
}
