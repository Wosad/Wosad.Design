using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.Section.Interfaces;
using Wosad.Steel.AISC.Interfaces;

namespace Wosad.Steel.AISC.AISC360_10.J_Connections
{
    public abstract class BeamCopeBase
    {
        public BeamCopeBase(double c, double d_c, ISliceableSection Section, ISteelMaterial Material)
        {
            this.c = c;
            this.d_c = d_c;
            this.Section = Section;
            this.Material = Material;
        }

        public double c { get; set; }
        public double  d_c { get; set; }
        public ISliceableSection Section { get; set; }
        public ISteelMaterial Material { get; set; }
    }
}
