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
    public partial class AciFlexureRectangularBeamTests : ToleranceTestBase
    {
        /// <summary>
        /// PCA notes on ACI 318-11 Example 10.1
        /// </summary>
        [Test]
        public void CrackedMomentOfInertiaReturnsValue()
        {
            ConcreteSectionFlexure beam = GetConcreteBeam(12, 22, 3000, new RebarInput(1.8, 2.5), new RebarInput(0.6, 19.5));
            double Icr = beam.GetCrackedMomentOfInertia(FlexuralCompressionFiberPosition.Top);

            double refValue = 3770.0;
            double actualTolerance = EvaluateActualTolerance(Icr, refValue);

            Assert.LessOrEqual(actualTolerance, tolerance);
        }
    }
    

}
