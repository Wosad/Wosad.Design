using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Steel.AISC360_10.Connections.AffectedElements;

namespace Wosad.Steel.Tests.AISC.AISC360_10.Connections.AffectedMembers
{
    [TestFixture]
    public class GussetPlateTests
    {
        [Test]
        public void GussetSingleBraceReturnsEffectiveLength()
        {
            AffectedElement el = new AffectedElement();
            double KL = el.GetGussetPlateEffectiveCompressionLength(Steel.AISC.GussetPlateConfiguration.SingleBrace, 10, 10);
            Assert.AreEqual(7,KL);
        }

    }
}
