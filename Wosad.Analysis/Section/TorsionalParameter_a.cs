using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Analysis.Section
{
    public class TorsionalParameters
    {
        public double Get_a(double C_w,double E,double G,double J)
        {
            double a = Math.Sqrt((E*C_w)/(G*J));
            //from AISC Design Guide 9 Equations 3.6 , 3.12, 3.23, 3.33
            return a;
        }
    }
}
