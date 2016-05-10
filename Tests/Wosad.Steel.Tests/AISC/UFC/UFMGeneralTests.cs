using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Steel.AISC.UFM;

namespace Wosad.Steel.Tests.AISC
{
    [TestFixture]
    public class UFMGeneralTests: ToleranceTestBase
    {
        public UFMGeneralTests()
        {
            tolerance = 0.02; //2% can differ from rounding
        }

        double tolerance;

        /// <summary>
        /// AISC Design Guide 29
        /// Example 5.1 page 52
        /// </summary>
        [Test]
        public void NoMomentCaseExample5_1ReturnsValueBasicCase()
        {
            UFMCaseNoMomentsAtInterfaces ufmCase = new UFMCaseNoMomentsAtInterfaces(21.4, 14.0, 47.2, 17.5, 12.0, 840.0, 0.0,false,0,0);
            double V_uc = ufmCase.V_c;
            double refValueV_uc = 302.0;
            double actualToleranceV_c = EvaluateActualTolerance(V_uc, refValueV_uc);
            Assert.LessOrEqual(actualToleranceV_c, tolerance);

            double V_ub = ufmCase.V_b;
            double refValueV_ub = 269.0;
            double actualToleranceV_b = EvaluateActualTolerance(V_ub, refValueV_ub);
            Assert.LessOrEqual(actualToleranceV_b, tolerance);

            double H_ub = ufmCase.H_b;
            double refValueH_ub = 440.0;
            double actualToleranceH_b = EvaluateActualTolerance(H_ub, refValueH_ub);
            Assert.LessOrEqual(actualToleranceH_b, tolerance);

            double H_uc = ufmCase.H_c;
            double refValueH_uc = 176.0;
            double actualToleranceH_c = EvaluateActualTolerance(H_uc, refValueH_uc);
            Assert.LessOrEqual(actualToleranceH_c, tolerance);

        }

        [Test]
        public void NoMomentCaseExample5_1ReturnsValueDistortionalMomentCase()
        {
            UFMCaseNoMomentsAtInterfaces ufmCase = new UFMCaseNoMomentsAtInterfaces(21.4, 14.0, 47.2, 17.5, 12.0, 840.0, 50.0, true, 1270, 100);
            double H_ubc = ufmCase.H_bc;
            double refValueH_ubc = 220.0;
            double actualToleranceV_c = EvaluateActualTolerance(H_ubc, refValueH_ubc);
            Assert.LessOrEqual(actualToleranceV_c, tolerance);


        }
    }
}
