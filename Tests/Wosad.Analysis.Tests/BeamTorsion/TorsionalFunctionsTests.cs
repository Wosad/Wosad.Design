using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Analysis.Torsion;

namespace Wosad.Analysis.Tests.BeamTorsion
{
    [TestFixture]
    public class TorsionalFunctionsTests : ToleranceTestBase
    {
        private void SetAiscDG9Example5_1Parameters()
        {

            L=180;
            G = 11200;
            T_u = -90;
            J = 1.39;
            t_el = 0.34;
            a = 62.1;
            C_w = 2070;
            W_no=23.6;
            S_w1=33.0;
            Q_f=13.0;
            Q_w=30.2;
            E = 29000;
            t=0;
            alpha = 0.5;
            tolerance = 0.02; //2% can differ from rounding 
        }
            double L;
            double alpha;
            double t;
            double G;
            double E;
            double T_u;
            double J;
            double t_el;
            double a;
            double C_w;
            double W_no;
            double S_w1;
            double Q_f;
            double Q_w;
        double z;
        double tolerance;

        [Test]
        public void TorsionalFunctionCase3ReturnsFirstDerivativeAtMidspan()
        {
            //AISC Design Guide 9 Example 5.1
            //Case 3, with alpha = 0.5:
            SetAiscDG9Example5_1Parameters();
            z = L / 2;
            TorsionalFunctionFactory tf = new TorsionalFunctionFactory();
            ITorsionalFunction function = tf.GetTorsionalFunction(TorsionalFunctionCase.Case3,E,G,J,L,z,T_u,C_w,t,alpha);
            double theta_1der = function.Get_theta_1();
            double refValue = 0;

            double actualTolerance = EvaluateActualTolerance(theta_1der, refValue);
            Assert.LessOrEqual(actualTolerance, tolerance);
        
        }

        [Test]
        public void TorsionalFunctionCase3ReturnsFirstDerivativeAtSupport()
        {
            //AISC Design Guide 9 Example 5.1
            //Case 3, with alpha = 0.5:
            SetAiscDG9Example5_1Parameters();
            z = 0;
            TorsionalFunctionFactory tf = new TorsionalFunctionFactory();
            ITorsionalFunction function = tf.GetTorsionalFunction(TorsionalFunctionCase.Case3,E, G, J, L, z, T_u, a, t, alpha);
            double theta_1der = function.Get_theta_1();
            double refValue = 0.28 * (-5.78) * Math.Pow(10, -3.0);

            double actualTolerance = EvaluateActualTolerance(theta_1der, refValue);
            Assert.LessOrEqual(actualTolerance, tolerance);


        }

        [Test]
        public void TorsionalFunctionCase3ReturnsSecondDerivativeAtMidspan()
        {
            //AISC Design Guide 9 Example 5.1
            //Case 3, with alpha = 0.5:
            SetAiscDG9Example5_1Parameters();
            z = L / 2;
            TorsionalFunctionFactory tf = new TorsionalFunctionFactory();
            ITorsionalFunction function = tf.GetTorsionalFunction(TorsionalFunctionCase.Case3, E, G, J, L, z, T_u, a, t, alpha);
            double theta_2der = function.Get_theta_2();
            double refValue = 0.44 * (-5.78) * Math.Pow(10, -3.0)/62.1;

            double actualTolerance = EvaluateActualTolerance(theta_2der, refValue);
            Assert.LessOrEqual(actualTolerance, tolerance);

        }

        [Test]
        public void TorsionalFunctionCase3ReturnsSecondDerivativeAtSupport()
        {
            //AISC Design Guide 9 Example 5.1
            //Case 3, with alpha = 0.5:
            SetAiscDG9Example5_1Parameters();
            z = 0;
            TorsionalFunctionFactory tf = new TorsionalFunctionFactory();
            ITorsionalFunction function = tf.GetTorsionalFunction(TorsionalFunctionCase.Case3, E, G, J, L, z, T_u, a, t, alpha);
            double theta_2der = function.Get_theta_2();
            double refValue = 0;

            double actualTolerance = EvaluateActualTolerance(theta_2der, refValue);
            Assert.LessOrEqual(actualTolerance, tolerance);

        }


        [Test]
        public void TorsionalFunctionCase3ReturnsThirdDerivativeAtMidspan()
        {
            //AISC Design Guide 9 Example 5.1
            //Case 3, with alpha = 0.5:
            SetAiscDG9Example5_1Parameters();
            z = L / 2;
            TorsionalFunctionFactory tf = new TorsionalFunctionFactory();
            ITorsionalFunction function = tf.GetTorsionalFunction(TorsionalFunctionCase.Case3, E, G, J, L, z, T_u, a, t, alpha);
            double theta_3der = function.Get_theta_3();
            double refValue = 0.5 * (-5.78) * Math.Pow(10, -3.0) / 62.1;

            double actualTolerance = EvaluateActualTolerance(theta_3der, refValue);
            Assert.LessOrEqual(actualTolerance, tolerance);

        }

        [Test]
        public void TorsionalFunctionCase3ReturnsThirdDerivativeAtSupport()
        {
            //AISC Design Guide 9 Example 5.1
            //Case 3, with alpha = 0.5:
            SetAiscDG9Example5_1Parameters();
            z = 0;
            TorsionalFunctionFactory tf = new TorsionalFunctionFactory();
            ITorsionalFunction function = tf.GetTorsionalFunction(TorsionalFunctionCase.Case3, E, G, J, L, z, T_u, a, t, alpha);
            double theta_3der = function.Get_theta_3();
            double refValue = 0.22 * (-5.78) * Math.Pow(10, -3.0) / 62.1;

            double actualTolerance = EvaluateActualTolerance(theta_3der, refValue);
            Assert.LessOrEqual(actualTolerance, tolerance);

        }

        [Test]
        public void TorsionalFunctionCase6ReturnsSecondDerivativeAtSupport()
        {

            SetAiscDG9Example5_1Parameters();
            z = 0.5*L;
            TorsionalFunctionFactory tf = new TorsionalFunctionFactory();
            ITorsionalFunction function = tf.GetTorsionalFunction(TorsionalFunctionCase.Case6, E, G, J, L, z, T_u, a, t, alpha);
            double theta_2der = function.Get_theta_2();
            double refValue = -0.23;
            double ratio = L / a;
            double GraphValuePredicted = theta_2der*G*J/T_u*a;
            double actualTolerance = EvaluateActualTolerance(GraphValuePredicted, refValue);
            Assert.LessOrEqual(actualTolerance, tolerance);

        }
    }
}
