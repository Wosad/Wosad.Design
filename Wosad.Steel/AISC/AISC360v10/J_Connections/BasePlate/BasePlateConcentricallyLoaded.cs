using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Steel.AISC.AISC360v10.Connections.BasePlate
{
    public class BasePlateConcentricallyLoaded : BasePlateTensionLoaded
    {


        public BasePlateConcentricallyLoaded(IBasePlate Plate)
            :base(Plate)
        {
            this.Plate = Plate;
        }
        public IBasePlate Plate { get; set; }


        public virtual double GetMinimumThicknessConcentricLoad(double P_u)
        {
            double F_y = Plate.F_y;
            double phi_b = 0.9;
            double B = Plate.B_bp;
            double N = Plate.N_bp;
            double l = Plate.GetLength(P_u);
            double t_minimum = l * Math.Sqrt(((2 * P_u) / (phi_b * F_y * B * N)));
            return t_minimum;
        }

        public double GetBearingStrength()
        {
            return Plate.GetphiP_p();
        }
    }
}
