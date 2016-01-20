using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Steel.AISC;
using Wosad.Steel.AISC.AISC360_10.Composite;

namespace Wosad.Steel.Tests.AISC.AISC360_10.Composite
{

    [TestFixture]
    public class HeadedAnchorTests: ToleranceTestBase
    {
        public HeadedAnchorTests()
        {
            tolerance = 0.05; //5% can differ from rounding in the manual
        }

        double tolerance;

        [Test]
        public void HeadedAnchorNoDeckReturnsValue()
        {
            HeadedAnchor a =new HeadedAnchor();
            double Q_n = a.GetNominalShearStrength(HeadedAnchorDeckCondition.NoDeck,HeadedAnchorWeldCase.WeldedDirectly,1,3,3,6,0.75,4,65,110);
            double refValue = 21.2;
            double actualTolerance = EvaluateActualTolerance(Q_n, refValue);

            Assert.LessOrEqual(actualTolerance, tolerance);
        }

        [Test]
        public void HeadedAnchorParalleDeckReturnsValue()
        {
            HeadedAnchor a = new HeadedAnchor();
            double Q_n = a.GetNominalShearStrength(HeadedAnchorDeckCondition.Parallel, HeadedAnchorWeldCase.WeldedThroughDeck, 1, 3, 3, 6, 0.75, 4, 65, 110);
            double refValue = 21.2;
            double actualTolerance = EvaluateActualTolerance(Q_n, refValue);

            Assert.LessOrEqual(actualTolerance, tolerance);
        }

        [Test]
        public void HeadedAnchorPerpendicularDeckReturnsValue()
        {
            HeadedAnchor a = new HeadedAnchor();
            double Q_n = a.GetNominalShearStrength(HeadedAnchorDeckCondition.Perpendicular, HeadedAnchorWeldCase.WeldedThroughDeck, 2, 3, 3, 6, 0.75, 4, 65, 110);
            double refValue = 18.3;
            double actualTolerance = EvaluateActualTolerance(Q_n, refValue);

            Assert.LessOrEqual(actualTolerance, tolerance);
        }
    }
}
