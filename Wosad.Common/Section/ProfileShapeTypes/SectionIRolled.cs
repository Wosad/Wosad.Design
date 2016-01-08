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

        public SectionIRolled(string Name, double d, double b_f, double t_f, 
            double t_w, double k)
            : base(Name, d,b_f,t_f,t_w)
        {
            this.k = k;
        }


        private double _T;

        public double T
        {
            get 
            {
                _T = d - t_fTop - t_fBot - 2*k;
                return _T; 
            }
        }

        private double _k;

        public double k
        {
            get { return _k; }
            set { _k = value; }
        }


        /// <summary>
        /// Defines a set of rectangles for analysis with respect to 
        /// x-axis, each occupying full width of section.
        /// </summary>
        /// <returns>List of analysis rectangles</returns>
        public override List<CompoundShapePart> GetCompoundRectangleXAxisList()
        {
            double t_f = this.t_fTop;
            double b_f = this.b_fTop;

            CompoundShapePart       TopFlange       = new CompoundShapePart(b_f, t_f, new Point2D(0, d / 2 - t_f / 2));
            CompoundShapePart       BottomFlange    = new CompoundShapePart(b_f, t_f, new Point2D(0, -(d / 2 - t_f / 2)));
            PartWithDoubleFillet    TopFillet       = new PartWithDoubleFillet(k, t_w, new Point2D(0, d / 2 - t_f), true);
            PartWithDoubleFillet    BottomFillet    = new PartWithDoubleFillet(k, t_w, new Point2D(0, -(d / 2 - t_f)), false);
            CompoundShapePart Web = new CompoundShapePart(t_w, d - 2 * t_f - 2 * k, new Point2D(0, 0));

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
            double FlangeThickness = this.t_fTop;
            double FlangeWidth = this.b_fTop;

            CompoundShapePart LeftFlange = new CompoundShapePart(2 * FlangeThickness, (FlangeWidth - t_w - 2.0 * k) / 2.0,
                new Point2D(0,(FlangeWidth -t_w-2.0*k)/4.0+t_w/2.0+k));
            CompoundShapePart RightFlange = new CompoundShapePart(2 * FlangeThickness, (FlangeWidth - t_w - 2.0 * k) / 2.0,
                new Point2D(0, -((FlangeWidth - t_w - 2.0 * k) / 4.0 + t_w / 2.0 + k)));
            PartWithDoubleFillet    LeftFillet       = new PartWithDoubleFillet(k, 2*FlangeThickness, new Point2D(0, (t_w)/2), false);
            PartWithDoubleFillet RightFillet = new PartWithDoubleFillet(k, 2 * FlangeThickness, new Point2D(0, -(t_w)/2), true);
            CompoundShapePart Web = new CompoundShapePart(d, t_w, new Point2D(0, 0)); 

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
