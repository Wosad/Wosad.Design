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
    public partial class DoublySymmetricICompactTests : ToleranceTestBase
    {




        [Test]
        public void DoublySymmetricIReturnsFlexuralLateralTorsionalBucklingStrengthInelasticW12 ()
        {
            FlexuralMemberFactory factory = new FlexuralMemberFactory();
            AiscShapeFactory AiscShapeFactory = new AiscShapeFactory();
            ISection section = AiscShapeFactory.GetShape("W12X26", ShapeTypeSteel.IShapeRolled);
            SteelMaterial mat = new SteelMaterial(50.0, 29000);
            ISteelBeamFlexure beam12 = factory.GetBeam(section, mat, null, MomentAxis.XAxis, FlexuralCompressionFiberPosition.Top);

            SteelLimitStateValue LTB =
            beam12.GetFlexuralLateralTorsionalBucklingStrength(1.0, 19 * 12, FlexuralCompressionFiberPosition.Top, Steel.AISC.FlexuralAndTorsionalBracingType.NoLateralBracing);
            double phiM_n = LTB.Value;
            double refValue = 60*12; // from AISC Steel Manual Table 3-10
            double actualTolerance = EvaluateActualTolerance(phiM_n, refValue);

            Assert.LessOrEqual(actualTolerance, tolerance);
        }




    }
}
