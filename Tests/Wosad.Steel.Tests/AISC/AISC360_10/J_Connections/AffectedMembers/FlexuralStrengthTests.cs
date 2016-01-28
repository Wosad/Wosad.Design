using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.CalculationLogger;
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Common.Section.SectionTypes;
using Wosad.Steel.AISC.AISC360_10.Connections.AffectedMembers;
using Wosad.Steel.AISC.Interfaces;
using Wosad.Steel.AISC.SteelEntities.Materials;

namespace Wosad.Steel.Tests.AISC.AISC360_10.Connections.AffectedMembers
{
    [TestFixture]
    public class FlexuralStrengthTests
    {
        [Test]
        public void ConnectedPlateReturnsFlexuralStrength()
        {
            ICalcLog log = new  CalcLog();
            SectionRectangular Section = new SectionRectangular(0.5, 8);
            ISteelMaterial Material = new SteelMaterial(50);
            AffectedElementInFlexure element = new AffectedElementInFlexure(Section, Material, log);
            double phiM_n = element.GetFlexuralStrength();
            Assert.AreEqual(360.0, phiM_n);
        }
    }
}
