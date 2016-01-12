using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.Section.Interfaces;
using Wosad.Common.Section.Predefined;

namespace Wosad.Common.Tests.Section.Predefined
{

    [TestFixture]
    public class ShapeFactoryTests
    {
        //[Test]
        //public void ShapeFactoryReturnsValueForDoubleAngle()
        //{
        //    AiscShapeFactory factory = new AiscShapeFactory();
        //    ISection section = factory.GetShape("2L4X4X3/8X3/4", Entities.ShapeTypeSteel.DoubleAngle);
        //    Assert.IsTrue(section != null);
        //}

        [Test]
        public void ShapeFactoryReturnsValueForIBeam()
        {
            AiscShapeFactory factory = new AiscShapeFactory();
            ISection section = factory.GetShape("W18X35", Entities.ShapeTypeSteel.IShapeRolled);
            Assert.IsTrue(section != null);
        }

    }
}
