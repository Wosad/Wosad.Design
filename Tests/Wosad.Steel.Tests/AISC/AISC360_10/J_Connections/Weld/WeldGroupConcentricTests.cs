using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Steel.AISC;
using Wosad.Steel.AISC.AISC360_10.Connections;
using Wosad.Steel.AISC.AISC360_10.Connections.Weld;

namespace Wosad.Steel.Tests.AISC.AISC360_10.J_Connections.Weld
{
    [TestFixture]
    public class WeldGroupConcentricTests : ToleranceTestBase
    {

        public WeldGroupConcentricTests()
        {

            tolerance = 0.05; //5% can differ from number of sub-elements
        }

        double tolerance;


        //AISC Design Exaples V14
        //Example J.1 Fillet weld in longitudinal shear
        [Test]
        public void WeldConcentricParallelLinesReturnsValue()
        {
            FilletWeldGroup wg = new FilletWeldGroup(WeldGroupPattern.ParallelVertical, 5.0, 28.0, 3.0 / 16.0, 70.0);
            double phiR_n = wg.GetConcentricLoadStrenth(0);
            double refValue = 0.75 * 5.57 * 2 * 28.0;
            double actualTolerance = EvaluateActualTolerance(phiR_n, refValue);
            Assert.LessOrEqual(actualTolerance, tolerance);

        }

    }
}
