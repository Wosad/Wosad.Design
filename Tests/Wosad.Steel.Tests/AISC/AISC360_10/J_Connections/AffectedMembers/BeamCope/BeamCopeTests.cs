using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Steel.AISC.AISC360_10.Connections;

namespace Wosad.Steel.Tests.AISC.AISC360_10.J_Connections.AffectedMembers.BeamCope
{
    [TestFixture]
    public class BeamCopeTests: ToleranceTestBase
    {
        public BeamCopeTests()
        {
            tolerance = 0.05;
        }
        double tolerance;
        [Test]
        public void BeamCopeReturnsValue()
        {
            //AISC Live Webinar:  FUNDAMENTALS OF CONNECTION DESIGN.  Tom Murray
            //August 21,  2014
            //Part 3 page 25 (Handout)
            double d = 13.8;
            double t_w = 0.270;
            double b_f = 6.73;
            double t_f = 0.385;
            double d_cope = 3.0;
            double c = 8;
            double F_y = 50;
            double F_u = 65;

            BeamCopeFactory factory = new BeamCopeFactory();
            IBeamCope copedBeam = factory.GetCope(Steel.AISC.BeamCopeCase.CopedTopFlange, d, b_f, t_f, t_w, d_cope, c, F_y, F_u);
            double phiM_n = copedBeam.GetFlexuralStrength();

            double refValue = 377; // from Lecture slides
            double actualTolerance = EvaluateActualTolerance(phiM_n, refValue);

            Assert.LessOrEqual(actualTolerance, tolerance);
        }
    }
}
