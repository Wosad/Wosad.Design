using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.Mathematics;

namespace Wosad.Steel.AISC.UFM
{
    public class UFMCase
    {
        public UFMCase(double d_b, double d_c, double theta)
        {
           this.d_b    =d_b  ;
           this.d_c    =d_c  ;
           this.theta = theta;
        }

       public  double d_b    {get; set;}
       public  double d_c    {get; set;}
       public  double theta { get; set; }


        public double tan_theta
        {
            get
            {
                double thetaRad = theta.ToRadians();
                return Math.Tan(thetaRad);
            }
        }

        public double e_b
        {
            get { 
                
                return d_b/2.0; 
            }
        }


        public double e_c
        {
            get
            {

                return d_c / 2.0;
            }
        }
        
        public double Get_r(double alpha, double beta)
        {
            double r=Math.Sqrt(Math.Pow((alpha+e_c), 2)+Math.Pow((beta+e_b), 2));
            return r;
        }
        public double Get_alpha(double beta)
        {
            double alpha = e_b * tan_theta - e_c + beta * tan_theta;
            return alpha;
        }

        public double Get_beta(double alpha)
        {
            double beta = ((alpha + e_c) / (tan_theta)) - e_b;
            return beta;
        }
    }
}
