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
    /// Generic rectangle shape with geometric parameters provided in a constructor.
    /// </summary>
    public class SectionRectangular : CompoundShape, ISectionRectangular, ISliceableSection //SectionBaseClass,
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Name">Name</param>
        /// <param name="b">Width</param>
        /// <param name="h">Height</param>
        /// <param name="Centroid">Coordinates of centroid.</param>
        public SectionRectangular(string Name, double b, double h, Point2D Centroid)
        {
            this.b = b;
            this.h = h;
            this.Centroid = Centroid;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="b">Width</param>
        /// <param name="h">Height</param>
        public SectionRectangular(double b, double h):this(null, b, h, new Point2D(0,0))
        {
        }


        public Point2D Centroid  { get; set; }

        private double h;
        public double Height
        {
            get { return h; }
            set { h = value; }
        }
        


        private double b;
        /// <summary>
        /// Width
        /// </summary>
        public double Width
        {
            get { return b; }
            set { b = value; }
        }
        

        public ISection GetWeakAxisClone()
        {
            throw new NotImplementedException();
        }

        public override List<CompoundShapePart> GetCompoundRectangleXAxisList()
        {
            List<CompoundShapePart> rectangles = new List<CompoundShapePart>();
            rectangles.Add(new CompoundShapePart(Width, Height, new Point2D(0, 0)));
            return rectangles;
        }

        public override List<CompoundShapePart> GetCompoundRectangleYAxisList()
        {
            List<CompoundShapePart> rectangles = new List<CompoundShapePart>();
            rectangles.Add(new CompoundShapePart(Width, Height, new Point2D(0, 0)));
            return rectangles;
        }

        protected override void CalculateWarpingConstant()
        {
            Cw = 0.0;
        }

        protected override void CalculateTorsionalConstant()
        {
            //From Boresi, Schmidt; Advanced Mechanics of Materials
            //Table B.1
            _J = (((b * Math.Pow(h, 3) + h * Math.Pow(b, 3))) / (12));
        }

    }
}
