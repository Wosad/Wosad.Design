using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Loads.ASCE.ASCE7_10.General;
using Wosad.Loads.ASCE7.Entities;

namespace Wosad.Loads.Tests.ASCE7.ASCE7_10.C01_General
{
    [TestFixture]   
    public class RiskCategory
    {
        //Commercial building
            [Test]
        public void ReturnsValue()
        {
            Structure s = new Structure();
            BuildingRiskCategory cat =s.GetRiskCategory("Commercial building");
            Assert.AreEqual(BuildingRiskCategory.II, cat);
        }

    }
}
