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
    /// This shape accounts for rounded fillet corners, but not the sloped flanges
    /// </summary>
    public class SectionChannelRolled : SectionChannel, ISectionChannelRolled
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Name">Shape name</param>
        /// <param name="Depth">Overall depth of member</param>
        /// <param name="FlangeWidth">Flange width</param>
        /// <param name="FlangeThickness"> Flange thickness (average)</param>
        /// <param name="WebThickness">Web thickness</param>
        /// <param name="FilletDistance">Fillet distance (k)</param>
        public SectionChannelRolled(string Name, double Depth, double FlangeWidth,
            double FlangeThickness, double WebThickness,
            double FilletDistance)
            : base(Name, Depth,  FlangeWidth,  FlangeThickness, WebThickness)
        {
            this.k = FilletDistance;
        }


        private double flangeClearDistanceWithoutFillets;

        public double FlangeClearDistanceWithoutFillets
        {
            get 
            {
                flangeClearDistanceWithoutFillets = Height - 2*FlangeThickness- 2*k ;
                return flangeClearDistanceWithoutFillets; 
            }

        }

        private double k;

        public double FilletDistance
        {
            get { return k; }
        }




        /// <summary>
        /// Defines a set of rectangles for analysis with respect to 
        /// x-axis, each occupying full width of section.
        /// </summary>
        /// <returns>List of analysis rectangles</returns>
        public override List<CompoundShapePart> GetCompoundRectangleXAxisList()
        {
            //double FlangeThickness = this.FlangeThicknessTop;
            //double FlangeWidth = this.FlangeWidthTop;

            CompoundShapePart TopFlange = new CompoundShapePart(FlangeWidth, FlangeThickness, new Point2D(0, Height / 2 - FlangeThickness / 2));
            CompoundShapePart BottomFlange = new CompoundShapePart(FlangeWidth, FlangeThickness, new Point2D(0, -(Height / 2 - FlangeThickness / 2)));
            PartWithDoubleFillet TopFillet = new PartWithSingleFillet(k, WebThickness, new Point2D(0, Height / 2 - FlangeThickness), true);
            PartWithDoubleFillet BottomFillet = new PartWithSingleFillet(k, WebThickness, new Point2D(0, -(Height / 2 - FlangeThickness)), false);
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
        /// <returns>List of analysis rtangles</returns>
        public override List<CompoundShapePart> GetCompoundRectangleYAxisList()
        {
            //double FlangeThickness = this.FlangeThicknessTop;
            //double FlangeWidth = this.FlangeWidthTop;


            //Note: all insertion points are calculated from the left side of the shape
            CompoundShapePart Web = new CompoundShapePart(Height, WebThickness,new Point2D(0, - WebThickness/2.0));
            PartWithDoubleFillet Fillet = new PartWithDoubleFillet(k, 2 * FlangeThickness, new Point2D( 0, -WebThickness),true);
            CompoundShapePart Flange = new CompoundShapePart(2 * FlangeThickness, FlangeWidth - WebThickness - k,
                new Point2D(0,-(WebThickness+k+ (FlangeWidth-WebThickness-k)/2.0)));
             

            List<CompoundShapePart> rectY = new List<CompoundShapePart>()
            {
                Web,
                Fillet,
                Flange  
            };
            return rectY;
        }


    }
}
