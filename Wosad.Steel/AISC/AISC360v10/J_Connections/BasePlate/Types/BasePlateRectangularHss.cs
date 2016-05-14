
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Steel.AISC.AISC360v10.Connections.BasePlate
{
    public class BasePlateRectangularHss : BasePlateTypeBase
    {
         public BasePlateRectangularHss(double B_bp, double N_bp, double B, double H,double f_c, double F_y, double A_2)
         :base(B_bp,N_bp,f_c,F_y, A_2)
        {
            this.B = B;
            this.H = H;
        }
         public double H { get; set; }

         public double B { get; set; }


        public override double GetLength()
        {

            double m = Get_m();
            double n = Get_n();

            return Math.Max(m, n);
        }

        public override double Get_m()
        {
            double m = ((N_bp - 0.95 * H) / (2.0));
            return m;
        }

        public  override double Get_n()
        {
            double n = ((B_bp - 0.95 * B) / (2.0));
            return n;
        }
    }
}
