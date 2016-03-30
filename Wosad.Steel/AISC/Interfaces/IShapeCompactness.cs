using System;
using Wosad.Steel.AISC.AISC360v10.General.Compactness;
namespace Wosad.Steel.AISC.AISC360v10.B_General
{
    public interface IShapeCompactness
    {
        double GetCompressionFlangeLambda();
        CompactnessClassAxialCompression GetFlangeCompactnessCompression();
        CompactnessClassFlexure GetFlangeCompactnessFlexure();
        double GetFlangeLambda_p(StressType stress);
        double GetFlangeLambda_r(StressType stress);
        CompactnessClassAxialCompression GetWebCompactnessCompression();
        CompactnessClassFlexure GetWebCompactnessFlexure();
        double GetWebLambda();
        double GetWebLambda_p(StressType stress);
        double GetWebLambda_r(StressType stress);
    }
}
