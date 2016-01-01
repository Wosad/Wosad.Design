using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Steel.AISC;
using Wosad.Steel.AISC.AISC360_10.Connections.Bolted;
using Wosad.Steel.AISC.SteelEntities.Bolts;

namespace Wosad.Steel.Tests.AISC.AISC360_10.Connections.Bolt
{
    [TestFixture]
    public class SlipCriticalBoltTests : ToleranceTestBase
    {

        public SlipCriticalBoltTests()
        {
            tolerance = 0.02; //2% can differ from rounding in the manual
        }

        double tolerance;
        /// <summary>
        /// Example J.4A Slip-Critical Connection With Short-Slotted Holes
        /// </summary>
        [Test]
        public void BoltSlipCritical_SSL_ReturnsSlipResistance()
        {
            BoltSlipCriticalGroupA bolt = new BoltSlipCriticalGroupA(0.75, BoltThreadCase.Included, BoltFayingSurfaceClass.ClassA,
            BoltHoleType.SSL_Perpendicular, BoltFillerCase.One, 2, null);
            double phiR_n = bolt.GetSlipResistance();
            double refValue = 19.0; // from Design Examples
            double actualTolerance = EvaluateActualTolerance(phiR_n, refValue);
            Assert.LessOrEqual(actualTolerance, tolerance);
        }

        /// <summary>
        /// Example J.4B Slip-Critical Connection With Long-Slotted Holes
        /// </summary>
        [Test]
        public void BoltSlipCritical_LSL_ReturnsSlipResistance()
        {
            BoltSlipCriticalGroupA bolt = new BoltSlipCriticalGroupA(0.75, BoltThreadCase.Included, BoltFayingSurfaceClass.ClassA,
            BoltHoleType.LSL_Parallel, BoltFillerCase.One, 2, null);
            double phiR_n = bolt.GetSlipResistance();
            double refValue = 13.3; // from Design Examples
            double actualTolerance = EvaluateActualTolerance(phiR_n, refValue);
            Assert.LessOrEqual(actualTolerance, tolerance);
        }

        /// <summary>
        /// Example J.5 Combined tension and shear in a slip-critical connection
        /// </summary>
        [Test]
        public void BoltSlipCriticalSTDReturnsReducedSlipResistance()
        {
            BoltSlipCriticalGroupA bolt = new BoltSlipCriticalGroupA(0.75, BoltThreadCase.Included, BoltFayingSurfaceClass.ClassA,
            BoltHoleType.STD, BoltFillerCase.One, 1, null);
            double T_u = 72.0/8.0;
            double phiR_n = bolt.GetReducedSlipResistance(T_u);
            double refValue = 54.4/8.0; // from Design Examples
            double actualTolerance = EvaluateActualTolerance(phiR_n, refValue);
            Assert.LessOrEqual(actualTolerance, tolerance);
        }
    }
}
