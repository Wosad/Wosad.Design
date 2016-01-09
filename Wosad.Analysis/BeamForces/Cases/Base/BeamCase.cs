using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Analysis.BeamForces
{

    public class BeamCase
    {
        public BeamCase(ISingleLoadCaseBeam ForceCase, ISingleLoadCaseDeflectionBeam DeflectionCase)
        {
            this.ForceCase = ForceCase;
            this.DeflectionCase = DeflectionCase;
        }
        public ISingleLoadCaseBeam ForceCase { get; set; }
        public ISingleLoadCaseDeflectionBeam DeflectionCase { get; set; }
    }
}
