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
        public double StabilizedExtendedSinglePlateDesignMoment(double R_u, double t_p, double t_w)
        {
                double M_tu = R_u * ((t_w + t_p) / (2)); //Manual equation 10-8a
                return M_tu;
        }

        public double StabilizedExtendedSinglePlateFlexuralStrength(double b_f,double F_yb, double L_bm,double R_u,double t_w,double d_pl,double t_p, double F_yp)
       {

        double phi_b=0.9;
        double phi_v=1.0;

        //Manual equation 10-7a
        double phiM_tu=(phi_v*(0.6*F_yp)-((R_u) / (d_pl*t_p)))*((d_pl*Math.Pow(t_p, 2)) / (2))+((2*Math.Pow(R_u, 2)*(t_w+t_p)*b_f) / ((phi_b*F_yb)*L_bm*Math.Pow(t_w, 2))) ;

        return phiM_tu;
        }
    }

}
