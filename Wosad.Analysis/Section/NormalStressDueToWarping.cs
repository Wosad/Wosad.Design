using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Analysis.Section
{
    public partial class SectionStressAnalysis
    {
        /// <summary>
        /// Normal stress due to warping in open cross section
        /// </summary>
        /// <param name="E">Modulus of elasticity</param>
        /// <param name="W_ns">Normalized warping function at point s</param>
        /// <param name="theta_2der">Second derivative of angle of rotation with respect to z</param>
        /// <returns></returns>
        public double NormalStressDueToWarpingOpenSection(double E, double W_ns, double theta_2der)
        {
            double sigma_w = E * W_ns * theta_2der; //AISC Design Guide 9 Equation(4.3a)
            return sigma_w;
        }
    }
}
