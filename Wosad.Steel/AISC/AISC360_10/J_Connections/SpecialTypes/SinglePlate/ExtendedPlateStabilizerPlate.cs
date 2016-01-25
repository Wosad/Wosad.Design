using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.Entities;

namespace Wosad.Steel.AISC.AISC360_10.Connections
{
    public partial class ExtendedSinglePlate : AnalyticalElement
    {
        public double GetShearStrengthWithoutStabilizerPlate(double d_pl, double t_p, double a_bolts, double F_y)
        {
            double R_n = 1500*Math.PI*(d_pl*Math.Pow(t_p,3))/Math.Pow(a_bolts,2); //Manual equation 10-6
            //per Thornton Fortney 1500 factor has units of ksi
            return 0.9 * R_n;
        }
    }
}
