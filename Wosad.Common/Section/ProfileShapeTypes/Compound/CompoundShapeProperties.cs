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
using System.Threading.Tasks;
using Wosad.Common.Mathematics;
using Wosad.Common.Section;
using Wosad.Common.Section.Interfaces;

namespace Wosad.Common.Section
{
    /// <summary>
    /// Shape comprised of full-width rectangles,
    /// providing default implementation of ISliceableSection
    /// for calculation of elastic properties,
    /// location of PNA (Plastic Neutral Axis)
    /// and Plastic Properties
    /// </summary>
    public abstract partial class CompoundShape : ISliceableSection, ISection //SectionBaseClass, 
    {

        bool momentsOfInertiaCalculated;
        bool elasticPropertiesCalculated;
        bool areaCalculated;
        protected bool torsionConstantCalculated {get; set;}
        bool warpingConstantCalculated;
        bool plasticPropertiesCalculated;

        public string Name { get; set; }

        double Ix;
        public double I_x
        {
            get 
            {
                if (momentsOfInertiaCalculated == false)
                {
                    CalculateMomentsOfInertia();
                }
                return Ix;
            }
        }

        double Iy;
        public double I_y
        {
            get 
            {
                if (momentsOfInertiaCalculated == false)
                {
                    CalculateMomentsOfInertia();
                }
                return Iy; 
            }
        }

        private void CalculateMomentsOfInertia()
        {
            
            foreach (var r in RectanglesXAxis)
            {
                double thisA = r.GetArea();
                double thisYbar = (r.Centroid.Y - this.Centroid.Y);
                double thisIx =  r.GetMomentOfInertia() + thisA * Math.Pow(thisYbar, 2);
                Ix = Ix + thisIx;
            }

            //iternally the RectanglesYAxis collection must provide rotated rectangles
            //therefore even though we calculate Iy,
            //we follow the procedures for calculation of Ix since the provided rectanges are not the same.
            //except for the centroid we need to use X coordinate (again because the shape is internally rotated)

            foreach (var r in RectanglesYAxis)
            {
                double thisA = r.GetArea();
                double thisYbar = (r.Centroid.Y - this.Centroid.X);
                double thisIy = r.GetMomentOfInertia() + thisA * Math.Pow(thisYbar, 2);
                Iy = Iy + thisIy;
            }

            momentsOfInertiaCalculated = true;
        }

        double Sxt;
        public double S_xTop
        {
            get {

                if (elasticPropertiesCalculated == false)
                {
                    CalculateElasticProperies();
                }
                return Sxt;
            }
        }



        double Sxb;
        public double S_xBot
        {
            get {

                if (elasticPropertiesCalculated == false)
                {
                    CalculateElasticProperies();
                }
                return Sxb;
            }
        }

        double Syl;
        public double S_yLeft
        {
            get {
                if (elasticPropertiesCalculated == false)
                {
                    CalculateElasticProperies();
                }
                return Syl;
            }
        }

        double Syr;
        public double S_yRight
        {
            get {

                if (elasticPropertiesCalculated == false)
                {
                    CalculateElasticProperies();
                }
                return Syr;
            }
        }

        double Zx;
        public double Z_x
        {
            get 
            {
                if (plasticPropertiesCalculated==false)
                {
                    CalculatePlasticProperties();
                }
                return Zx;
            }
        }

        double Zy;
        public double Z_y
        {
            get {

                if (plasticPropertiesCalculated == false)
                {
                    CalculatePlasticProperties();
                }
                return Zy;
            }
        }

        private class plasticRectangle: CompoundShapePart
        {
            public plasticRectangle(double b, double h, Point2D Centroid):
                base(b,h, Centroid)
            {

            }
            public double An { get; set; }
            public double Y_n_tilda { get; set; }
            public double h_n_tilda { get; set; }
        }
        private void CalculatePlasticProperties()
        {
            //sort rectangles collection to make sure that they go from top to bottom
            var sortedRectanglesX = RectanglesXAxis.OrderByDescending(r => r.InsertionPoint.Y).ToList();
            CalculatePlasticSectionModulus(AnalysisAxis.X, sortedRectanglesX);

            //sort rectangles collection to make sure that they go from left to right
            if (RectanglesYAxis!=null)
            {
                var sortedRectanglesY = RectanglesYAxis.OrderBy(r => r.InsertionPoint.X).ToList();
                CalculatePlasticSectionModulus(AnalysisAxis.Y, sortedRectanglesY); 
            }

        }

        /// <summary>
        /// Calculates plastic newutral axis (PNA) and plastic section modulus
        /// in accordance with procedure in the foloowing paper:
        /// "CALCULATION OF THE PLASTIC SECTION MODULUS USING THE COMPUTER" 
        /// DOMINIQUE BERNARD BAUER 
        /// AISC ENGINEERING JOURNAL / THIRD QUARTER /1997 
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="rects"></param>
        private void CalculatePlasticSectionModulus(AnalysisAxis axis, 
            List<CompoundShapePart> rects)
        {
        
            double Z = 0.0;
            double Atar = _A / 2;
            double Sum_hi=0;  //summation of height of previous rectangles
            double Sum_Ai =0; //summation of areas of previous rectangles

            //find location of PNA
            //and store the information in a list
            List<plasticRectangle> pRects = new List<plasticRectangle>();
            double PNACoordinate= 0;

            foreach (var r in rects)
            {

                double bn=0;
                double hn = 0;
                double yn = 0;
                plasticRectangle pr = null;
                //Create a new rectangle and swith Z ad Y 
                    switch (axis)
                    {
                        case AnalysisAxis.X:
                                pr = new plasticRectangle(r.b,r.h,r.InsertionPoint);
                                bn = pr.b;
                                hn = pr.h;
                            break;
                        case AnalysisAxis.Y:
                                pr = new plasticRectangle(r.h,r.b,new Point2D(r.InsertionPoint.Y,r.InsertionPoint.X));
                                bn = pr.b;
                                hn = pr.h;
                            break;
                    }
                    

                    yn = pr.InsertionPoint.Y; //centroid of this rectangle
                    double An = bn*hn;
                    pr.An = An;
                    //distance from top of the rectangle to the PNA
                    //this number is meaningful only for one rectangle
                    double h_n_tilda = (Atar - Sum_Ai) / bn; 
                    double Y_n_tilda=0;

                    //check if this rectangle is the one where
                    //PNA is located
                    if (h_n_tilda > 0 && h_n_tilda <= hn)
	                {
                        //this condition is met only for one rectangle
		                Y_n_tilda = Sum_hi+h_n_tilda;
                        PNACoordinate = this.YMax - Y_n_tilda;
                        pr.h_n_tilda = h_n_tilda;
	                }
                    else
                    {
                        pr.h_n_tilda = 0;
                    }
                    pr.Y_n_tilda = Y_n_tilda;
                    
                    pRects.Add(pr);
                    Sum_Ai+=An;
                    Sum_hi +=hn;
            }
            //Calculate contribution of this rectangle
            foreach (var pr in pRects)
            {
             double Zn;
             if (pr.Y_n_tilda!=0)
	            {
		            double ZnTop = pr.b*Math.Pow(pr.h_n_tilda,2)/2      ;
                    double ZnBot = pr.b * Math.Pow((pr.h - pr.h_n_tilda), 2) / 2;
                    Zn = ZnTop + ZnBot;
	            }
            else
	            {
                    double dn = pr.InsertionPoint.Y - PNACoordinate ;
                    Zn = Math.Abs(dn) * pr.An;

                } 
                Z += Zn;

            }

            switch (axis)
            {
                case AnalysisAxis.X:
                    this.Zx = Z;
                    this.ypb = PNACoordinate - YMin;
                    break;
                case AnalysisAxis.Y:
                    this.Zy = Z;
                    this.xpl = PNACoordinate -XMin;
                    break;
            }
        }

        double rx;
        public double r_x
        {
            get {
                if (elasticPropertiesCalculated == false)
                {
                    CalculateElasticProperies();
                }
                return rx;
            }
        }

        double ry;
        public double r_y
        {
            get {

                if (elasticPropertiesCalculated == false)
                {
                    CalculateElasticProperies();
                }
                return ry;
            }
        }
        /// <summary>
        /// Calculates section moduli and radii of gyration
        /// </summary>
        private void CalculateElasticProperies()
        {
            double yt = YMax - Centroid.Y;
            double Ix = I_x;
            Sxt = Ix / yt;
            double ybot = y_Bar;
            Sxb = Ix / ybot;
            double xl = x_Bar;
            Syl = Iy / xl;
            double xr = XMax - Centroid.X;
            Syr = Iy / xr;
            double A = this._A;
            rx = Math.Sqrt(Ix / A);
            ry = Math.Sqrt(Iy / A);
           
        }

        double xleft;
        public double x_Bar
        {
            get 
            {
                xleft = Centroid.X - XMin;
                return xleft;
            }
        }
        double yb;
        public double y_Bar
        {
            get 
            {
                yb = Centroid.Y - YMin;
                return yb;
            }
        }

        double xpl;
        public double x_pBar
        {
            get { 
                
                return xpl;
            }
        }

        double ypb;
        public double y_pBar
        {
            get { 

                return ypb;
            }
        }

        protected double Cw;
        public double C_w
        {
            get {

                if (warpingConstantCalculated == false)
                {
                    CalculateWarpingConstant();
                }
                return Cw; 
            }
        }

        protected abstract void CalculateWarpingConstant();


        protected double _J;
        public double J
        {
            get {

                if (torsionConstantCalculated == false)
                {
                    CalculateTorsionalConstant();
                }
                return _J; 
            }
        }

        protected virtual void CalculateTorsionalConstant()
        {
            foreach (var r in RectanglesXAxis)
            {
                _J = _J + r.b * Math.Pow(r.h, 3) / 3;
            }


            torsionConstantCalculated = true;
        }

        public ISection Clone()
        {
            throw new NotImplementedException();
        }

        double _A;
        public double A
        {
            get {
                if (areaCalculated == false)
                {
                    CalculateArea();
                }
                return _A; 
            }
        }

        private void CalculateArea()
        {
          
            foreach (var r in RectanglesXAxis)
            {
                _A = _A + r.GetArea();
            }
            areaCalculated = true;
        }

        enum AnalysisAxis
        {
            X,
            Y
        }
    }
}
