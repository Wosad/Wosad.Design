using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Steel.AISC;
using Wosad.Steel.AISC.AISC360_10.Connections.Bolted;

namespace Wosad.Steel.Tests.AISC.AISC360_10.J_Connections.Bolt
{
    [TestFixture]
    public class ModifiedBoltShearStrength
    {

         [Test]
        public void GetNominalTensileStrengthModifiedToIncludeTheEffectsOfShearStress()
        {
            BoltFactory bf = new BoltFactory("A325");

            BoltBearingGroupA bolt = new BoltBearingGroupA(3.0 / 4.0, BoltThreadCase.Included, null);
            double V = 8.0;
            double phi_R_n = bolt.GetAvailableTensileStrength(V);

            Assert.AreEqual(25.4, Math.Round(phi_R_n,1));
        }
    }
}
