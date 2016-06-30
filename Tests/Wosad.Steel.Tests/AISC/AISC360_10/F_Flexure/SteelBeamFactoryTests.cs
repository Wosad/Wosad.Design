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
using Wosad.Steel.AISC.AISC360v10.Flexure;
using Wosad.Steel.AISC.Interfaces;
using Wosad.Steel.AISC.SteelEntities;
using Wosad.Steel.AISC.SteelEntities.Materials;

namespace Wosad.Steel.Tests.AISC.AISC360v10.Flexure
{

    [TestFixture]
    public partial class SteelBeamFactoryTests : ToleranceTestBase
    {
        public SteelBeamFactoryTests()
        {

            tolerance = 0.02; //2% can differ from rounding in the manual
        }
         double tolerance;

               
        [Test]
        public void DoublySymmetricIReturnsLateralTorsionalStrength () 
        {

            FlexuralMemberFactory factory = new FlexuralMemberFactory();
            AiscShapeFactory AiscShapeFactory = new AiscShapeFactory();
            ISection r = new  SectionI("", 12.2, 6.49, 0.38, 0.23);


            SteelMaterial mat = new SteelMaterial(50.0, 29000);
            ISteelBeamFlexure beam12 = factory.GetBeam(r, mat, null, MomentAxis.XAxis, FlexuralCompressionFiberPosition.Top);

            SteelLimitStateValue LTB =
            beam12.GetFlexuralLateralTorsionalBucklingStrength(1.0, 19 * 12, FlexuralCompressionFiberPosition.Top, Steel.AISC.FlexuralAndTorsionalBracingType.NoLateralBracing);
            double phiM_n = LTB.Value;
            double refValue = 60 * 12; // from AISC Steel Manual Table 3-10
            double actualTolerance = EvaluateActualTolerance(phiM_n, refValue);


            Assert.LessOrEqual(actualTolerance, tolerance);
        }
         //ISection r = new ds.SectionI("", d, b_f, t_f, t_w);
    }
}
