using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Steel.AISC.AISC360v10.Connections.BasePlate
{
    public class BasePlateConcentricallyLoaded
    {
        public double GetphiP_p( double A_2,double f_c )
        {
            double A_1 = Plate.B_bp * Plate.N_bp;
            double phi = 0.65;
            double P_p = 0.85 * f_c * A_1 * Math.Sqrt(((A_2) / (A_1)));
            double phiP_p = phi * P_p;
            return phiP_p;
        }

        public BasePlateConcentricallyLoaded(IBasePlate Plate, double F_y)
        {
            this.Plate = Plate;
            this.F_y = F_y;
        }
        public IBasePlate Plate { get; set; }

        public double F_y { get; set; }

        public virtual double GetMinimumThicknessConcentricLoad(double P_u)
        {
            double phi_b = 0.9;
            double B = Plate.B_bp;
            double N = Plate.N_bp;
            double l = Plate.GetLength();
            double t_minimum = l * Math.Sqrt(((2 * P_u) / (phi_b * F_y * B * N)));
            return t_minimum;
        }
    }
}
