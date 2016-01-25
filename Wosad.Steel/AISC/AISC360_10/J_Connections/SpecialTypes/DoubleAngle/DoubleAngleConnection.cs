using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.Entities;

namespace Wosad.Steel.AISC.AISC360_10.Connections
{
    public class DoubleAngleConnection : AnalyticalElement
    {
        /// <summary>
        /// Weld strength for outstanding leg of back to back angles. Both legs included.
        /// </summary>
        /// <param name="phiR_nWeld">Total weld strength for a single vertical leg  </param>
        /// <param name="L_angle"> Vertical length (height) of angle </param>
        /// <param name="e">Width of leg, welded to support</param>
        /// <returns></returns>
        public double GetWeldStrengthForOutstandingLegsOfBacktoBackAngles(double phiR_nWeld, double L_angle, double e)
        {
            double phiR_n = 2 * (((phiR_nWeld) / (Math.Sqrt(1 + ((12.96 * e) / (Math.Pow(L_angle, 2))))))); //(AISCM p.10-11)
            return phiR_n;
        }
    }
}
