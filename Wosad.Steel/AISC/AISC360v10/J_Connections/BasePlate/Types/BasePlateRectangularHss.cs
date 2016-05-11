
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Steel.AISC.AISC360v10.Connections.BasePlate
{
    public class BasePlateRectangularHss : BasePlateTypeBase
    {
         public BasePlateRectangularHss(double B_bp, double N_bp, double B, double H)
         :base(B_bp,N_bp)
        {
            this.B = B;
            this.H = H;
        }
         public double H { get; set; }

         public double B { get; set; }


        public override double GetLength()
        {

   
            double m = ((N_bp - 0.95 * H) / (2.0));
            double n = ((B_bp - 0.95 * B) / (2.0));

            return Math.Max(m, n);
        }
    }
}
