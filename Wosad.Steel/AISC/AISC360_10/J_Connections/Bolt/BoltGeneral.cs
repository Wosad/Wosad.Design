using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using b = Wosad.Steel.AISC.AISC360_10.Connections.Bolted;

namespace Wosad.Steel.Tests.AISC.AISC360_10.Connections.Bolt
{
    public class BoltGeneral: b.Bolt
    {

        public BoltGeneral(double Diameter, double F_nt, double F_nv): base(Diameter,null)
        {
            this.F_nt = F_nt;
            this.F_nv = F_nv;
        }

        double F_nt;
        double F_nv;


        public override double NominalTensileStress { get { return F_nt; } }

        public override double NominalShearStress { get { return F_nv; } }

    }
}
