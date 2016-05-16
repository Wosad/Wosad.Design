using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Steel.AISC.Entities.FloorVibrations
{
    public class FloorVibrationBeamSingle
    {

        public double GetFundamentalFrequency(double Delta_j)
        {
            double g = 386.0;
            double f_j=0.18*Math.Sqrt(((g) / (Delta_j)));
           
            return f_j;
        }
    }
}
