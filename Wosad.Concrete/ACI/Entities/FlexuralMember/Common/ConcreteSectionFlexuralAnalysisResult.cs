using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Wosad.Concrete.ACI
{
    public class ConcreteSectionFlexuralAnalysisResult : SectionFlexuralAnalysisResult
    {
        public ConcreteSectionFlexuralAnalysisResult(double Moment, LinearStrainDistribution StrainDistribution, RebarPointResult controllingTensionBar): base(Moment,StrainDistribution)
        {
            this.ControllingTensionBar = ControllingTensionBar;
        }

        public RebarPointResult ControllingTensionBar { get; set; }
    }
}
