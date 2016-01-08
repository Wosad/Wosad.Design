using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Analysis
{
    public class BeamFactoryData
    {
        public BeamFactoryData (double L, double P,double M,double w,
           double a_load, double b_load, double c_load, double P1, double P2, 
           double M1, double M2)
       {
                this.L      =L;
                this.P      =P;
                this.M      =M;
                this.w      =w;
                this.a_load =a_load;
                this.b_load =b_load;
                this.c_load =c_load;
                this.P1     =P1;
                this.P2     =P2;
                this.M1     =M1;
                this.M2     =M2;
       }
        public double L { get; set; }
        public double P { get; set; }
        public double M { get; set; }
        public double w { get; set; }
        public double a_load { get; set; }
        public double b_load { get; set; }
        public double c_load { get; set; }
        public double P1 { get; set; }
        public double P2 { get; set; }
        public double M1 { get; set; }
        public double M2 { get; set; }
            
    }
}
