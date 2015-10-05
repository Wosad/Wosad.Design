using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Loads.ASCE.ASCE7_10.LiveLoads;

namespace Wosad.Loads.Tests
{
    [TestFixture]
    public class LiveLoadTests
    {
        [Test]
        public void  ReturnsValueOffice()
        {
            double q = 0;
            LiveLoadBuilding lb = new LiveLoadBuilding();
            q=lb.GetLiveLoad("Office", false);
            Assert.AreEqual(50.0, q);
        }
        [Test]
        public void ReturnsValueHouseWithPartitions()
        {
            double q = 0;
            LiveLoadBuilding lb = new LiveLoadBuilding();
            q = lb.GetLiveLoad("House", true,15);
            Assert.AreEqual(55.0, q);
        }
    }
}
