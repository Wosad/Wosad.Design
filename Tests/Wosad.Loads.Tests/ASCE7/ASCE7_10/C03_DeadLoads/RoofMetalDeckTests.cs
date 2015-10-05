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
        public void RoofMetalDeckReturnsValue1_5In()
        {
            ComponentRoofDeck rd = new ComponentRoofDeck(0, 0, 0);
            var rdEntr = rd.Weight;
            Assert.AreEqual(2.0, rdEntr);
        }
    }
}
