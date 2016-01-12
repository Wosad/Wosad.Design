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
    public class WhitmoreSectionTests : ToleranceTestBase
    {
        public WhitmoreSectionTests()
        {
            tolerance = 0.02; //2% can differ from rounding in the manual
        }
        double tolerance;

        /// <summary>
        /// AISC Design Guide 29, Page 51
        /// </summary>
        [Test]
        public void AffectedElementReturnsWhitmoreSectionWidth()
        {
            AffectedElement el = new AffectedElement();

                double b_Whitmore = el.GetWhitmoreSectionWidth(18.0, 3.0);
                double refValue = 23.8;
                double actualTolerance = EvaluateActualTolerance(b_Whitmore, refValue);
                Assert.LessOrEqual(actualTolerance, tolerance);
            
        }
    }
}
