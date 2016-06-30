using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Wosad.Concrete.ACI318_14;
using Wosad.Concrete.ACI;
using Wosad.Common.Section.Interfaces;

namespace Wosad.Concrete.ACI318_14.Tests.Flexure
{
    [TestFixture]
    public partial class RectangularBeamTests: ToleranceTestBase
    {

        [Test]
        public void SimpleBeamFlexuralCapacityTopReturnsValue()
        {
            ConcreteSectionFlexure beam = GetConcreteBeam(12, 12, 4000, new RebarInput(1, 1));
            IStrainCompatibilityAnalysisResult MResult = beam.GetNominalFlexuralCapacity(FlexuralCompressionFiberPosition.Top);
            double M_n = MResult.Moment;

            double refValue = 615883;
            double actualTolerance = EvaluateActualTolerance(M_n, refValue);

            Assert.LessOrEqual(actualTolerance, tolerance);

        }

        [Test]
        public void SimpleBeamFlexuralCapacityTopReturnsRebarStrainValue()
        {
            ConcreteSectionFlexure beam = GetConcreteBeam(12, 12, 4000, new RebarInput(1, 1));
            IStrainCompatibilityAnalysisResult MResult = beam.GetNominalFlexuralCapacity(FlexuralCompressionFiberPosition.Top);
            double M_n = MResult.Moment;

            double refValue = 615883;
            double actualTolerance = EvaluateActualTolerance(M_n, refValue);

            Assert.LessOrEqual(actualTolerance, tolerance);

        }

         [Test]
        public void SimpleBeamFlexuralCapacityBottomReturnsValue()
        {
            ConcreteSectionFlexure beam = GetConcreteBeam(12, 12, 4000, new RebarInput(1, 11));
            IStrainCompatibilityAnalysisResult MResult = beam.GetNominalFlexuralCapacity(FlexuralCompressionFiberPosition.Bottom);
            double M_n = MResult.Moment;

            double refValue = 615883;
            double actualTolerance = EvaluateActualTolerance(M_n, refValue);

            Assert.LessOrEqual(actualTolerance, tolerance);
        }

         public void TwoLayerBeamFlexuralCapacityReturnsValue()
        {
            ConcreteSectionFlexure beam = GetConcreteBeam(12, 12, 4000, new RebarInput(1, 1), new RebarInput(1, 3));
            IStrainCompatibilityAnalysisResult MResult = beam.GetNominalFlexuralCapacity(FlexuralCompressionFiberPosition.Top);
            double phiMn = MResult.Moment;
            Assert.AreEqual(1023529, Math.Round(phiMn, 0));
        }

         public void ThreeLayerBeamFlexuralCapacityReturnsValue()
        {
            ConcreteSectionFlexure beam = GetConcreteBeam(12, 12, 4000, new RebarInput(1, 1), new RebarInput(1, 3), new RebarInput(1, 7));
            IStrainCompatibilityAnalysisResult MResult = beam.GetNominalFlexuralCapacity(FlexuralCompressionFiberPosition.Top);
            double phiMn = MResult.Moment;
            Assert.AreEqual(1101327, Math.Round(phiMn, 0));
        }
    }
}
