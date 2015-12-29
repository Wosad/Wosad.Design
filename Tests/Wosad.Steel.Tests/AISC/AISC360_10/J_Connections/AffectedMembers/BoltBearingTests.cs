using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Steel.AISC;
using Wosad.Steel.AISC.SteelEntities.Bolts;
using Wosad.Steel.AISC360_10.Connections.AffectedElements;

namespace Wosad.Steel.Tests.AISC.AISC360_10.Connections.AffectedMembers
{
    [TestFixture]
    public class BoltBearingTests : ToleranceTestBase
    {
        public BoltBearingTests()
        {
            tolerance = 0.02; //2% can differ from rounding in the manual
        }

        double tolerance;

        /// <summary>
        /// AISC Design Guide 29
        /// Example 5.1 page 50
        /// </summary>
         [Test]
        public void BoltBearingInnerBoltsReturnsValue()
        {
            AffectedElementWithHoles element = new AffectedElementWithHoles();
            double phiR_n = element.GetBearingStrengthAtBoltHole(2.06, 7.0 / 8.0, 1, 50.0, 65.0,BoltHoleType.Standard, BoltHoleDeformationType.ConsideredUnderServiceLoad, false);
            double refValue = 102.0;
            double actualTolerance = EvaluateActualTolerance(phiR_n, refValue);
            Assert.LessOrEqual(actualTolerance, tolerance);
        }

         [Test]
         public void BoltBearingEndBoltsReturnsValue()
         {
             AffectedElementWithHoles element = new AffectedElementWithHoles();
             double phiR_n = element.GetBearingStrengthAtBoltHole(1.03, 7.0 / 8.0, 1, 50.0, 65.0, BoltHoleType.Standard, BoltHoleDeformationType.ConsideredUnderServiceLoad, false);
             double refValue = 60.3;
             double actualTolerance = EvaluateActualTolerance(phiR_n, refValue);
             Assert.LessOrEqual(actualTolerance, tolerance);
         }
    }
}
