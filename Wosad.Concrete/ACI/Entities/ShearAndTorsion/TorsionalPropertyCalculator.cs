using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Concrete.ACI
{
    public class TorsionalPropertyCalculator
    {
        public TorsionalPropertyCalculator(TorsionalReinforcementType TorsionalReinforcementType)
        {
            this.TorsionalReinforcementType = TorsionalReinforcementType;
        }
        public TorsionalReinforcementType TorsionalReinforcementType { get; set; }
    }
}
