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
    /// Generic channel shape with geometric parameters provided in a constructor.
    /// The corners are assumed to be sharp 90-degree corners, as would be typical 
    /// for a shape built-up from plates.
    /// </summary>
    public class SectionChannel : CompoundShape, ISectionChannel
    {

        public SectionChannel(string Name, double Depth, double FlangeWidth, 
            double FlangeThickness, double WebThickness)
            : base(Name)
        {
            this.d = Depth;
            this.b = FlangeWidth;
            this.tf = FlangeThickness;
            this.tw = WebThickness;
        }


        #region Section properties specific to channel

        private double d;

        public double Height
        {
            get { return d; }
        }


        private double flangeCentroidDistance;

        public double FlangeCentroidDistance
        {
            get
            {
                flangeCentroidDistance = d - FlangeThickness / 2.0 - FlangeThickness / 2.0;
                return flangeCentroidDistance;
            }

        }

        private double b;

        public double FlangeWidth
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

        private double flangeClearDistance;

        public double FlangeClearDistance
        {
            get
            {
                flangeClearDistance = d - 2*FlangeThickness;
                return flangeClearDistance;
            }
        }


        private double filletDistance;

        public double FilletDistance
        {
            get { return filletDistance; }
            set { filletDistance = value; }
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
                new CompoundShapePart(FlangeWidth,FlangeThickness, new Point2D(0,Height/2-FlangeThickness/2)),
                new CompoundShapePart(WebThickness,Height-2*FlangeThickness, new Point2D(0,0)),
                new CompoundShapePart(FlangeWidth,FlangeThickness, new Point2D(0,-(Height/2-FlangeThickness/2)))
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
            //Converted to TEE
            //Insertion point measured from top
            List<CompoundShapePart> rectY = new List<CompoundShapePart>()
            {
                new CompoundShapePart(Height, WebThickness, new Point2D(0, WebThickness/2.0)),
                new CompoundShapePart(2*FlangeThickness,FlangeWidth-WebThickness, new Point2D(0, (WebThickness+(FlangeWidth -WebThickness)/2))),
            };
            return rectY;
        }


        /// <summary>
        /// From:
        /// TORSIONAL SECTION PROPERTIES OF STEEL SHAPES
        ///Canadian Institute of Steel Construction, 2002
        /// </summary>
        protected override void CalculateWarpingConstant()
        {
            double d_prime = 0.0;
            d_prime = d - tf;
            double b_prime = 0.0;
            b_prime = b - ((tw) / (2));
            double alpha = 1 / (2 + (d_prime * tw) / (3 * b_prime * tf));
            Cw=Math.Pow((d_prime), 3)*Math.Pow((b_prime), 3)*tf*((1-3*alpha)/6+((alpha*alpha) / (2))*(1+((d_prime*tw) / (6*b_prime*tf))));

        }
    }
}
