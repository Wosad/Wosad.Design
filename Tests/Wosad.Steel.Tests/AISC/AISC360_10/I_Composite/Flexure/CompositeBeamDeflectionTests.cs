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

namespace Wosad.Steel.Tests.AISC.AISC360_10.I_Composite.Flexure
{
    [TestFixture]
    public class CompositeBeamDeflectionTests: ToleranceTestBase
    {
        public CompositeBeamDeflectionTests()
        {
            tolerance = 0.05; //5% can differ from rounding in the manual
        }

        double tolerance;

        [Test]
        public void CompositeBeamSectionReturnsLowerBoundMomentOfInertia()
        {
            double SumQ_n = 387;
            double Y_2 = 5;
            double f_cPrime = 4;
            double h_solid = 3;
            double b_eff;
            double h_rib = 3;
            b_eff = SumQ_n / ((h_rib + h_solid - Y_2) * 2 * 0.85 * f_cPrime); //Back calculate b_eff to get the round number from AISC manual
            double Y_2T =h_solid-( SumQ_n / (0.85 * f_cPrime * b_eff) / 2) + h_rib; //test
            
            AiscShapeFactory factory = new AiscShapeFactory();
            ISection section = factory.GetShape("W18X35",ShapeTypeSteel.IShapeRolled);
            PredefinedSectionI catI = section as PredefinedSectionI;
            SectionIRolled secI = new SectionIRolled("", catI.d, catI.b_fTop, catI.t_f, catI.t_w,catI.k);
            CompositeBeamSection cs = new CompositeBeamSection(secI, b_eff, h_solid, h_rib, 50.0, f_cPrime);
            double I_LB = cs.GetLowerBoundMomentOfInertia(SumQ_n);

            double refValue = 1360; // from AISC Steel Manual
            double actualTolerance = EvaluateActualTolerance(I_LB, refValue);

            Assert.LessOrEqual(actualTolerance, tolerance);
        }
    }
}
