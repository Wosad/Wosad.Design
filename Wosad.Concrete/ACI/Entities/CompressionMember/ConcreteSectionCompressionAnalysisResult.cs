using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Wosad.Concrete.ACI
{
    
    public class ConcreteSectionCompressionAnalysisResult : ConcreteSectionFlexuralAnalysisResult
    {
        public ConcreteSectionCompressionAnalysisResult(double Moment, LinearStrainDistribution StrainDistribution, RebarPointResult controllingTensionBar)
            : base( Moment,  StrainDistribution,  controllingTensionBar)
        {

        }


    }
}
