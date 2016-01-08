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


        public SectionI(string Name, double d, double b_f, double t_f, 
            double t_w)
            : base(Name)
        {
            this._d = d;
            this._b_f = b_f;
            this._b_fTop = b_f;
            this._t_fTop = t_f;
            this._b_fBot = b_f;
            this._t_fBot = t_f;
            this._t_w = t_w;
        }

        public SectionI(string Name, double d, double b_fTop, double b_fBot,
            double t_fTop, double t_fBot, double t_w)
            : base(Name)
        {
            this._d = d;
            this._b_fTop = b_fTop;
            this._t_fTop = t_fTop;
            this._b_fBot = b_fBot;
            this._t_fBot = t_fBot;
            this._t_w = t_w;
        }

        #region Properties specific to I-Beam

        private double _b_f;

        private double _d;

        public double d
        {
            get { return _d; }
        }


        private double _h_o;

        public double h_o
        {
            get {
                double df = _d - (this.t_fTop / 2 + this.t_fBot / 2);
                return _h_o; }
        }

        private double _b_fTop;

        public double b_fTop
        {
            get { return _b_fTop; }
        }

        private double _t_fTop;

        public double t_fTop
        {
            get { return _t_fTop; }
        }

        private double _b_fBot;

        public double b_fBot
        {
            get { return _b_fBot; }
        }

        private double _t_fBot;

        public double t_fBot
        {
            get { return _t_fBot; }
        }

        private double _t_w;

        public double t_w
        {
            get { return _t_w; }
        }

        //private double filletDistance;

        //public double FilletDistance
        //{
        //    get { return filletDistance; }
        //    set { filletDistance = value; }
        //}



        double _T;
        public double T
        {
            get
            {
                _T = _d - t_fBot - t_fTop;
                return _T;
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
            double t_f = this.t_fTop;
            double b_f = this.b_fTop;

            List<CompoundShapePart> rectX = new List<CompoundShapePart>()
            {
                new CompoundShapePart(b_f,t_f, new Point2D(0,d/2-t_f/2)),
                new CompoundShapePart(t_w,d-2*t_f, new Point2D(0,0)),
                new CompoundShapePart(b_f,t_f, new Point2D(0,-(d/2-t_f/2)))
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

            // I-shape converted to X-shape 
            List<CompoundShapePart> rectY = new List<CompoundShapePart>()
            {
                new CompoundShapePart(2*FlangeThickness,(FlangeWidth-t_w)/2, new Point2D(0,(FlangeWidth -t_w)/4+t_w/2 )),
                new CompoundShapePart(t_w,d, new Point2D(t_w/2.0,0)),
                new CompoundShapePart(2*FlangeThickness,(FlangeWidth-t_w)/2, new Point2D(0,-((FlangeWidth -t_w)/4+t_w/2))),
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
            double d = this.d;
            double t_1 = t_fTop;
            double t_2 = t_fBot;
            double b_1 = b_fTop;
            double b_2 = b_fBot;

            double d_p=d-((t_1+t_2) / 2);
            double a =1/(Math.Pow(1+(b_1/b_2 ),3)*(t_1/t_2 ) );
            this.Cw =(Math.Pow(d_p,2)*Math.Pow(b_1,3)* t_1*a)/12;

        }


        public double h_web
        {
            get 
            {
                return d - (t_fTop + t_fBot);
            }
        }
    }
}
