using Wosad.Concrete.ACI318_11.Anchorage.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Concrete.ACI318_11.Anchorage.LimitStates
{
    public class AnchorSteelShear: AnchorageSteelLimitState
    {


        public AnchorSteelShear(
            int n,
            double f_uta,
            double f_ya,
            double A_se_N,
            AnchorSteelElementFailureType SteelElementFailureType,
            AnchorInstallationType AnchorType, CastInAnchorageType CastInAnchorageType)
            : base(n, f_uta, f_ya, A_se_N, SteelElementFailureType, AnchorType)
        {

        }

        public override double GetNominalStrength()
        {
            //(17.4.1.2)
            List<double> stresses = new List<double>() { 125000, 1.9 * fya, futa };
            double N_sa = n * (A_se_N * stresses.Min() / 1000);
            return N_sa;
        }

        public double GetDesignStrength()
        {
            //17.3.3
            //cases (a) and (b)
            double phi = 1.0;
            if (SteelFailureType == AnchorSteelElementFailureType.Ductile)
            {
                phi = 0.65;
            }
            else
            {
                phi = 0.6;
            }

            return phi * GetNominalStrength();
        }


    }
}
