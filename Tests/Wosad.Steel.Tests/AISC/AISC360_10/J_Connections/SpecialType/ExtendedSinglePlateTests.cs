using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
