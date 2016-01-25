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
        public double TriangularSeatStiffenerPlateThicknessToPrecludeBuckling(double a_seat, double b_seat, double F_y, double E)
        {
            //From Salmon Johnson Malhas. 2009 edition. page 706
            double t_p;
            if ((b_seat / a_seat)>=0.5 && (b_seat / a_seat)<= 1.0)
            {
                t_p= b_seat/(1.47*Math.Sqrt(((E) / (F_y)))); //Equation 13.5.4a

            }
            else if ((b_seat / a_seat)>=1.0 && (b_seat / a_seat)<= 2.0)
            {
                t_p = b_seat / (1.47 * (((b_seat) / (a_seat))) * Math.Sqrt(((E) / (F_y)))); //Equation 13.5.4b
            }
            else
	        {
                throw new Exception("The ratio of seat triangular stiffener dimensions a and b are outside of the range. Select 0.5 < a/b <2");
	        }

            return t_p;
        }

    }
}
