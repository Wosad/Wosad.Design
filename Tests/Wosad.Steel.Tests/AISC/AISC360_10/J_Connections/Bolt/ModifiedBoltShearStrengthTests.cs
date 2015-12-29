using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Steel.AISC;
using Wosad.Steel.AISC.AISC360_10.Connections.Bolted;
using Wosad.Steel.AISC.Interfaces;

namespace Wosad.Steel.Tests.AISC.AISC360_10.Connections.Bolt
{
    [TestFixture]
    public class ModifiedBoltShearStrengthTests
    {
        //AISC Design Examples V14
        //EXAMPLE J.3 COMBINED TENSION AND SHEAR IN BEARING TYPE CONNECTIONS 

         [Test]
        public void GetNominalTensileStrengthModifiedToIncludeTheEffectsOfShearStress()
        {
            BoltFactory bf = new BoltFactory("A325");

            BoltBearingGroupA bolt = new BoltBearingGroupA(3.0 / 4.0, BoltThreadCase.Included, null);
            double V = 8.0;
            double phi_R_n = bolt.GetAvailableTensileStrength(V);

            Assert.AreEqual(25.4, Math.Round(phi_R_n,1));
        }

        //AISC Desin guide 29
        //Example 5.4 
        //Page 110

         [Test]
         public void GetNominalTensileStrengthModifiedToIncludeTheEffectsOfShearStressDG29()
         {
             BoltFactory bf = new BoltFactory("A325");

             IBoltBearing bolt = bf.GetBearingBolt(7.0 / 8.0, "N");
             double V = 8.05;
             double phi_R_n = bolt.GetAvailableTensileStrength(V);

             Assert.AreEqual(39.3, Math.Round(phi_R_n, 1));
         }
    }
}
