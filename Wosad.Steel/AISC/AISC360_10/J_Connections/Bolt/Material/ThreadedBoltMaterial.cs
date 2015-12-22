using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Steel.AISC.AISC360_10.Connections.Bolted
{
    public class ThreadedBoltMaterial : IBoltMaterial
    {
        public double GetNominalShearStress(BoltThreadCase ThreadCase)
        {
            return 0.563 * F_u; //Table J3.2
        }

        public double GetNominalTensileStress(BoltThreadCase ThreadCase)
        {
            return 0.75 * F_u; //Table J3.2
        }

        double F_u;
        public ThreadedBoltMaterial (double F_u)
	    {
            this.F_u = F_u;
	    }



        public double GetNominalShearStress(string ThreadCase)
        {
            if (ThreadCase == "X")
            {
                return GetNominalShearStress(BoltThreadCase.Excluded);
            }
            else
            {
                return GetNominalShearStress(BoltThreadCase.Included);
            }
        }

        public double GetNominalTensileStress(string ThreadCase)
        {
            if (ThreadCase == "X")
            {
                return GetNominalTensileStress(BoltThreadCase.Excluded);
            }
            else
            {
                return GetNominalTensileStress(BoltThreadCase.Included);
            }
        }
    }
}
