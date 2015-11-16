using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.Section.Interfaces;
using Wosad.Steel.AISC.Interfaces;

namespace Wosad.Steel.AISC.AISC360_10.J_Connections
{
    public class BeamCopeSingle: BeamCopeBase
    {
        public BeamCopeSingle(double c, double d_c, ISliceableSection Section, ISteelMaterial Material):
            base(c,d_c,Section,Material)
        {

        }
    }
}
