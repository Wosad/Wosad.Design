using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Steel.AISC.AISC360v10.Connections.BasePlate
{
    public class BasePlateCircularHss : BasePlateTypeBase
    {

        public BasePlateCircularHss(double B_bp, double N_bp, double D, double f_c, double F_y, double A_2)
            :base(B_bp,N_bp, f_c, F_y, A_2)
        {
            this.D = D;
        }

        public double D { get; set; }
        public override double GetLength()
        {
            return Get_m();
        }

        public override double Get_m()
        {
            double diag = Math.Sqrt(Math.Pow(N_bp, 2) + Math.Pow(B_bp, 2));
            double m = ((diag - 0.8 * D) / (2.0));
            return m;
        }

        public override double Get_n()
        {
            return Get_m();
        }
    }
}
