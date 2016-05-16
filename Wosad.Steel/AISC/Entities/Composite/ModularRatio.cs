using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Steel.AISC.Entities.Composite
{
    public class ModularRatio
    {
        public double Get_n(double E_s, double E_c, bool IncludeDynamicIncrease)
        {
            if (IncludeDynamicIncrease == false)
            {
                return E_s / E_c;
            }
            else
            {
                return E_s / (1.35 * E_c);
            }
        }
    }
}
