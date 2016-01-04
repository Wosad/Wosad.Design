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
    /// Generic box shape with geometric parameters provided in a constructor.
    /// It is assumed that the corners are sharp, as is the case in built-up 
    /// box girders and columns.
    /// </summary>
    public class SectionBox: CompoundShape, ISectionBox
    {

        #region Section properties specific to Box
        private double h;

        public double Height
        {
            get { return h; }
        }


        private double flangeCentroidDistance;

        public double FlangeCentroidDistance
        {
            get { return flangeCentroidDistance; }
        }

        private double b;

        public double Width
        {
            get { return b; }
        }

        private double tf;

        public double FlangeThickness
        {
            get { return tf; }
        }

        private double tw;

        public double WebThickness
        {
            get { return tw; }
        }

        private double webClearDistanceWithoutFlanges;

        public double WebClearDistanceWithoutFlanges
        {
            get { return webClearDistanceWithoutFlanges; }
        } 
        #endregion


        public SectionBox(string Name, double Depth, double Width, double FlangeThickness, double WebThickness): base(Name)
        {
            this.h = Depth;
            this.b = Width;
            this.tf = FlangeThickness;
            this.tw = WebThickness;
            this.flangeCentroidDistance = h - tf;
            this.webClearDistanceWithoutFlanges = h - 2.0 * tf;
        }


        /// <summary>
        /// Defines a set of rectangles for analysis with respect to 
        /// x-axis, each occupying full width of section.
        /// </summary>
        /// <returns>List of analysis rectangles</returns>
        public override List<CompoundShapePart> GetCompoundRectangleXAxisList()
        {
            List<CompoundShapePart> rectX = new List<CompoundShapePart>()
            {
                new CompoundShapePart(Width,FlangeThickness, new Point2D(0,Height/2-FlangeThickness/2)),
                new CompoundShapePart(WebThickness*2,Height-2*FlangeThickness, new Point2D(0,0)),
                new CompoundShapePart(Width,FlangeThickness, new Point2D(0,-(Height/2-FlangeThickness/2)))
            };
            return rectX;
        }

        /// <summary>
        /// Defines a set of rectangles for analysis with respect to 
        /// y-axis, each occupying full height of section. The rectangles are rotated 90 deg., 
        /// because internally the properties are calculated  with respect to x-axis.
        /// </summary>
        /// <returns>List of analysis rectangles</returns>
        public override List<CompoundShapePart> GetCompoundRectangleYAxisList()
        {
            List<CompoundShapePart> rectY = new List<CompoundShapePart>()
            {
                new CompoundShapePart(Height,WebThickness, new Point2D(0,Width-WebThickness/2.0)),
                new CompoundShapePart(2*FlangeThickness, Width-2*WebThickness, new Point2D(0,0)),
                new CompoundShapePart(Height, WebThickness, new Point2D(0, -(Width-WebThickness/2.0))),
                
            };
            return rectY;
        }


        public ISection GetWeakAxisClone()
        {
            string cloneName = this.Name + "_clone";
            return new SectionBox(cloneName, b, h, tw, tf);
        }


        protected override void CalculateWarpingConstant()
        {
            Cw = 0.0;
        }

        /// <summary>
        /// From:
        /// TORSIONAL SECTION PROPERTIES OF STEEL SHAPES
        ///Canadian Institute of Steel Construction, 2002
        /// </summary>
        protected override void CalculateTorsionalConstant()
        {
            double p=2*((h-tf)+(b-tw));
            double A_p=(h-tf)*(b-tw);
            _J=((4*A_p*A_p*tw) / (p)); //need to confirm tw in this equation
        }
    }
}
