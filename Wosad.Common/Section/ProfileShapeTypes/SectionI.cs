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
    /// Generic I-shape with geometric parameters provided in a constructor.
    /// This shape has sharp corners, as is typical for built-up shapes.
    /// </summary>
    public class SectionI : CompoundShape, ISectionI
    {


        public SectionI(string Name, double Depth, double FlangeWidth, double FlangeThickness, 
            double WebThickness)
            : base(Name)
        {
            this.height = Depth;
            this.flangeWidth = FlangeWidth;
            this.bf_Top = FlangeWidth;
            this.tf_Top = FlangeThickness;
            this.bf_Bottom = FlangeWidth;
            this.tf_Bottom = FlangeThickness;
            this.webThickness = WebThickness;
        }

        public SectionI(string Name, double Depth, double FlangeWidthTop, double FlangeWidthBottom,
            double FlangeThicknessTop, double FlangeThicknessBottom, double WebThickness)
            : base(Name)
        {
            this.height = Depth;
            this.bf_Top = FlangeWidthTop;
            this.tf_Top = FlangeThicknessTop;
            this.bf_Bottom = FlangeWidthBottom;
            this.tf_Bottom = FlangeThicknessBottom;
            this.webThickness = WebThickness;
        }

        #region Properties specific to I-Beam

        private double flangeWidth;

        private double height;

        public double Height
        {
            get { return height; }
        }


        private double flangeCentroidDistance;

        public double FlangeCentroidDistance
        {
            get {
                double df = height - (this.FlangeThicknessTop / 2 + this.FlangeThicknessBottom / 2);
                return flangeCentroidDistance; }
        }

        private double bf_Top;

        public double FlangeWidthTop
        {
            get { return bf_Top; }
        }

        private double tf_Top;

        public double FlangeThicknessTop
        {
            get { return tf_Top; }
        }

        private double bf_Bottom;

        public double FlangeWidthBottom
        {
            get { return bf_Bottom; }
        }

        private double tf_Bottom;

        public double FlangeThicknessBottom
        {
            get { return tf_Bottom; }
        }

        private double webThickness;

        public double WebThickness
        {
            get { return webThickness; }
        }

        //private double filletDistance;

        //public double FilletDistance
        //{
        //    get { return filletDistance; }
        //    set { filletDistance = value; }
        //}



        double flangeClearDistance;
        public double FlangeClearDistance
        {
            get
            {
                flangeClearDistance = height - tf_Bottom - tf_Top;
                return flangeClearDistance;
            }
        } 
        #endregion


        /// <summary>
        /// Defines a set of rectangles for analysis with respect to 
        /// x-axis, each occupying full width of section.
        /// </summary>
        /// <returns>List of analysis rectangles</returns>
        public override List<CompoundShapePart> GetCompoundRectangleXAxisList()
        {
            double FlangeThickness = this.FlangeThicknessTop;
            double FlangeWidth = this.FlangeWidthTop;

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
            double FlangeThickness = this.FlangeThicknessTop;
            double FlangeWidth = this.FlangeWidthTop;

            // I-shape converted to X-shape 
            List<CompoundShapePart> rectY = new List<CompoundShapePart>()
            {
                new CompoundShapePart(2*FlangeThickness,(FlangeWidth-WebThickness)/2, new Point2D(0,(FlangeWidth -WebThickness)/4+WebThickness/2 )),
                new CompoundShapePart(WebThickness,Height, new Point2D(WebThickness/2.0,0)),
                new CompoundShapePart(2*FlangeThickness,(FlangeWidth-WebThickness)/2, new Point2D(0,-((FlangeWidth -WebThickness)/4+WebThickness/2))),
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
            double d = this.Height;
            double t_1 = tf_Top;
            double t_2 = tf_Bottom;
            double b_1 = bf_Top;
            double b_2 = bf_Bottom;

            double d_p=d-((t_1+t_2) / 2);
            double a =1/(Math.Pow(1+(b_1/b_2 ),3)*(t_1/t_2 ) );
            this.Cw =(Math.Pow(d_p,2)*Math.Pow(b_1,3)* t_1*a)/12;

        }


        public double WebHeight
        {
            get 
            {
                return Height - (FlangeThicknessTop + FlangeThicknessBottom);
            }
        }
    }
}
