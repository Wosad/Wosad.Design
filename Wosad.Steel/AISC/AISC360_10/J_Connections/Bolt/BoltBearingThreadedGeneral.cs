using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Steel.AISC.AISC360_10.Connections.Bolted;

namespace Wosad.Steel.AISC.AISC360_10.Connections.Bolted
{

    public class BoltBearingThreadedGeneral : BoltBearing
    {
        public BoltBearingThreadedGeneral(double Diameter, BoltThreadCase ThreadType, IBoltMaterial material,
            ICalcLog log=null)
            : base(Diameter, ThreadType, log)
        {
            this.material = material;
            nominalTensileStress = material.GetNominalTensileStress(ThreadType);
            nominalShearStress = material.GetNominalShearStress(ThreadType);

        }

        IBoltMaterial material;

        private double nominalTensileStress;

        public override double NominalTensileStress
        {
            get { return nominalTensileStress; }
        }

        private double nominalShearStress;

        public override double NominalShearStress
        {
            get { return nominalShearStress; }
        }

    }
}
