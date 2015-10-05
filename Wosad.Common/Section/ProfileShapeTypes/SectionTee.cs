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
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wosad.Common.Mathematics;
using Wosad.Common.Section.Interfaces;


namespace Wosad.Common.Section.SectionTypes
{
    /// <summary>
    /// Generic tee shape with geometric parameters provided in a constructor.
    /// </summary>
    public class SectionTee:CompoundShape, ISectionTee
    {
        public SectionTee(string Name, double Depth, double Width, double FlangeThickness, double WebThickness)
            :base(Name)
        {

        }

        #region Properties specific to Tees

        private double h;

        public double Height
        {
            get { return h; }

        }

        private double b_f;

        public double FlangeWidth
        {
            get { return b_f; }
        }

        private double t_f;

        public double FlangeThickness
        {
            get { return t_f; }
        }

        private double t_w;

        public double WebThickness
        {
            get { return t_w; }
        }

        private double d;

        public double StemHeight
        {
            get { return d; }
            set { d = value; }
        }
        
        #endregion

        /// <summary>
        /// Defines a set of rectangles for analysis with respect to 
        /// x-axis, each occupying full width of section.
        /// </summary>
        /// <returns>List of analysis rectangles</returns>
        public override List<CompoundShapePart> GetCompoundRectangleXAxisList()
        {

            List<CompoundShapePart> rectX = new List<CompoundShapePart>()
            {
                new CompoundShapePart(FlangeWidth,FlangeThickness, new Point2D(0,-FlangeThickness/2)),
                new CompoundShapePart(WebThickness,Height-FlangeThickness, new Point2D(0,-(Height-FlangeThickness)/2-FlangeThickness)),
            };
            return rectX;
        }

        /// <summary>
        /// Defines a set of rectangles for analysis with respect to 
        /// y-axis, each occupying full height of section. The rectangles are rotated 90 deg., because internally the properties are calculated  with respect to x-axis.
        /// </summary>
        /// <returns>List of analysis rectangles</returns>
        public override List<CompoundShapePart> GetCompoundRectangleYAxisList()
        {
            List<CompoundShapePart> rectY = new List<CompoundShapePart>()
            {
                new CompoundShapePart((FlangeWidth-WebThickness)/2,FlangeThickness, new Point2D(-((FlangeWidth -WebThickness)/4+WebThickness/2) ,-FlangeThickness/2)),
                new CompoundShapePart(WebThickness,Height, new Point2D(0,-Height/2)),
                new CompoundShapePart((FlangeWidth-WebThickness)/2,FlangeThickness, new Point2D((FlangeWidth -WebThickness)/4+WebThickness/2 ,-FlangeThickness/2)),
            };
            return rectY;
        }


protected override void CalculateWarpingConstant()
{
    throw new NotImplementedException();
}
    }
}
