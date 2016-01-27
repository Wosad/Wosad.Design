using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Loads.ASCE7.ASCE7_10.Combinations
{
    public class CombinationResult
    {
        public LoadCombination MaxCombination { get; set; }
        public double MaxValue { get; set; }
        public LoadCombination MinCombination { get; set; }
        public double MinValue { get; set; }
        public double MaxAbsoluteValue { get; set; }
    }
}
