using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.Section.SectionTypes;

namespace Wosad.Common.Tests.Section.ShapeTypes
{
    /// <summary>
    /// Compare calculated properties to C8X18.7 listed properties.
    /// </summary>
    [TestFixture]
    public class SectionChannelRolledTests
    {

        [Test]
        public void SectionChannelRolledReturnsArea()
        {
            SectionChannelRolled shape = new SectionChannelRolled("", 8, 2.53, 0.39, 0.487, 15.0/16.0);
            double A = shape.Area;
            Assert.AreEqual(5.87, Math.Round(A, 2));

        }
        [Test]
        public void SectionChannelRolledReturnsIx()
        {
            SectionChannelRolled shape = new SectionChannelRolled("", 8, 2.53, 0.39, 0.487, 15.0 / 16.0);
            double A = shape.MomentOfInertiaX;
            Assert.AreEqual(48.244, Math.Round(A, 3));

        }

        [Test]
        public void SectionChannelRolledReturnsIy()
        {
            SectionChannelRolled shape = new SectionChannelRolled("", 8, 2.53, 0.39, 0.487, 15.0 / 16.0);
            double A = shape.MomentOfInertiaY;
            Assert.AreEqual(2.455, Math.Round(A, 3));

        }
    }
}
