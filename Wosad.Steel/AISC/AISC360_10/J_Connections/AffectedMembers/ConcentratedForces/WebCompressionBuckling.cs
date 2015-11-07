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
        /// Web compression buckling strength
        /// </summary>
        /// <param name="t_w">Web thickness</param>
        /// <param name="h">clear distance between flanges less the fillet or corner radius for rolled shapes; distance between adjacent lines of fasteners or the clear distance between flanges when welds are used for built-up shapes,</param>
        /// <param name="d">Member depth</param>
        /// <param name="l_edge">Edge distance</param>
        /// <param name="F_yw">Web yield strength</param>
        /// <returns></returns>
        public static double WebCompressionBucklingStrength(double t_w, 
            double h, double d,  double l_edge, double F_yw)
        {
            double R_n = 0.0;
            double E = 29000;
            R_n = ((24 * Math.Pow(t_w, 3) * Math.Sqrt(E * F_yw)) / (h));
            if (l_edge<d/2)
            {
                R_n = R_n * 0.5; 
            }

            return 0.9 * R_n;
        }
    }
}
