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
    /// This shapes accounts for fillet areas as is typical for rolled shapes.
    /// </summary>
    public class SectionIRolled: SectionI, ISectionIRolled
    {

        public SectionIRolled(string Name, double Depth, double FlangeWidth, double FlangeThickness, 
            double WebThickness, double FilletDistance)
            : base(Name, Depth,FlangeWidth,FlangeThickness,WebThickness)
        {
            this.FilletDistance = FilletDistance;
        }


        private double flangeClearDistanceWithoutFillets;

        public double FlangeClearDistanceWithoutFillets
        {
            get 
            {
                flangeClearDistanceWithoutFillets = Height - FlangeThicknessTop - FlangeThicknessBottom - 2*FilletDistance;
                return flangeClearDistanceWithoutFillets; 
            }
        }

        private double k;

        public double FilletDistance
        {
            get { return k; }
            set { k = value; }
        }


        /// <summary>
        /// Defines a set of rectangles for analysis with respect to 
        /// x-axis, each occupying full width of section.
        /// </summary>
        /// <returns>List of analysis rectangles</returns>
        public override List<CompoundShapePart> GetCompoundRectangleXAxisList()
        {
            double FlangeThickness = this.FlangeThicknessTop;
            double FlangeWidth = this.FlangeWidthTop;

            CompoundShapePart       TopFlange       = new CompoundShapePart(FlangeWidth, FlangeThickness, new Point2D(0, Height / 2 - FlangeThickness / 2));
            CompoundShapePart       BottomFlange    = new CompoundShapePart(FlangeWidth, FlangeThickness, new Point2D(0, -(Height / 2 - FlangeThickness / 2)));
            PartWithDoubleFillet    TopFillet       = new PartWithDoubleFillet(k, WebThickness, new Point2D(0, Height / 2 - FlangeThickness), true);
            PartWithDoubleFillet    BottomFillet    = new PartWithDoubleFillet(k, WebThickness, new Point2D(0, -(Height / 2 - FlangeThickness)), false);
            CompoundShapePart Web = new CompoundShapePart(WebThickness, Height - 2 * FlangeThickness - 2 * FilletDistance, new Point2D(0, 0));

            List<CompoundShapePart> rectX = new List<CompoundShapePart>()
            {
                 TopFlange,  
                 TopFillet,
                 Web,
                 BottomFillet,
                 BottomFlange
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

            CompoundShapePart LeftFlange = new CompoundShapePart(2 * FlangeThickness, (FlangeWidth - WebThickness - 2.0 * k) / 2.0,
                new Point2D(0,(FlangeWidth -WebThickness-2.0*k)/4.0+WebThickness/2.0+k));
            CompoundShapePart RightFlange = new CompoundShapePart(2 * FlangeThickness, (FlangeWidth - WebThickness - 2.0 * k) / 2.0,
                new Point2D(0, -((FlangeWidth - WebThickness - 2.0 * k) / 4.0 + WebThickness / 2.0 + k)));
            PartWithDoubleFillet    LeftFillet       = new PartWithDoubleFillet(k, 2*FlangeThickness, new Point2D(0, (WebThickness)/2), false);
            PartWithDoubleFillet RightFillet = new PartWithDoubleFillet(k, 2 * FlangeThickness, new Point2D(0, -(WebThickness)/2), true);
            CompoundShapePart Web = new CompoundShapePart(Height, WebThickness, new Point2D(0, 0)); 

            List<CompoundShapePart> rectY = new List<CompoundShapePart>()
            {
                LeftFlange,   
                LeftFillet,
                Web,
                RightFillet,
                RightFlange  
            };
            return rectY;
        }
        
    }
}
