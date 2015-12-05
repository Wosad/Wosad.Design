using Wosad.Concrete.ACI318_11.Anchorage.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Concrete.ACI318_11.Anchorage.LimitStates
{
    public class Bond : AnchorageConcreteLimitState
    {

        public Bond (int n, double h_eff, bool IsHookedBolt, double d_a, double e_h, double A_brg, AnchorInstallationType AnchorType,
            AnchorReliabilityAndSensitivityCategory AnchorCategory, ConcreteCrackingCondition ConcreteCondition
            )
            : base(n, h_eff, AnchorType)
            {
                //this.IsHookedBolt = IsHookedBolt;
                //this.d_a = d_a;
                //this.e_h = e_h;
                //this.A_brg = A_brg;
                //this.AnchorType = AnchorType;
                //this.AnchorCategory = AnchorCategory;
                //this.ConcreteCondition = ConcreteCondition;
            }


        public override double GetNominalStrength()
        {
            throw new NotImplementedException();

        }
    }
}
