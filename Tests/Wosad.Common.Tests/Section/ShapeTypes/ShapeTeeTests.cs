using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.Section.SectionTypes;

namespace Wosad.Common.Tests.Section.ShapeTypes
{
    [TestFixture]
    public class ShapeTeeTests: ToleranceTestBase
    {

        public ShapeTeeTests()
        {
            tolerance = 0.05; //5% can differ from fillet areas and rounding in the manual
        }

        double tolerance;


        double Z_x;
        double y_eTop;
        double y_PlTop;
        double t_w;
        double d;
        double b_f;
        double t_f;
        double k;
        double A;
        double I_x;
        
        private  void SetUpTests()
        {
                //WT8X13 
                Z_x = 7.36;
                y_eTop = 2.09;
                y_PlTop = 0.372;
                t_w = 0.25;
                d = 7.85;
                b_f = 5.5;
                t_f = 0.345;
                k = 0.747;
                A = 3.84;
                I_x = 23.5;
        }
        
        [Test]
        public void TeeReturnsZ_x()
        {
            SetUpTests();
            SectionTee tee = new SectionTee(null,d,b_f,t_f,t_w);
            double Z_xCalc = tee.Z_x;

            double refValue = Z_x; // from AISC Steel Manual
            double actualTolerance = EvaluateActualTolerance(Z_xCalc, refValue);

            Assert.LessOrEqual(actualTolerance, tolerance);

        }
    }
}
