﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Analysis.Torsion;

namespace Wosad.Analysis.Tests.BeamTorsion
{
    [TestFixture]
    public partial class TorsionalFunctionsTests : ToleranceTestBase
    {

        [Test]
        public void TorsionalFunctionCase6ReturnsSecondDerivativeAtSupport()
        {

            SetAiscDG9Example5_1Parameters();
            z = 0.0 * L;
            TorsionalFunctionFactory tf = new TorsionalFunctionFactory();
            //double a_rev = Math.Sqrt(E * C_w / (G * J));
            ITorsionalFunction function = tf.GetTorsionalFunction(TorsionalFunctionCase.Case6, E, G, J, L, z, T_u,C_w , t, alpha);
            double theta_2der = function.Get_theta_2();
            double refValue = 0.30;
            double ratio = L / a;
            double GraphValuePredicted = theta_2der * G * J / T_u * a;
            double actualTolerance = EvaluateActualTolerance(GraphValuePredicted, refValue);
            Assert.LessOrEqual(actualTolerance, tolerance);

        }

        [Test]
        public void TorsionalFunctionCase6ReturnsFirstDerivativeAt02()
        {

            SetAiscDG9Example5_1Parameters();
            z = 0.2 * L;
            TorsionalFunctionFactory tf = new TorsionalFunctionFactory();
            //double a_rev = Math.Sqrt(E * C_w / (G * J));
            ITorsionalFunction function = tf.GetTorsionalFunction(TorsionalFunctionCase.Case6, E, G, J, L, z, T_u, C_w, t, alpha);
            double theta_1der = function.Get_theta_1();
            double refValue = 0.1;
            double ratio = L / a;
            double GraphValuePredicted = theta_1der * G * J / T_u;
            double actualTolerance = EvaluateActualTolerance(GraphValuePredicted, refValue);
            Assert.LessOrEqual(actualTolerance, tolerance);

        }
    }
}
