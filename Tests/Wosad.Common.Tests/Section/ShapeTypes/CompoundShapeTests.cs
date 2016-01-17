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
using Wosad.Common.Section.Predefined;

namespace Wosad.Common.Tests.Section.ShapeTypes
{
    [TestFixture]
    public class CompoundShapeTests : ToleranceTestBase
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
            double Zx = shape.Z_x;
            Assert.AreEqual(94733.3, Math.Round(Zx,1));

        }

        public CompoundShapeTests()
        {
            tolerance = 0.07; //7% can differ from fillet areas and rounding in the manual
        }

        double tolerance;

        /// <summary>
        /// WT8X50 Plastic neutral axis location
        /// </summary>
        [Test]
        public void CompoundShapeReturnsPNA()
        {


            //Properties
            double d	=	8.49 ;
            double b_f	=	10.4 ;
            double t_w	=	0.585;
            double t_f = 0.985;
            double k = 1.39;
            double y_p = 0.706;
            double refValue = d - y_p;

            CompoundShapePart TopFlange = new CompoundShapePart(b_f, t_f, new Point2D(0, d  - t_f / 2));
            PartWithDoubleFillet TopFillet = new PartWithDoubleFillet(k, t_w, new Point2D(0, d  - t_f), true);
            CompoundShapePart Web = new CompoundShapePart(t_w, d - t_f - k, new Point2D(0, d/2));

            List<CompoundShapePart> tee = new List<CompoundShapePart>()
            {
                 TopFlange,  
                 TopFillet,
                 Web,
            };

            ArbitraryCompoundShape shape = new ArbitraryCompoundShape(tee, null);
            double y_pCalculated = shape.y_pBar;
            double actualTolerance = EvaluateActualTolerance(y_pCalculated, refValue);
            Assert.LessOrEqual(actualTolerance, tolerance);

        }
    }
}
