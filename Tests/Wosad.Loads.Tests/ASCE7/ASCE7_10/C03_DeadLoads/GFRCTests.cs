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
        public void GFRC120pcfAnd5_8BackingReturnsValue()
        {
            ComponentGFRC gfrc = new ComponentGFRC(0, 1, 0);
            var gfrcEntr = gfrc.Weight;
            Assert.AreEqual(6.3, gfrcEntr);
        }

         [Test]
        public void GFRC120pcfAnd1_2BackingReturnsValue()
        {
            BuildingElementComponent bec = new BuildingElementComponent("GFRCPanels", 0, 0, 0.0, "");
            double gfrcEntr = bec.GetComponentWeight().LoadValue;
            Assert.AreEqual(5, gfrcEntr);
        }
    }
}
