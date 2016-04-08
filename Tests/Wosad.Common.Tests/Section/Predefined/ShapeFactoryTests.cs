#region Copyright
   /*Copyright (C) 2015 Wosad Inc

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
   */
#endregion
 
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

        [Test]
        public void ShapeFactoryReturnsValueForAngle()
        {
            AiscShapeFactory factory = new AiscShapeFactory();
            ISection section = factory.GetShape("L3-1/2X3-1/2X1/2", Entities.ShapeTypeSteel.Angle);
            Assert.IsTrue(section != null);
        }

    }
}
