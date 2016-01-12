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
        /// Normal stress due to flexure
        /// </summary>
        /// <param name="M">Bending moment</param>
        /// <param name="y"> distance to fiber from neutral axis</param>
        /// <param name="I">Moment of inertia</param>
        public double GetNormalStressDueToBending(double M, double y, double I )
        {
            double sigma_b = M * y / I; //Mechanics of materials
            return sigma_b;
        }
    }
}
