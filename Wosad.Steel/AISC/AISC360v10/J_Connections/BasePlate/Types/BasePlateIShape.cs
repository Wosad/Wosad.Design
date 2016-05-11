using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Steel.AISC.AISC360v10.Connections.BasePlate
{
    public class BasePlateIShape : BasePlateTypeBase
    {

        public BasePlateIShape(double B_bp, double N_bp, double d_c, double b_f, 
            double P_u, double phiP_p)
            :base(B_bp,N_bp)

        {

        }

        double d_c;
        double b_f;
        double P_u;
        double phiP_p;

        public override double GetLength()
        {
           double  m = ((N_bp - 0.95 * d_c) / (2.0));
           double  n = ((B_bp - 0.8 * b_f) / (2.0));
           double  lambda_n_prime = Get_lambda_n_prime();

           List<double> ls = new List<double>
           {
               m,n,lambda_n_prime
           };
           var l_max = ls.Max();

           return l_max;
        }

        private double Get_lambda_n_prime()
        {
            double X=(((4.0*d_c*b_f) / (Math.Pow((d_c+b_f), 2))))*((P_u) / (phiP_p));
            double lambda1 = ((2.0 * Math.Sqrt(X)) / (1 + Math.Sqrt(1 - X)));
            double lambda2 = 1.0;
            double lambda = Math.Min(lambda1, lambda2);
            double lambda_n_prime = lambda * ((Math.Sqrt(d_c * b_f)) / (4.0));
            return lambda_n_prime;
        }
    }
}
