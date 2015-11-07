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
        /// Web local crippling strength
        /// </summary>
        /// <param name="t_w">Web thickness</param>
        /// <param name="t_f">Flange thickness</param>
        /// <param name="d">Full nominal depth of the section</param>
        /// <param name="l_b">Bearing length</param>
        /// <param name="l_edge">Edge distance</param>
        /// <param name="F_yw">Web yield strength</param>
        /// <returns></returns>
        public static double WebLocalCripplingStrength(double t_w, double t_f, double d, double l_b, double l_edge,
            double F_yw)
        {
            double R_n = 0.0;
            double E = 29000;
            if (l_b >= d)
            {
                //(J10-4)
                R_n = 0.8 * Math.Pow(t_w, 2) * (1 + 3 * (((l_b) / (d))) * Math.Pow((((t_w) / (t_f))), 1.5)) * Math.Sqrt(((E * F_yw * t_f) / (t_w)));
            }
            else
            {
                if (((l_b) / (d))<=0.2)
                {
                    //(J10-5a)
                    R_n = 0.4 * Math.Pow(t_w, 2) * (1 + 3 * (((l_b) / (d))) * Math.Pow((((t_w) / (t_f))), 1.5)) * Math.Sqrt(((E * F_yw * t_f) / (t_w)));
                }
                else
                {
                    //(J10-5b)
                    R_n=0.4*Math.Pow(t_w, 2)*(1+(((4*l_b) / (d))-0.2)*Math.Pow((((t_w) / (t_f))), 15))*Math.Sqrt(((E*F_yw*t_f) / (t_w)));
                }
            }
            return 0.75 * R_n;
        }
    }
}
