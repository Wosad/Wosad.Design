using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Steel.AISC.AISC360v10.Connections.BasePlate
{
    public class BasePlateTensionLoaded
    {
        public BasePlateTensionLoaded(IBasePlate Plate)
        {
            this.Plate = Plate;
        }
        public IBasePlate Plate { get; set; }


        public double GetMinimumBasePlateBasedOnBoltTension(double T_uAnchor, double x_anchor, double b_effPlate)
        {

            double t_p = 2.11 * Math.Sqrt(((T_uAnchor * x_anchor) / (b_effPlate * Plate.F_y)));
            return t_p;
        }
    }
}
