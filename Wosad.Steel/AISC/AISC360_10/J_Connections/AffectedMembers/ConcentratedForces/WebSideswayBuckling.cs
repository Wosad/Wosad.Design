using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Steel.AISC.AISC360_10.J_Connections.AffectedMembers.ConcentratedForces
{
    // WebSideswayBuckling
    public partial class FlangeOrWebWithConcentratedForces
    {
        /// <summary>
        /// Flange Local Bending limit state
        /// </summary>
        /// <param name="F_y">Specified minimum yield stressof the flange (ksi)</param>
        /// <param name="t_f">thickness of the loaded flange (in.)</param>
        /// <returns></returns>
        public double GetFlangeOrWebWithConcentratedForcesStrength(double F_y, double t_f)
        {
            throw new NotImplementedException();
        }
    }
}
