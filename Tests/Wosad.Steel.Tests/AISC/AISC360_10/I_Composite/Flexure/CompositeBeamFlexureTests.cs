using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.Entities;
using Wosad.Common.Section.Interfaces;
using Wosad.Common.Section.Predefined;
using Wosad.Common.Section.SectionTypes;
using Wosad.Steel.AISC.AISC360_10.Composite;

namespace Wosad.Steel.Tests.AISC.AISC360_10.Composite.Flexure
{
    [TestFixture]
    public partial class CompositeBeamTests: ToleranceTestBase
    {


        [Test]
        public void CompositeBeamReturnsFlexuralStrength()
        {
            double SumQ_n = 387;
            CompositeBeamSection cs = GetBeamForTests(SumQ_n);
            double phiM_n = cs.GetFlexuralStrength(SumQ_n);

            double refValue = 486; // from AISC Steel Manual
            double actualTolerance = EvaluateActualTolerance(phiM_n/12.0, refValue);

            Assert.LessOrEqual(actualTolerance, tolerance);
        }

    }
}
