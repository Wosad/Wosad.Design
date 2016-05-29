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
    public partial class TorsionalFunctionsTests : ToleranceTestBase
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

    }
}
