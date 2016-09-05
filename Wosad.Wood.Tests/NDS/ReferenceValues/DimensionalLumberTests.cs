using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.CalculationLogger;
using Wosad.Wood.NDS.NDS2015;
using Wosad.Wood.NDS.NDS2015.Material;




namespace Wosad.Wood.Tests.NDS.SawnLumber
{
    [TestFixture]
    public partial class WoodVisuallyGradedDimensionLumberTests : ToleranceTestBase
    {

        public WoodVisuallyGradedDimensionLumberTests()
        {

            tolerance = 0.02; //2% can differ from rounding
        }

        double tolerance;

        [Test]
        public void DougFirReturnsBendingReferenceValue()
        {
            CalcLog log = new CalcLog();
            VisuallyGradedDimensionLumber dl = new VisuallyGradedDimensionLumber("DOUGLAS FIR-LARCH (NORTH)", 
                Wood.NDS.Entities.CommercialGrade.Stud,
                Wood.NDS.Entities.SizeClassification.T2inAndWider,log);

            double F_b = dl.F_b;


            double refValue = 650.0;
            double actualTolerance = EvaluateActualTolerance(F_b, refValue);
            Assert.LessOrEqual(actualTolerance, tolerance);

        }



    }
}
