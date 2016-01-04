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
    /// Generic angle shape with geometric parameters provided in a constructor.
    /// Shape properties do not account for rounded corners, typical for rolled shapes
    /// </summary>
    public class SectionAngle : CompoundShape, ISectionAngle, ISliceableSection
    {
        public SectionAngle(string Name, double Height, double Width, double Thickness)
            : base(Name)
        {
            this.h = Height;
            this.b = Width;
            this.t = Thickness;
        }

        private double b;

        public double Width
        {
            get { return b; }
        }

        private double h;

        public double Height
        {
            get { return h; }
            set { h = value; }
        }

        private double t;

        public double Thickness
        {
            get { return t; }
        }
        


        public ISection GetWeakAxisClone()
        {
            string cloneName= this.Name+"_clone";
            return new SectionAngle(cloneName, b, h, t);
        }



        #region Section properties specific to Angle

        public double MomentOfInertiaPrincipalMajor
        {
            get { throw new NotImplementedException(); }
        }

        public double MomentOfInertiaPrincipalMinor
        {
            get { throw new NotImplementedException(); }
        }

        public double SectionModulusPrincipalMajor
        {
            get { throw new NotImplementedException(); }
        }

        public double SectionModulusPrincipalMinor
        {
            get { throw new NotImplementedException(); }
        }

        public double RadiusOfGyrationPrincipalMajor
        {
            get { throw new NotImplementedException(); }
        }

        public double RadiusOfGyrationPrincipalMinor
        {
            get { throw new NotImplementedException(); }
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
                new CompoundShapePart(Thickness,Height-Thickness, new Point2D(Thickness/2.0,(Height-Thickness)/2)),
                new CompoundShapePart(Width,Thickness, new Point2D(Width/2,Thickness/2)),
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
            //angle is rotated 90 deg and converted to TEE
            //Insertion point at the top of TEE
            List<CompoundShapePart> rectY = new List<CompoundShapePart>()
            {
                new CompoundShapePart(Height,Thickness, new Point2D(0, -Thickness/2.0)),
                new CompoundShapePart(Thickness, Width-Thickness, new Point2D(0,-((Width-Thickness)/2+Thickness))),
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
            throw new NotImplementedException();
            double b_prime = this.b-t/2;
            double d_prime = this.h-t/2;
            this.Cw=((Math.Pow(t, 3)) / (36))*(Math.Pow((h), 3)+Math.Pow((b), 3));
        }
        /// <summary>
        /// From:
        /// TORSIONAL SECTION PROPERTIES OF STEEL SHAPES
        ///Canadian Institute of Steel Construction, 2002
        /// </summary>
        protected override void CalculateTorsionalConstant()
        {
            double b_prime = this.b-t/2;
            double d_prime = this.h-t/2;
            this._J=(((d_prime+b_prime)*Math.Pow(t, 3)) / (3));
        }
    }
}
