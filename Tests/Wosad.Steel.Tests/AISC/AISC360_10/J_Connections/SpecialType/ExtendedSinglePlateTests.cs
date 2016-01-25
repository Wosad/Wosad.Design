using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Steel.AISC.AISC360_10.Connections;

namespace Wosad.Steel.Tests.AISC.AISC360_10.Connections.SpecialType
{
    [TestFixture]
    public class ExtendedSinglePlateTests: ToleranceTestBase
    {
        public ExtendedSinglePlateTests()
        {
            tolerance = 0.02; //2% can differ from rounding
        }

        double tolerance;

        public void ExtendedSinglePlateReturnsMaximumPlateThickness()
        {
            //double refValue = 5.79; // from AISC Steel Manual
            //double actualTolerance = EvaluateActualTolerance(C, refValue);

            //Assert.LessOrEqual(actualTolerance, tolerance);
        }

        //Thornton Fortney AISC EJ 2011. Example1.
        [Test]
        public void ExtendedPlateUnstiffenedReturnsBucklingStrength()
        {
            double d_pl = 24;
            double a = 9;
            double t_pl = 0.5;
            ExtendedSinglePlate sp = new ExtendedSinglePlate();
            double phiR_n = sp.GetShearStrengthWithoutStabilizerPlate(d_pl,t_pl,a,50.0);
            double refValue = 157;

            double actualTolerance = EvaluateActualTolerance(phiR_n, refValue);

            Assert.LessOrEqual(actualTolerance, tolerance);

        }
        //Thornton Fortney AISC EJ 2011. Example6.
        [Test]
        public void ExtendedPlateReturnsTorsionalStrength()
        {
            double  R = 14; 
            double  l = 9; 
            double  a = 12;
            double  tp = 0.375; 
            double tw = 0.20;
            double  L = 233;
            double  bf = 3.97;
            ExtendedSinglePlate sp = new ExtendedSinglePlate();
            double phiM_n = sp.StabilizedExtendedSinglePlateTorsionalStrength(bf,50.0,L,R,tw,l,tp,50.0);
            double refValue = 18.6;

            double actualTolerance = EvaluateActualTolerance(phiM_n, refValue);

            Assert.LessOrEqual(actualTolerance, tolerance);

        }
    }
}
