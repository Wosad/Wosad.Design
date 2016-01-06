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
        /// Shear stress due to applied shear
        /// </summary>
        /// <param name="V">Internal shear force</param>
        /// <param name="Q">Statical moment for the point in question</param>
        /// <param name="I">Moment of inertia (I_x or I_y where applicable)</param>
        /// <returns></returns>
        public double GetShearStressDueToAppliedShear(double V, double Q, double I)
        {
            double tau_b = V * Q / I;
            return tau_b;
        }
    }
}
