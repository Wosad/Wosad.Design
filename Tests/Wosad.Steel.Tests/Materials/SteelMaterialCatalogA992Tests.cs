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

        SteelMaterialCatalog GetSteelMaterial(string SteelMaterialId, double d_b)
        {
            CalcLog cl = new CalcLog();
            SteelMaterialCatalog sm = new SteelMaterialCatalog(SteelMaterialId, d_b, cl);
            return sm;
        }

        [Test]
        public void A992SteelReturnsF_y()
        {
            string SteelMaterialId = "A992";
            double d_b = 0.0;
            SteelMaterialCatalog sm = GetSteelMaterial(SteelMaterialId, d_b);
            double F_y = sm.YieldStress;
            Assert.AreEqual(50.0, F_y);
        }
        [Test]
        public void A992SteelReturnsF_u()
        {
            string SteelMaterialId = "A992";
            double d_b = 0.0;
            SteelMaterialCatalog sm = GetSteelMaterial(SteelMaterialId, d_b);
            double F_u = sm.UltimateStress;
            Assert.AreEqual(65.0, F_u);
        }

        [Test]
        public void A992SteelReturnsE()
        {
            string SteelMaterialId = "A992";
            double d_b = 0.0;
            SteelMaterialCatalog sm = GetSteelMaterial(SteelMaterialId, d_b);
            double E = sm.ModulusOfElasticity;
            Assert.AreEqual(29000.0, E);
        }

        [Test]
        public void A992SteelReturnsG()
        {
            string SteelMaterialId = "A992";
            double d_b = 0.0;
            SteelMaterialCatalog sm = GetSteelMaterial(SteelMaterialId, d_b);
            double G = sm.ShearModulus;
            Assert.AreEqual(11200.0, G);
        }
    }
}
