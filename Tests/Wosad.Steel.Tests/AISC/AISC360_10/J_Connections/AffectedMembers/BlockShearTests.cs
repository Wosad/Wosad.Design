using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Steel.AISC360_10.Connections.AffectedElements;

namespace Wosad.Steel.Tests.AISC.AISC360_10.Connections.AffectedMembers
{
    [TestFixture]
    public class BlockShearTests : ToleranceTestBase
    {
        public BlockShearTests()
        {
            tolerance = 0.02; //2% can differ from rounding in the manual
        }

        double tolerance;

        /// <summary>
        /// AISC Design Guide 29
        /// Example 5.1 page 48
        /// </summary>
         [Test]
        public void BlockShearReturnsValue()
        {
            AffectedElement element = new AffectedElement(36.0, 58.0);
            double phiR_n = element.GetBlockShearStrength(39.0, 26.0, 7.0, true);
            double refValue = 938.0;
            double actualTolerance = EvaluateActualTolerance(phiR_n, refValue);
            Assert.LessOrEqual(actualTolerance, tolerance);
        }
    }
}