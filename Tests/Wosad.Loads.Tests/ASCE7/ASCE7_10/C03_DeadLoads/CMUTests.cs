using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Loads.ASCE.ASCE7_10.DeadLoads;
using Wosad.Loads.ASCE.ASCE7_10.DeadLoads.Components;

namespace Wosad.Loads.Tests.ASCE7.ASCE7_10.C03_DeadLoads
{
    [TestFixture]
    public partial class ComponentDeadWeightTests
    {
        [Test]
        public void UngroutedCMULWReturnsValue8In()
        {
            ComponentCMUUngrouted105 rd = new ComponentCMUUngrouted105(2, 0, 0);
            var masEntr = rd.Weight;
            Assert.AreEqual(31.0, masEntr);
        }
        [Test]
        public void PartiallyGroutedCMULWReturnsValue8In24OC()
        {
            ComponentCMUPartialGrouted105 rd = new ComponentCMUPartialGrouted105(1, 3, 0);
            var masEntr = rd.Weight;
            Assert.AreEqual(46.0, masEntr);
        }
        [Test]
        public void FullyGroutedCMULWReturnsValue8In()
        {
            ComponentCMUGrouted105 rd = new ComponentCMUGrouted105(1, 0, 0);
            var masEntr = rd.Weight;
            Assert.AreEqual(75.0, masEntr);
        }
        [Test]
        public void SolidCMULWReturnsValue8In()
        {
            ComponentCMUSolid105 rd = new ComponentCMUSolid105(2, 0, 0);
            var masEntr = rd.Weight;
            Assert.AreEqual(69.0, masEntr);
        }
    }
}
