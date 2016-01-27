using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Loads.ASCE7.ASCE7_10.Combinations
{
    public class LoadCombination
    {
        public double D { get; set; }
        public double L { get; set; }
        public double L_r { get; set; }
        public double S { get; set; }
        public double W { get; set; }
        public double E { get; set; }
        public double H { get; set; }
        public double F { get; set; }
        public double T { get; set; }
        public double R { get; set; }
        public double F_a { get; set; }
        public double W_i { get; set; }
        public double D_i { get; set; }
        public string Description { get; set; }
    }
}
