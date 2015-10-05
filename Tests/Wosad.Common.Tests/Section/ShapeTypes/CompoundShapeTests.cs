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
using Wosad.Common.Mathematics;
using Wosad.Common.Section;

namespace Wosad.Common.Tests.Section.ShapeTypes
{
    [TestFixture]
    public class CompoundShapeTests
    {
        /// <summary>
        /// Example from paper:
        /// CALCULATION OF THE PLASTIC SECTION MODULUS USING THE COMPUTER 
        /// DOMINIQUE BERNARD BAUER 
        /// AISC ENGINEERING JOURNAL / THIRD QUARTER /1997 
        /// </summary>
        [Test]
        public void CompoundShapeReturnsPlasticSectionModulusZx()
        {
            List<CompoundShapePart> rectX = new List<CompoundShapePart>()
            {
                new CompoundShapePart(125,8, new Point2D(0,-4)),
                new CompoundShapePart(6,100, new Point2D(0,-58)),
                new CompoundShapePart(75,8, new Point2D(0,-112))
            };
            ArbitraryCompoundShape shape = new ArbitraryCompoundShape(rectX,null);
            double Zx = shape.PlasticSectionModulusX;
            Assert.AreEqual(94733.3, Math.Round(Zx,1));

        }
    }
}
