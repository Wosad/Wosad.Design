using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Loads.ASCE.ASCE7_10.DeadLoads;

namespace Wosad.Loads.Tests.ASCE7.ASCE7_10.C03_DeadLoads
{
    [TestFixture]
    public partial class ComponentDeadWeightTests
    {
        [Test]
        public void ReturnsValueForCurtainwall()
        {
            BuildingElementComponent bec = new BuildingElementComponent("AluminumCurtainWall");
            ComponentReportEntry wEntr = bec.GetComponentWeight();
            double w = wEntr.LoadValue;
            Assert.AreEqual(10.0, w);
        }
    }
}
