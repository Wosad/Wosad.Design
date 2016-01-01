using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Steel.AISC.SteelEntities.Bolts;
using b = Wosad.Steel.Tests.AISC.AISC360_10.Connections.Bolt;


namespace Wosad.Steel.Tests.AISC.AISC360_10.Connections.Bolt
{
    /// <summary>
    /// Comparison to AISC Manual values
    /// </summary>
    [TestFixture]

    public class BoltHoleTests
    {
        [Test]
        public void BoltHoleSTDReturnsSize()
        {
            b.BoltGeneral b = new b.BoltGeneral(0.75,0,0);
            double d_h = b.GetBoltHoleWidth(BoltHoleType.STD, false);
            Assert.AreEqual(13.0 / 16.0, d_h);
        }

        [Test]
        public void BoltHoleOVSReturnsSize()
        {
            b.BoltGeneral b = new b.BoltGeneral(0.75, 0, 0);
            double d_h = b.GetBoltHoleWidth(BoltHoleType.OVS, false);
            Assert.AreEqual(15.0 / 16.0, d_h);
        }

        [Test]
        public void BoltHoleSSLReturnsWidth()
        {
            b.BoltGeneral b = new b.BoltGeneral(0.75, 0, 0);
            double d_h = b.GetBoltHoleWidth(BoltHoleType.SSL_Parallel, false);
            Assert.AreEqual(13.0 / 16.0, d_h);
        }


        [Test]
        public void BoltHoleSSLReturnsLength()
        {
            b.BoltGeneral b = new b.BoltGeneral(0.75, 0, 0);
            double d_l = b.GetBoltHoleLength(BoltHoleType.SSL_Parallel, false);
            Assert.AreEqual(1.0, d_l);
        }

    }
}
