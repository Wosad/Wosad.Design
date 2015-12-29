using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Steel.AISC;
using Wosad.Steel.AISC.AISC360_10.Connections.Bolted;

namespace Wosad.Steel.Tests.AISC.AISC360_10.Connections.Bolt
{
    [TestFixture]
    public class BoltNominalStressTests
    {

        // AISC Design examples V14
        //EXAMPLE J.3 COMBINED TENSION AND SHEAR IN BEARING TYPE CONNECTIONS 
        [Test]
        public void BoltReturnsNominalShearStress()
        {
            BoltFactory bf = new BoltFactory("A325");
            IBoltMaterial material = bf.GetBoltMaterial();
            double F_nv = material.GetNominalShearStress(BoltThreadCase.Included);
            Assert.AreEqual(54.0, F_nv);
        }

        [Test]
        public void BoltReturnsNominalShearStressStringInput()
        {
            BoltFactory bf = new BoltFactory("A325");
            IBoltMaterial material = bf.GetBoltMaterial();
            double F_nv = material.GetNominalShearStress("N");
            Assert.AreEqual(54.0, F_nv);
        }

        [Test]
        public void BoltReturnsNominalTensileStress()
        {
            BoltFactory bf = new BoltFactory("A325");
            IBoltMaterial material = bf.GetBoltMaterial();
            double F_nt = material.GetNominalTensileStress(BoltThreadCase.Included);
            Assert.AreEqual(90.0, F_nt);
        }

        [Test]
        public void BoltReturnsNominalTensileStressStringInput()
        {
            BoltFactory bf = new BoltFactory("A325");
            IBoltMaterial material = bf.GetBoltMaterial();
            double F_nt = material.GetNominalTensileStress("N");
            Assert.AreEqual(90.0, F_nt);
        }
    }
}
