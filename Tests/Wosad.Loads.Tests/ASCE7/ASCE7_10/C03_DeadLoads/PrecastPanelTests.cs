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
        public void PrecastPanel6InReturnsValue()
        {
            ComponentPrecastPanel precast = new ComponentPrecastPanel(1, 0, 0);
            var pcEntr = precast.Weight;
            Assert.AreEqual(80, pcEntr);
        }
    }
}
