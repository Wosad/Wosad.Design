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
        /// Pure torsion stress in open cross section
        /// </summary>
        /// <param name="G">Shear modulus of elasticity</param>
        /// <param name="t_el">Thickness of element</param>
        /// <param name="theta_1der">First derivative of angle of rotation with respect to z</param>
        /// <returns></returns>
        public double GetPureTorsionStressOpenSection(double G, double t_el, double theta_1der)
        {
            double tau_t = G * t_el * theta_1der; //AISC Design Guide 9 Equation(4.1)
            return tau_t;
        }
    }
}
