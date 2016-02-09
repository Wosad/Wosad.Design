using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Concrete.ACI.Infrastructure.Entities.Rebar;

namespace Wosad.Concrete.Tests.General.Rebar
{
    [TestFixture]
    public class RebarSectionTests
    {
        [Test]
        public void RebarSectionReturnsArea()
        {
            RebarSection sec = new RebarSection(ACI.Entities.RebarDesignation.No6);
            Assert.AreEqual(0.44, sec.Area);
        }
        
    }
}
