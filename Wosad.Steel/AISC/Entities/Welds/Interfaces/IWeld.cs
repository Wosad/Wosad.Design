using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Steel.AISC.Entities.Welds.Interfaces
{
    public interface IWeld
    {
        double GetStrength(WeldLoadType LoadType, double theta, bool IgnoreBaseMetal);
        double GetWeldArea();
    }
}
