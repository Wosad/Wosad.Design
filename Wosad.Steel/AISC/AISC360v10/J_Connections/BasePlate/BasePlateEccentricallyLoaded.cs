using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Steel.AISC.AISC360v10.Connections.BasePlate
{
    public class BasePlateEccentricallyLoaded:BasePlateConcentricallyLoaded
    {

        public BasePlateEccentricallyLoaded(IBasePlate Plate, double F_y, double f_anchor)
            :base(Plate, F_y)
        {
            this.f_anchor = f_anchor;
            N_bp = Plate.N_bp;
            B_bp = Plate.B_bp;

            CalculateBasePlateCommonParameters();
        }

        private void CalculateBasePlateCommonParameters()
        {
            throw new NotImplementedException();
        }

        public double f_anchor { get; set; }
        double N_bp;
        double B_bp;

        public bool DetermineIfBasePlateMeetsMinimumSizeLimits(double P_u, double M_u, double f_c, BendingAxis Axis, double A_2)
        {
            double phi_c = 0.65;
            double A_1 = Plate.B_bp * Plate.N_bp;

           
            double q_pMax = Get_q_pMax(f_c, Axis, A_1, A_2);
            double f_pMax = Get_f_pMax(f_c,A_1, A_2);
            double e = M_u/P_u;

            double Cr1 = Math.Pow((f_anchor+N_bp / 2.0), 2);
            double Cr2 =(2.0*P_u*(e+f_anchor)) / q_pMax;
             if (Cr1<Cr2)
	{
		 return false;
	}
            else
	{
                 return true;
	}
           
        }

        double Get_q_pMax(double f_c, BendingAxis Axis,double A_1, double A_2)
        {
            double f_pMax = Get_f_pMax(f_c,A_1, A_2);
            double q_pMax = 0.0;

            if (Axis == BendingAxis.Major)
            {
                q_pMax = f_pMax * B_bp;
            }
            else
            {
                q_pMax = f_pMax * N_bp;
            }

            return q_pMax;
        }

        double Get_f_pMax(double f_c, double A_1, double A_2)
        {
            double phi_c = 0.65;
            double f_pMax = phi_c*(0.85 * f_c) * Math.Sqrt(((A_2) / (A_1)));
            return f_pMax;
        }
    }
}
