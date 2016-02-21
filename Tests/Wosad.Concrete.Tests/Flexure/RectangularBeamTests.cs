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
        public void GetSimpleBeamFlexuralCapacityTop()
        {
            ConcreteSectionFlexure beam = GetConcreteBeam(12, 12, 4000, new RebarInput(1, 1));
            SectionFlexuralAnalysisResult MResult = beam.GetNominalFlexuralCapacity(FlexuralCompressionFiberPosition.Top);
            double M_n = MResult.Moment;

            double refValue = 615883;
            double actualTolerance = EvaluateActualTolerance(M_n, refValue);

            Assert.LessOrEqual(actualTolerance, tolerance);

        }

         [Test]
        public void GetSimpleBeamFlexuralCapacityBottom()
        {
            ConcreteSectionFlexure beam = GetConcreteBeam(12, 12, 4000, new RebarInput(1, 11));
            SectionFlexuralAnalysisResult MResult = beam.GetNominalFlexuralCapacity(FlexuralCompressionFiberPosition.Bottom);
            double M_n = MResult.Moment;

            double refValue = 615883;
            double actualTolerance = EvaluateActualTolerance(M_n, refValue);

            Assert.LessOrEqual(actualTolerance, tolerance);
        }

        public void Get2LayerBeamFlexuralCapacity()
        {
            ConcreteSectionFlexure beam = GetConcreteBeam(12, 12, 4000, new RebarInput(1, 1), new RebarInput(1, 3));
            SectionFlexuralAnalysisResult MResult = beam.GetNominalFlexuralCapacity(FlexuralCompressionFiberPosition.Top);
            double phiMn = MResult.Moment;
            Assert.AreEqual(1023529, Math.Round(phiMn, 0));
        }

        public void Get3LayerBeamFlexuralCapacity()
        {
            ConcreteSectionFlexure beam = GetConcreteBeam(12, 12, 4000, new RebarInput(1, 1), new RebarInput(1, 3), new RebarInput(1, 7));
            SectionFlexuralAnalysisResult MResult = beam.GetNominalFlexuralCapacity(FlexuralCompressionFiberPosition.Top);
            double phiMn = MResult.Moment;
            Assert.AreEqual(1101327, Math.Round(phiMn, 0));
        }
    }
}
