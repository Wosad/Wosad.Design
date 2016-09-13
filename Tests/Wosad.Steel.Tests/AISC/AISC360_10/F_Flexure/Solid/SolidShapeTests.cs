using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.Entities;
using Wosad.Common.Section.Interfaces;
using Wosad.Common.Section.Predefined;
using Wosad.Steel.AISC.AISC360v10.Flexure;
using Wosad.Steel.AISC.Interfaces;
using Wosad.Steel.AISC.Steel.Entities;
using Wosad.Steel.AISC.SteelEntities;
using Wosad.Steel.AISC.SteelEntities.Materials;

namespace Wosad.Steel.Tests.AISC.AISC360v10.Flexure
{

    [TestFixture]
    public class SolidShapeTests : ToleranceTestBase
    {
        public SolidShapeTests()
        {
            CreateBeam();
            tolerance = 0.02; //2% can differ from rounding in the manual
        }

        double tolerance;

        ISteelBeamFlexure beam { get; set; }
        private void CreateBeam()
        {
            FlexuralMemberFactory factory = new FlexuralMemberFactory();
            AiscShapeFactory AiscShapeFactory = new AiscShapeFactory();
            ISection section = AiscShapeFactory.GetShape("HSS8X6X.500", ShapeTypeSteel.RectangularHSS);
            SteelMaterial mat = new SteelMaterial(46.0, 29000);
            beam = factory.GetBeam(section, mat, null, MomentAxis.XAxis, FlexuralCompressionFiberPosition.Top);

        }
        [Test]
        public void RectangularHSSReturnsFlexuralYieldingStrength()
        {
            SteelLimitStateValue Y =
             beam.GetFlexuralYieldingStrength(FlexuralCompressionFiberPosition.Top);
            double phiM_n = Y.Value;
            double refValue = 30.5 * 46 * 0.9; // Z_x*F_y*0.9
            double actualTolerance = EvaluateActualTolerance(phiM_n, refValue);

            Assert.LessOrEqual(actualTolerance, tolerance);
        }
    }
}
