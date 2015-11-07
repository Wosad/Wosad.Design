using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Steel.AISC.AISC360_10.J_Connections.AffectedMembers.ConcentratedForces
{

    public static partial class FlangeOrWebWithConcentratedForces
    {
        /// <summary>
        /// Web Local Yielding strength
        /// </summary>
        /// <param name="d"> Depth of member</param>
        /// <param name="l_edge"> Edge distance</param>
        /// <param name="F_yw">Web yield strength</param>
        /// <param name="k">Distance from outer face of flange to the web toe of fillet </param>
        /// <param name="l_b">Length of bearing </param>
        /// <returns></returns>
        public static double GetWebLocalYieldingStrength(double d, double t_w, double l_edge, double F_yw, double k, double l_b )
        {
            double R_n=0.0;
            if (l_edge>=d)
            {
                R_n = F_yw * t_w * (5 * k + l_b); //(J10-2)
            }
            else
	        {
                R_n = F_yw * t_w * (2.5 * k + l_b); //(J10-3)
	        }
            double phiR_n = 1.0 * R_n;
            return phiR_n;
        }
    }
}
