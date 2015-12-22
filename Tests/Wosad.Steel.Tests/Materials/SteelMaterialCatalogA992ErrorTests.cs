using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.CalculationLogger;
using Wosad.Steel.AISC.SteelEntities.Materials;

namespace Wosad.Steel.Tests.Materials
{
    [TestFixture]
    public partial class SteelMaterialCatalogA992Tests
    {

        [Test]
        public void A992SteelReturnsF_yWithSpecifiedDiameter()
        {
            string SteelMaterialId = "A992";
            double d_b = 1.0;
            SteelMaterialCatalog sm = GetSteelMaterial(SteelMaterialId, d_b);
            double F_y = sm.YieldStress;
            Assert.AreEqual(50.0, F_y);
        }

        [Test]
        public void A992SteelReturnsF_yWithSpecifiedWrongDiameter()
        {
            string SteelMaterialId = "A992";
            double d_b = 100.0;
            SteelMaterialCatalog sm = GetSteelMaterial(SteelMaterialId, d_b);
            double F_y = sm.YieldStress;
            Assert.AreEqual(50.0, F_y);
        }

    }
}
