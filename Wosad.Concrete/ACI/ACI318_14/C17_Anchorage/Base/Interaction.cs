using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Concrete.ACI318_11.Anchorage
{
    public partial class ConcreteAnchorageElement
    {

        public double GetDemandToCapacityRatio(double N_u, double V_u, double phi_Nn, double phi_V_n)
        {
            //Section 17.6—Interaction of tensile and shear forces
            double DCR;

            if (V_u/phi_V_n<=0.2)
            {
                //17.6.1
                DCR = N_u / phi_Nn;
            }
            else if (N_u/phi_Nn <=0.2)
            {
                //17.6.2
                DCR = V_u / phi_V_n;
            }
            else
            {
                //17.6.3
                DCR = (N_u / phi_Nn + V_u / phi_V_n) / 1.2;
            }

            return DCR;
        }

    }
}
