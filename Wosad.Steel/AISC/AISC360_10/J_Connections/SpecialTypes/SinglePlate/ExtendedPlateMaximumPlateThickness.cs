using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.Entities;
using Wosad.Steel.AISC.AISC360_10.Connections.Bolted;
using Wosad.Steel.Tests.AISC.AISC360_10.Connections.Bolt;

namespace Wosad.Steel.AISC.AISC360_10.Connections
{
    public partial class ExtendedSinglePlate : AnalyticalElement
    {
        public double GetMaximumPlateThickness(double F_nv, double d_b, double C_prime, double F_yp, double d_pl, double t_w, double L_ehBm, double L_ehPl, double N_cols)
        {
            //the manual refers to F_v but table gives F_nv
            double M_max = GetM_max(F_nv,d_b,C_prime);
            double t_max1 = 6 * M_max / (F_yp * Math.Pow(d_pl, 2)); //AISC manual Equation 10-3
            double t_max, t_max2;

            t_max2 = 2 * d_b + 1 / 16.0;

            if (N_cols == 1)
            {
                if (L_ehBm>2*d_b && L_ehPl>2*d_b)
                {
                    if (t_w<=t_max2)
                    {
                        t_max = 8.0; //maximimum practical thickness of plate because the condition is not applicable
                        return t_max;
                    }
                }
            }
            else
            {
                if (L_ehBm > 2 * d_b && L_ehPl > 2 * d_b)
                {
                    if (t_w <= t_max2)
                    {
                        t_max = t_max2; //maximimum plate thickness such as both plate and web are less than t_max2
                    }
                }
                
            }
            return t_max1;
        }

        private double GetM_max(double F_nv, double d_b, double C_prime)
        {
            BoltGeneral b = new BoltGeneral(d_b, F_nv, 0);
            double A_b = b.Area;
            double M_max = F_nv/(0.9)*(A_b*C_prime); //AISC Manual Equation 10-4

            return M_max;
        }
    }
}
