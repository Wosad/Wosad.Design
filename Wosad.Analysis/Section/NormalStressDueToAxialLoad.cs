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
        /// Axial stress
        /// </summary>
        /// <param name="P">Axial force</param>
        /// <param name="A">Cross-section area</param>
        /// <returns></returns>
        public double GetNormalStressDueToAxialLoad(double P, double A)
        {
            double sigma_a = P / A;
            return sigma_a;
        }
    }
}
