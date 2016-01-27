using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Steel.AISC;
using Wosad.Steel.AISC.AISC360_10.Shear;
using aisc = Wosad.Steel.AISC;

namespace Wosad.Steel.Tests.AISC.AISC360_10.Shear
{
    [TestFixture]
    public class ShearUnstiffenedBeamTests: ToleranceTestBase
    {
        public ShearUnstiffenedBeamTests()
        {
            tolerance = 0.02; //2% can differ from rounding in the manual
        }

        double tolerance;

        [Test]
        public void ShearBeamIShapeReturnsValue()
        {
                double h = 26.3;
                double t_w = 1.04;
                double a=0;
                double F_y = 50.0;
                double E = 29000;
                ShearMemberFactory factory = new ShearMemberFactory();
                IShearMember member = factory.GetShearMemberNonCircular(ShearNonCircularCase.MemberWithoutStiffeners, h, t_w, a, F_y,E);
                double phiV_n = member.GetShearStrength();

                double refValue = 821.0;
                double actualTolerance = EvaluateActualTolerance(phiV_n, refValue);

                Assert.LessOrEqual(actualTolerance, tolerance);

        }
    }
}
