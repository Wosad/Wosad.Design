using System;
namespace Wosad.Concrete.ACI
{
    public interface IStrainCompatibilityAnalysisResult
    {
        double Moment { get; set; }
        LinearStrainDistribution StrainDistribution { get; set; }
        RebarPointResult ControllingTensionBar { get; set; }
    }
}
