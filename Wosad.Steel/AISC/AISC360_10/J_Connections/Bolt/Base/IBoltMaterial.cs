using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Steel.AISC.AISC360_10.Connections.Bolted
{
    public interface IBoltMaterial
    {
        double GetNominalShearStress(BoltThreadCase ThreadCase);
        double GetNominalTensileStress(BoltThreadCase ThreadCase);

        double GetNominalShearStress(string ThreadCase);
        double GetNominalTensileStress(string ThreadCase);

    }
}
