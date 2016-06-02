using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Steel.AISC.AISC360v10.Connections.BasePlate
{
    public abstract class BasePlateTypeBase : IBasePlate
    {
        public BasePlateTypeBase(double B_bp, double N_bp, double f_c, double F_y, double A_2)
        {
            this.B_bp = B_bp;
            this.N_bp = N_bp;
            A_1 = this.B_bp * this.N_bp;
            if (A_2!=0)
            {
                this.A_2 = A_2; 
            }
            else
            {
                this.A_2 = A_1;
            }
            this.f_c = f_c;
            this.F_y = F_y;
        }

        public double GetphiP_p()
        {
            double phi = 0.65; //Bearing
            double P_p = 0.85 * f_c * A_1 * Math.Sqrt(((A_2) / (A_1)));
            double phiP_p = phi * P_p;
            return phiP_p;
        }
        public double f_c{get; set;}
        public double F_y { get; set; }
        public double A_1 { get; set; }


        private double _A_2;

        public double A_2
        {
            get { 
                
                return _A_2; }
            set { 
                _A_2 = value; }
        }
        
        public abstract double GetLength(double P_u);

        public double B_bp { get; set; }

        public double  N_bp { get; set; }

        public abstract double Get_m();
        public abstract double Get_n();

    }
}
