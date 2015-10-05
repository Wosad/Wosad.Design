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
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Common.Entities;
using Wosad.Common.Mathematics;
using Wosad.Common.Section.Interfaces;

namespace Wosad.Common.Section
{
    public abstract class SectionBase: AnalyticalElement, ISection
    {

        public SectionBase():this(null)
        {

        }

        public SectionBase(string Name): this(Name,null)
        {
        }
        public SectionBase(string Name, ICalcLog Log)
        {
            this.name = Name;
            plasticCentroidCoordinate = new Point2D(0,0);
            elasticCentroidCoordinate = new Point2D(0,0);
        }

        private string  name;

        public virtual string  Name
        {
            get { return name; }
        }

        protected double b;

        public double Width
        {
            get { return b; }
            set { b = value; }
        }

        protected double h;

        public double Height
        {
            get { return h; }
            set { h = value; }
        }
        
        

        protected double A;

        public virtual double Area
        {
            get { return A; }
            set { A = value; }
        }

        protected double I_x;

        public virtual double MomentOfInertiaX
        {
            get { return I_x; }
        }

        protected double I_y;

        public virtual double MomentOfInertiaY
        {
            get { return I_y; }

        }

        protected double S_x_Top;

        public virtual double SectionModulusXTop
        {
            get 
            {
                S_x_Top = I_x / (h - y_Bar);
                return S_x_Top; 
            }

        }

        protected double S_x_Bot;

        public virtual double SectionModulusXBot
        {
            get 
            {
                S_x_Bot = I_x / y_Bar;
                return S_x_Bot; 
            }
        }

        protected double S_y_Left;

        public virtual double SectionModulusYLeft
        {
            get 
            {
                S_y_Left = I_y / x_Bar;
                return S_y_Left; 
            }

        }

        protected double S_y_Right;

        public virtual double SectionModulusYRight
        {
            get
            {
                S_y_Right = I_y / (b-x_Bar);
                return S_y_Right;
            }
        }

        protected double Z_x;

        public virtual double PlasticSectionModulusX
        {
            get { return Z_x; }
        }

        protected double Z_y;

        public virtual double PlasticSectionModulusY
        {
            get { return Z_y; }
        }

        protected double r_x;

        public virtual double RadiusOfGyrationX
        {
            get {
                r_x = Math.Sqrt(I_x / A);
                return r_x; }
        }

        protected double r_y;

        public virtual double RadiusOfGyrationY
        {
            get {
                r_y = Math.Sqrt(I_y / A);
                return r_y; }
        }

        protected double x_Bar;

        public virtual double CentroidXtoLeftEdge
        {
            get { return x_Bar; }
        }

        protected double y_Bar;

        public virtual double CentroidYtoBottomEdge
        {
            get { return y_Bar; }
        }

        protected double x_p_Bar;

        public virtual double PlasticCentroidXtoLeftEdge
        {
            get { return x_p_Bar; }
        }

        protected double y_p_Bar;

        public virtual double PlasticCentroidYtoBottomEdge
        {
            get { return y_p_Bar; }

        }

        protected double C_w;

        public virtual double WarpingConstant
        {
            get { return C_w; }

        }

        protected double J;

        public virtual double TorsionalConstant
        {
            get { return J; }

        }


        protected Point2D elasticCentroidCoordinate;


        public Point2D ElasticCentroidCoordinate
        {
            get 
            { 
                return elasticCentroidCoordinate; 
            }
            set 
            { 
                elasticCentroidCoordinate = value; 
            }
        }

        protected Point2D plasticCentroidCoordinate;

        public Point2D PlasticCentroidCoordinate
        {
            get
            {
                return plasticCentroidCoordinate; 
            }
            set
            {
                plasticCentroidCoordinate = value; 
            }
        }

        public abstract ISection Clone();

    }
}
