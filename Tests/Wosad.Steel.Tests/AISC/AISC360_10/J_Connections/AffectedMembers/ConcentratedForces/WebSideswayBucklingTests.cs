using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Steel.AISC.AISC360_10.Connections.AffectedMembers.ConcentratedForces;

namespace Wosad.Steel.Tests.AISC.AISC360_10.Connections.AffectedMembers.ConcentratedForces
{

    [TestFixture]
    public class WebSideswayBucklingTests
    {
        [Test]
        public void WebSideswayBucklingReturnsValueW18()
        {
            double t_w = 0.425;
            double t_f = 0.68;
            double h_web = 16.84;
            double L_b_flange = 36.0;
            double M_y = 7300;
            double M_u = 8150;
            double b_f = 11.0;

            bool CompressionFlangeRestrained = true;

            double phiR_n = FlangeOrWebWithConcentratedForces.GetWebSideswayBucklingStrength(t_w, t_f, h_web, L_b_flange, b_f, CompressionFlangeRestrained, M_u, M_y);

        }
    }
}
