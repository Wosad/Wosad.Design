using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Steel.AISC.AISC360_10.Connections;

namespace Wosad.Steel.Tests.AISC.AISC360_10.J_Connections.Weld
{

    [TestFixture]
    public class WeldGroupTests : ToleranceTestBase
    {
        public WeldGroupTests()
        {
            tolerance = 0.05; //5% can differ from number of sub-elements
        }

        double tolerance;


        [Test]
        public void WeldGroupChannelLinesReturnsValue()
        {
            double L =10;
            FilletWeldGroup wg = new FilletWeldGroup("C",5.0,L,1.0/16.0,70.0);
            double C = wg.GetInstantaneousCenterCoefficient(5.0, 0);
            double refValue = 2.85; // from AISC Steel Manual
            double P_n = refValue * L;
            double spreadsheetPn = 20.4 / 0.75; //Yakpol.net version 2008.1
            double actualTolerance = EvaluateActualTolerance(C, refValue);

            Assert.LessOrEqual(actualTolerance, tolerance);
        }

        [Test]
        public void WeldGroup2LinesLinesReturnsValue()
        {
            double L = 10;
            FilletWeldGroup wg = new FilletWeldGroup("ParallelVertical", 5.0, L, 1.0 / 16.0, 70.0);
            double C = wg.GetInstantaneousCenterCoefficient(5.0, 0);
            double refValue = 2.44; // from AISC Steel Manual
            double P_n = refValue * L;
            double spreadsheetPn = 18.34 / 0.75; //Yakpol.net version 2008.1
            double actualTolerance = EvaluateActualTolerance(C, refValue);

            Assert.LessOrEqual(actualTolerance, tolerance);
        }
    }
}
