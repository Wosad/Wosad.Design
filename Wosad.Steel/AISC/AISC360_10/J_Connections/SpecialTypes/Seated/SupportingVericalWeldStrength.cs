using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.Entities;

namespace Wosad.Steel.AISC.AISC360_10.Connections
{
    public partial class SeatedConnection : AnalyticalElement
    {
        /// <summary>
        /// Weld strength for outstanding leg of back to back angles. Both legs included.
        /// </summary>
        /// <param name="phiR_nWeld">Total weld strength for a single vertical leg  </param>
        /// <param name="L_vSeat"> Vertical length (height) of seat </param>
        /// <param name="e1">Eccentricity from load to face of support</param>
        /// <returns>phiR_n - weld strength</returns>
        public double GetWeldStrengthForOutstandingLegsOfBacktoBackAngles(double phiR_nWeld, double L_vSeat, double e_1)
        {

            double phiR_n = 2 * ((phiR_nWeld) / (Math.Sqrt(1 + ((20.25 * e_1) / (Math.Pow(L_vSeat, 2))))));
            return phiR_n;
        }
    }
}
