using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Steel.AISC;
using Wosad.Steel.AISC.AISC360_10.Connections.Weld;

namespace Wosad.Steel.Tests.AISC.AISC360_10.Connections.Weld
{
    [TestFixture]
    public class WeldStrengthTests : ToleranceTestBase
    {

        public WeldStrengthTests()
        {

            tolerance = 0.05; //5% can differ from number of sub-elements
        }

        double tolerance;

        //AISC Design Exaples V14
        //Example J.2 Fillet weld in longitudinal shear
        [Test]
        public void WeldConcentricLoadAtAngleReturnsValue()
        {
            FilletWeld weld = new FilletWeld(50, 65, 70, 5.0 / 16.0, 2.0, 2.0); //L = 2 because Example uses 2 sided welds
            double phiF_nw = weld.GetStrength( WeldLoadType.WeldShear, 60.0, false);
            double refValue = 19.5;
            double actualTolerance = EvaluateActualTolerance(phiF_nw, refValue); 
            Assert.LessOrEqual(actualTolerance, tolerance);

        }
    }
}
