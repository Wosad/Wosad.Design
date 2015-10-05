using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Loads.ASCE.ASCE7_10.DeadLoads.Components;

namespace Wosad.Loads.Tests.ASCE7.ASCE7_10.C03_DeadLoads
{
    [TestFixture]
    public partial class ComponentDeadWeightTests
    {
        [Test]
        public void WoodDoubleFloor2X10_16OCReturnsValue()
        {
            ComponentDoubleWoodFloor wood = new ComponentDoubleWoodFloor(2, 1, 0);
            var woodEntr = wood.Weight;
            Assert.AreEqual(6, woodEntr);
        }
    }
}
