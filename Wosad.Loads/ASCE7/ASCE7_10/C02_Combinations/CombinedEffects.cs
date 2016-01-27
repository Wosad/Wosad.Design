using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Loads.ASCE7.ASCE7_10.Combinations
{
    public class CombinedEffects
    {
        public CombinationResult GetMaximumCombination(List<LoadCombination> Combinations)
        {
            double MaxVal = double.NegativeInfinity;
            double MinVal = double.PositiveInfinity;
            double MaxAbsVal = 0;
            CombinationResult ComboResult = new CombinationResult();
            foreach (var c in Combinations)
            {
                double val = c.D + c.L + c.L_r + c.S + c.E + c.F + c.H + c.R + c.T + c.W+c.F_a+c.W_i+c.D_i;
                if (val>=MaxVal)
                {
                    ComboResult.MaxValue = MaxVal;
                    ComboResult.MaxCombination = c;
                }
                if (val<=MinVal)
                {
                    ComboResult.MinValue = MaxVal;
                    ComboResult.MinCombination = c;
                }
                if (Math.Abs(val)>MaxAbsVal)
                {
                    ComboResult.MaxAbsoluteValue = Math.Abs(val);
                }
            }
            return ComboResult;
        }
    }
}
